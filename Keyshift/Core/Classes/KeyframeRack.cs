using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Keyshift.Core.Structs;

namespace Keyshift.Core.Classes
{
    /// <summary>
    /// Base class for Keyframe Racks, which will be the containers of all kinds of keyframes. This class cannot be instantiated.
    /// <para>You shouldn't use this class's derivatives on their own either. Add them to a Timeline.</para>
    /// </summary>
    public abstract class KeyframeRack
    {
        [JsonProperty("name")] public string Name { get; set; } = "Unnamed Track";
        [JsonIgnore]
        public abstract List<Keyframe> OrderedGenericList { get; }

        /// <summary>
        /// Returns the keyframe found at the specified frame. Null if there's none.
        /// </summary>
        /// <param name="frame">Frame to get the keyframe from</param>
        /// <returns></returns>
        public abstract Keyframe GetKeyframeAtPosition(int frame);

        // If not null, this will make all keyframes added to this Rack be forced this Interpolation type.
        public abstract KeyframeType? LockedToInterpolation { get; }

        /// <summary>
        /// Commits changes to this rack and notifies the event handler.
        /// </summary>
        /// <param name="changes">The UncommitedRackChange which contains the changes to perform.</param>
        public abstract void Commit(UncommittedRackChange changes);

        /// <summary>
        /// Same as <b>Commit</b> except it notifies no event. Used for Undo/Redo.
        /// </summary>
        /// <param name="changes">The UncommitedRackChange which contains the changes to perform.</param>
        public abstract void ReCommit(UncommittedRackChange changes);

        /// <summary>
        /// Reverts the changes made from a Commit
        /// </summary>
        /// <param name="commit">The RackCommitInfo which contains the changes that are about to be reverted.</param>
        public abstract void Revert(RackCommitInfo commit);

        /// <summary>
        /// Returns the keyframe behind the current reference frame.
        /// </summary>
        [JsonIgnore]
        public abstract Keyframe ImmediateBehindKeyframe { get; }
        [JsonIgnore]
        public abstract Keyframe ImmediateForwardKeyframe { get; }

        /// <summary>
        /// Updates the Immediate Behind and Immediate Forward Keyframes.
        /// </summary>
        public abstract void UpdateImmediates();

        internal int _currentFrame;

        [JsonIgnore]
        public int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                if (value < 0) value = 0;
                ReferenceFrame = value;
                CurrentFrameChanged?.Invoke();
            }
        }

        /// <summary>
        /// Used by Timeline when Timeline Sync is not enabled
        /// </summary>
        [JsonIgnore]
        public int ReferenceFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                UpdateImmediates();
            }
        }

        /// <summary>
        /// Adds the current value from the Getter Delegate at the desired position
        /// </summary>
        /// <param name="framepos"></param>
        public abstract Keyframe AddCurrentStateAtPosition(int framepos);

        public delegate void OnCurrentFrameChanged();
        /// <summary>
        /// Triggers whenever the frame is changed. Controlled by Timeline. (if overlying Timeline has "Sync" mode on, this triggers everytime)
        /// </summary>
        public event OnCurrentFrameChanged CurrentFrameChanged;

        public event EventHandler<RackCommitInfo> ChangeCommitted;

        protected virtual void OnChangeCommitted(object sender, RackCommitInfo e)
        {
            ChangeCommitted?.Invoke(sender, e);
        }

        public event EventHandler<RackCommitInfo> ChangeReverted;

        protected virtual void OnChangeReverted(object sender, RackCommitInfo e)
        {
            ChangeReverted?.Invoke(sender, e);
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract void Add(Keyframe item);

        public abstract void Clear();

        public abstract void Wipe();

        public abstract bool Remove(Keyframe item);

        /// <summary>
        /// Takes all the Keyframes from another Keyframerack and then clears the other rack.
        /// </summary>
        /// <param name="snatched">The victim KeyframeRack</param>
        public abstract void Snatch(KeyframeRack snatched);

        public abstract Type UnderlyingType { get; }

    }

    /// <summary>
    /// Base class for TYPED Keyframe Racks, which will be the containers of all kinds of keyframes. This class cannot be instantiated.
    /// <para>You shouldn't use this class's derivatives on their own either. Add them to a Timeline.</para>
    /// </summary>
    /// <typeparam name="TKeyframe">The underlying type for the Keyframes.</typeparam>
    [JsonObject]
    public abstract class KeyframeRack<TKeyframe> : KeyframeRack, IList<Keyframe<TKeyframe>> where TKeyframe : new()
    {
        [JsonProperty("list")]
        protected List<Keyframe<TKeyframe>> elements = new List<Keyframe<TKeyframe>>();

        [JsonIgnore]
        public override Type UnderlyingType => typeof(TKeyframe);
        [JsonIgnore]
        public readonly Keyframe<TKeyframe> basicKeyframe;

        [JsonIgnore]
        public List<Keyframe<TKeyframe>> OrderedKeyframes => elements.OrderBy(k => k.Position).ToList();
        [JsonIgnore]
        public override List<Keyframe> OrderedGenericList => OrderedKeyframes.Cast<Keyframe>().ToList();

        /// <summary>
        /// The reason this one exists while BehindKeyframe still exists is because Behind is margin inclusive,
        /// meaning it will include the keyframe at its own position too.
        /// </summary>
        Keyframe<TKeyframe> closestBackKeyframe;
        Keyframe<TKeyframe> closestBehindKeyframe;
        Keyframe<TKeyframe> closestFrontKeyframe;

        public override Keyframe ImmediateBehindKeyframe => closestBackKeyframe;
        public override Keyframe ImmediateForwardKeyframe => closestFrontKeyframe;

        protected Func<TKeyframe> valueGetter;

        protected KeyframeRack()
        {
            elements = new List<Keyframe<TKeyframe>>();
        }

        /// <summary>
        /// Initializes a KeyframeRack with a value getter and an initial keyframe.
        /// </summary>
        /// <param name="initialKeyframe"></param>
        /// <param name="valgetter"></param>
        public KeyframeRack(Keyframe<TKeyframe> initialKeyframe, Func<TKeyframe> valgetter) : this()
        {
            elements.Add(initialKeyframe);
            basicKeyframe = initialKeyframe;
            valueGetter = valgetter;
        }

        public KeyframeRack(Func<TKeyframe> valgetter) : this()
        {
            valueGetter = valgetter;
            basicKeyframe = new Keyframe<TKeyframe>(new TKeyframe());
            AddCurrentStateAtPosition(0);
        }

        protected KeyframeRack(List<Keyframe<TKeyframe>> list, string name)
        {
            elements.Clear();
            basicKeyframe = new Keyframe<TKeyframe>(new TKeyframe());
            elements.AddRange(list.Cast<Keyframe<TKeyframe>>());
            Name = name;
        }

        public int FarthestKeyframePosition()
        {
            return this.Select(x => x.Position).Max();
        }

        /// <summary>
        /// Returns the keyframe found at the specified frame. Null if there's none.
        /// </summary>
        /// <param name="frame">Frame to get the keyframe from</param>
        /// <returns></returns>
        public override Keyframe GetKeyframeAtPosition(int frame)
        {
            if (frame < 0) return null;

            return elements.FirstOrDefault(x => x.Position == frame);
        }

        /// <summary>
        /// Reanalyzes the keyframes in front and behind the current reference trackhead.
        /// </summary>
        public void ReassessClosestFrames()
        {
            List<Keyframe<TKeyframe>> oKeyframes = OrderedKeyframes;
            closestBehindKeyframe = oKeyframes.Where(k => k.Position <= ReferenceFrame && k.SubordinateOf == null)
                .OrderByDescending(k => k.Position).FirstOrDefault();
            closestBackKeyframe = oKeyframes.Where(k => k.Position < ReferenceFrame && k.SubordinateOf == null)
                .OrderByDescending(k => k.Position).FirstOrDefault();
            closestFrontKeyframe = oKeyframes.Where(k => k.Position > ReferenceFrame && k.SubordinateOf == null)
                .OrderBy(k => k.Position).FirstOrDefault();
        }

        /// <summary>
        /// Returns the distance of the trackhead to the specified keyframe. Should throw exception if Keyframe is not present in the KeyframeRack.
        /// </summary>
        /// <param name="kf">The Keyframe to get the distance from</param>
        /// <returns></returns>
        public int TrackHeadDistanceToKeyframe(Keyframe kf)
        {
            return (int)Math.Abs(ReferenceFrame - kf.Position);
        }

        /// <summary>
        /// Wipes itself, takes all the Keyframes from another Keyframerack of (hopefully) the same kind and then clears the other rack, hopefully making GC do its thing.
        /// (no undo)
        /// </summary>
        /// <param name="snatched">The victim KeyframeRack</param>
        public override void Snatch(KeyframeRack snatched)
        {
            Clear();
            foreach (Keyframe<TKeyframe> key in ((KeyframeRack<TKeyframe>)snatched).OrderedKeyframes)
            {
                Add(key);
            }
            snatched.Clear();
        }

        public override void UpdateImmediates()
        {
            ReassessClosestFrames();
        }

        /// <summary>
        /// Calculates the interpolation in the current track head position.
        /// </summary>
        /// <param name="reassess"></param>
        /// <returns>The value of the Rack at the current location.</returns>
        public virtual TKeyframe CalculateInterpolation(bool reassess = true)
        {
            if (reassess) ReassessClosestFrames();
            if (closestBehindKeyframe == null) return elements[0].CurrentValue;
            if (closestFrontKeyframe == null) return closestBehindKeyframe.CurrentValue;
            float progress = (float)TrackHeadDistanceToKeyframe(closestBehindKeyframe) /
                             closestBehindKeyframe.Distance(closestFrontKeyframe);

            switch (closestBehindKeyframe.InterpolationType)
            {
                case KeyframeType.Bezier:
                    {
                        return NBezier(closestBehindKeyframe.Subordinates, progress);
                    }
                case KeyframeType.Linear:
                default:
                    {
                        return Lerp(closestBehindKeyframe, closestFrontKeyframe, progress);
                    }
                case KeyframeType.Fast:
                    {
                        return Lerp(closestBehindKeyframe, closestFrontKeyframe, 1 - (1 - progress) * (1 - progress));
                    }
                case KeyframeType.Slow:
                    {
                        return Lerp(closestBehindKeyframe, closestFrontKeyframe, progress * progress);
                    }
                case KeyframeType.Smooth:
                    {
                        // PIECEWISE FUNCTIONS‼
                        // WHAT THE FUUUUU-
                        return Lerp(closestBehindKeyframe, closestFrontKeyframe, (float)(progress < 0.5 ? 2 * progress * progress : 1 - Math.Pow(-2 * progress + 2, 2) / 2));
                    }
                case KeyframeType.Sharp:
                    {
                        // ANOTHER PIECEWISE FUNCTION‼
                        return Lerp(closestBehindKeyframe, closestFrontKeyframe,
                            (float)(progress < 0.5 ?
                                -2 * Math.Pow((progress - 0.5), 2) + 0.5 :
                                2 * Math.Pow((progress - 0.5), 2) + 0.5));
                    }
                case KeyframeType.Hold:
                    {
                        return closestBehindKeyframe.CurrentValue;
                    }
                case KeyframeType.Shake:
                    {
                        return NShake(closestBehindKeyframe.Subordinates);
                    }

            }

        }

        /// <summary>
        /// Performs a Linear Interpolation between two Keyframes.
        /// </summary>
        /// <param name="start">Starting keyframe</param>
        /// <param name="end">Ending keyframe</param>
        /// <param name="position">Position of the linear interpolation (clamped between 0 and 1)</param>
        /// <returns></returns>
        public abstract TKeyframe Lerp(Keyframe<TKeyframe> start, Keyframe<TKeyframe> end, float position);

        /// <summary>
        /// Performs an N-Bezier interpolation on an array of Keyframes.<para>The first element in the array is the starting point,
        /// the last element in the array is the ending point. Anything in-between is a "control point".</para>
        /// </summary>
        /// <param name="points"><para>Keyframes that compose the Bezier curve.</para></param>
        /// <param name="position"></param>
        /// <returns>Position in the curve (clamped between 0 and 1)</returns>
        public abstract TKeyframe NBezier(Keyframe<TKeyframe>[] points, float position);

        /// <summary>
        /// <para>Performs "shaking" on several "target" keyframes starting from the first keyframe in the array.</para>
        /// <para>Every frame, chooses a dependent keyframe at random and chooses a random linear interpolation progress.</para>
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public abstract TKeyframe NShake(Keyframe<TKeyframe>[] points);

        bool IsPositionBetweenTwoKeyframes(int position, Keyframe<TKeyframe> start, Keyframe<TKeyframe> end)
        {
            if (start == null || end == null) return false;
            return (position < end.Position && position > start.Position);
        }

        public override Keyframe AddCurrentStateAtPosition(int framepos)
        {
            TKeyframe currentVal = valueGetter();
            ReassessClosestFrames();
            if (closestBehindKeyframe != null && !IsPositionBetweenTwoKeyframes(CurrentFrame, closestBehindKeyframe, closestFrontKeyframe) && closestBehindKeyframe.Position == CurrentFrame)
            {
                closestBehindKeyframe.CurrentValue = currentVal;
                return closestBehindKeyframe;
            }
            Keyframe<TKeyframe> _keyframe = new Keyframe<TKeyframe>(currentVal)
            {
                Position = CurrentFrame
            };
            if (LockedToInterpolation != null)
            {
                _keyframe.InterpolationType = (KeyframeType)LockedToInterpolation;
            }
            else if (closestBackKeyframe != null)
            {
                _keyframe.InterpolationType = closestBackKeyframe.InterpolationType;
            }
            Add(_keyframe);
            return _keyframe;
        }

        public override void Revert(RackCommitInfo commit)
        {
            OnChangeReverted(this, commit);
            foreach (Keyframe keyframe in commit.Collisions)
            {
                var kf = (Keyframe<TKeyframe>)keyframe;
                elements.Add(kf);
            }
            foreach (Keyframe kf in commit.ChangePerformed.Keyframes)
            {
                if (!elements.Contains(kf)) elements.Add((Keyframe<TKeyframe>)kf);
                kf.Position -= commit.ChangePerformed.Delta;

                if (LockedToInterpolation != null)
                {
                    kf.InterpolationType = (KeyframeType)LockedToInterpolation;
                }
                else if (commit.ChangePerformed.NewInterpolation != null)
                {
                    kf.InterpolationType = commit.ChangePerformed.OldInterpolations[kf];
                }
            }
        }

        public override void Commit(UncommittedRackChange changes)
        {
            int[] frameNumbers = changes.Keyframes.Select(x => (x.Position + changes.Delta)).ToArray();
            List<Keyframe> conflicts = new List<Keyframe>();
            conflicts.AddRange(elements.Where(x => frameNumbers.Contains(x.Position)));
            RackCommitInfo commitInfo = new RackCommitInfo()
            {
                ChangePerformed = changes,
                Collisions = conflicts.ToArray()
            };
            OnChangeCommitted(this, commitInfo);

            ReCommit(changes);
        }
        public override void ReCommit(UncommittedRackChange changes)
        {
            int[] frameNumbers = changes.Keyframes.Select(x => (x.Position + changes.Delta)).ToArray();
            List<Keyframe> conflicts = new List<Keyframe>();
            conflicts.AddRange(elements.Where(x => frameNumbers.Contains(x.Position)));
            foreach (Keyframe conflKeyframe in conflicts)
            {
                elements.Remove((Keyframe<TKeyframe>)conflKeyframe);
            }
            foreach (Keyframe kf in changes.Keyframes)
            {
                if (!elements.Contains(kf)) elements.Add((Keyframe<TKeyframe>)kf);
                kf.Position += changes.Delta;
                if (LockedToInterpolation != null)
                {
                    kf.InterpolationType = (KeyframeType)LockedToInterpolation;
                }
                else if (changes.NewInterpolation != null)
                {
                    kf.InterpolationType = (KeyframeType)changes.NewInterpolation;
                }
            }

        }

        [JsonIgnore]
        public bool IsReadOnly => false;

        [JsonIgnore]
        public bool IsFixedSize => false;

        [JsonIgnore]
        public int Count => elements.Count();

        public void Add(Keyframe<TKeyframe> item)
        {
            Keyframe<TKeyframe> collision = elements.FirstOrDefault(x => x.Position == item?.Position);
            if (collision != null) Remove(collision);
            if (LockedToInterpolation != null)
            {
                item.InterpolationType = (KeyframeType)LockedToInterpolation;
            }
            elements.Add(item);
            ReassessClosestFrames();
        }

        public override void Add(Keyframe item)
        {
            if (item.GetType() != typeof(Keyframe<TKeyframe>)) throw new ArgumentException($"Cannot add the {item.GetType()} to a {this.GetType()}");
            if (LockedToInterpolation != null)
            {
                item.InterpolationType = (KeyframeType)LockedToInterpolation;
            }
            Add((Keyframe<TKeyframe>)item);
        }

        /// <summary>
        /// Clears the internal list. Should only be used when this is an ephemereal KeyframeRack.
        /// </summary>
        public override void Clear()
        {
            elements.Clear();
        }

        /// <summary>
        /// Clears the internal list and also adds a first keyframe.
        /// </summary>
        public override void Wipe()
        {
            Clear();
            Keyframe<TKeyframe> kf = new Keyframe<TKeyframe>(valueGetter());
            Add(kf);
        }

        public bool Remove(Keyframe<TKeyframe> item)
        {
            if (elements.Count < 2) return false;
            bool worked = elements.Remove(item);
            if (!worked) return false;

            if (closestBehindKeyframe == null || closestFrontKeyframe == null)
            {
                ReassessClosestFrames();
            }

            return true;
        }

        public override bool Remove(Keyframe item)
        {
            if (item.GetType() != typeof(Keyframe<TKeyframe>)) throw new ArgumentException($"Cannot add the {item.GetType()} to a {this.GetType()}");
            return Remove((Keyframe<TKeyframe>)item);
        }


        public bool Contains(Keyframe<TKeyframe> item)
        {
            return elements.Contains(item);
        }

        public IEnumerator<Keyframe<TKeyframe>> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        public int IndexOf(Keyframe<TKeyframe> item)
        {
            return elements.IndexOf(item);
        }

        public void Insert(int index, Keyframe<TKeyframe> item)
        {
            elements.Insert(index, item);

        }

        public void RemoveAt(int index)
        {
            if (elements.Count < 2) return;
            elements.RemoveAt(index);
            if (closestBehindKeyframe == null || closestFrontKeyframe == null)
            {
                ReassessClosestFrames();
            }
        }

        public void CopyTo(Keyframe<TKeyframe>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        public Keyframe<TKeyframe> this[int index]
        {
            get => elements[index];
            set => elements[index] = value;
        }
    }
}
