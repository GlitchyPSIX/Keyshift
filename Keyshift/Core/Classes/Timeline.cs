using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Keyshift.Core.Classes.Rack;
using Newtonsoft.Json;
using Keyshift.Core.Interfaces;
using Keyshift.Core.Structs;
using Keyshift.FormatHelpers;

namespace Keyshift.Core.Classes
{
    public class Timeline
    {
        // TODO: Make this one private and expose a readonlydictionary instead
        public Dictionary<string, KeyframeRack> KeyframeRacks { get; } = new();

        public Dictionary<string, UncommittedRackChange> UncommittedRackChanges { get; } =
            new Dictionary<string, UncommittedRackChange>();

        private int _trackHead;

        public bool Synchronize { get; set; } = true;

        public bool Playing { get; set; }

        public bool Loop { get; set; }

        public int Length { get; set; }

        private float _fps = 30;

        /// <summary>
        /// Frames per second. Used for timestamp calculations.
        /// </summary>
        public float Fps
        {
            get => _fps;
            set
            {
                float temp = value;
                if (IntegerFrames) temp = (float)Math.Round(temp);
                _fps = temp;
            }
        }

        /// <summary>
        /// If true, any FPS changes will be rounded instead of allowing fractionary values.
        /// </summary>
        public bool IntegerFrames = true;

        /// <summary>
        /// Timecode for the current Trackhead position.
        /// </summary>
        /// <returns>A HH:mm:ss.ff formatted timecode.</returns>
        public string TimecodeString() => Timecode.FramesToTimecode(TrackheadPosition, Fps);

        public bool LoopActive { get; set; }

        private bool _isLooping;

        private bool _canLoop => LoopRegion != null && LoopRegion.Length != 0;

        public TimelineRegion LoopRegion { get; set; }

        public List<TimelineRegion> Regions { get; set; }

        public List<Marker> Markers { get; set; }

        private HistoryManager _history;

        public HistoryManager History
        {
            get => _history;
            set
            {
                if (_history != null)
                {
                    _history.BeforeUndo -= OnUndo;
                    _history.BeforeRedo -= OnRedo;
                }

                _history = value;
                _history.BeforeUndo += OnUndo;
                _history.BeforeRedo += OnRedo;
            }
        }

        public delegate void KeyframeChanged(object sender, KeyframeChangedEventArgs e);

        public event KeyframeChanged OnKeyframeChanged;

        public delegate void KeyframeBulkChanged(object sender, KeyframeBulkChangedEventArgs e);

        public event KeyframeBulkChanged OnKeyframeBulkChanged;

        public delegate void ChangesCommitted(object sender, ChangesCommittedEventArgs e);

        public event ChangesCommitted OnChangesCommitted;

        public delegate void ChangesCancelled(object sender, ChangesCancelledEventArgs e);

        public event ChangesCancelled OnChangesCancelled;

        public delegate void TrackheadChanged(object sender, TrackheadChangedEventArgs e);

        public event TrackheadChanged OnTrackheadChanged;

        public delegate void TimelineLoaded(object sender, EventArgs e);

        public event EventHandler<EventArgs> OnTracksChanged;

        /// <summary>
        /// Triggered whenever the Timeline is deserialized to.
        /// </summary>
        public event TimelineLoaded OnTimelineLoaded;

        public List<string> SelectedRacks { get; set; } = new List<string>();
        public string ActiveRack { get; set; }

        public List<Keyframe> SelectedKeyframes { get; set; } = new List<Keyframe>();

        public bool StagedKeyframesPresent => UncommittedRackChanges.Any(x => x.Value != null);

        /// <summary>
        /// The position of the Timeline's trackhead. Moved manually. Triggers events on move.
        /// </summary>
        public int TrackheadPosition
        {
            get => _trackHead;
            set
            {
                int temp = value;
                temp = Math.Max((value >= Length ? Length : value), 0);

                if (LoopActive && _canLoop)
                {
                    if (_isLooping && !Playing) {
                        _isLooping = false;
                    }

                    if (!_isLooping && Playing && LoopRegion.IsPositionInsideRegion(temp))
                    {
                        _isLooping = true;
                    }

                    if (_isLooping && temp > LoopRegion.FrameEnd) {
                        temp = LoopRegion.FrameStart;
                    }
                }

                OnTrackheadChanged?.Invoke(this, new TrackheadChangedEventArgs(temp, _trackHead));
                _trackHead = temp;
                
                foreach (KeyframeRack kRack in KeyframeRacks.Values)
                {
                    if (Synchronize) kRack.CurrentFrame = _trackHead;
                    else kRack.ReferenceFrame = _trackHead;
                }
            }
        }

        /// <summary>
        /// "Staging" refers to marking keyframes as uncommitted and allowing delta changes to the specific rack, uncommitted.
        /// </summary>
        public void StageSelectedKeyframes()
        {
            if (StagedKeyframesPresent) return; // staging area must be clean otherwise no

            foreach (KeyValuePair<string, KeyframeRack> kRack in KeyframeRacks)
            {
                // woah²! antidupes!
                Keyframe[] selected = kRack.Value.OrderedGenericList.Where(x => SelectedKeyframes.Contains(x))
                    .Distinct().ToArray();
                if (selected.Length == 0) continue;
                UncommittedRackChange changes = new UncommittedRackChange(selected);
                UncommittedRackChanges[kRack.Key] = changes;
            }
        }

        /// <summary>
        /// Deletes the keyframes under the trackhead from their racks.
        /// </summary>
        public void DeleteKeyframesAtTrackhead()
        {
            Keyframe[] underTrackhead = GetKeyframesAtTrackhead();
            foreach (KeyValuePair<string, KeyframeRack> kRack in KeyframeRacks)
            {
                Keyframe[] selected = kRack.Value.OrderedGenericList.Where(x => underTrackhead.Contains(x)).ToArray();
                if (selected.Length == 0) continue;
                foreach (Keyframe sKf in selected)
                {
                    kRack.Value.Remove(sKf);
                    OnKeyframeChanged?.Invoke(this, new KeyframeChangedEventArgs(sKf, ChangeType.DELETION, kRack.Key));
                }
            }
        }

        private void OnRackCommit(object rack, RackCommitEventArgs info)
        {
            // Rack is always type KeyframeRack
            KeyframeRack actualRack = (KeyframeRack)rack;
            History?.AddUndo(new KeyframeCommitChange(actualRack, info));
        }

        private void OnRackKeyframeAdd(object rack, RackKeyframeAddRemoveEventArgs info)
        {
            // Rack is always type KeyframeRack
            KeyframeRack actualRack = (KeyframeRack)rack;
            History?.AddUndo(new KeyframeAddChange(actualRack, info));
        }

        protected void OnUndo(object sender, EventArgs e)
        {
            SelectedKeyframes.Clear();
        }

        protected void OnRedo(object sender, EventArgs e)
        {
            SelectedKeyframes.Clear();
        }

        /// <summary>
        /// Serializes the Timeline to JSON.
        /// </summary>
        /// <returns>A String with the Timeline's contents.</returns>
        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(KeyframeRacks, Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                SerializationBinder = new KIOTypesBinder()
            });
        }

        /// <summary>
        /// Deserializes a JSON to Timeline. Only deserializes to tracks that coincide in ID.
        /// </summary>
        /// <param name="jsonIn">JSON string.</param>
        /// <returns>True if success, false if failure.</returns>
        public bool DeserializeFromJson(string jsonIn)
        {
            Dictionary<string, KeyframeRack> jE = JsonConvert.DeserializeObject<Dictionary<string, KeyframeRack>>(
                jsonIn, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    SerializationBinder = new KIOTypesBinder()
                });

            if (jE.Count == 0) return false;
            foreach (KeyValuePair<string, KeyframeRack> kp in jE)
            {
                if (!KeyframeRacks.ContainsKey(kp.Key)) continue;
                KeyframeRacks[kp.Key].Snatch(kp.Value);
            }

            OnTimelineLoaded?.Invoke(this, EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// nuke. nuke all.
        /// </summary>
        public void WipeEverything()
        {
            foreach (KeyValuePair<string, KeyframeRack> kRack in KeyframeRacks)
            {
                kRack.Value.Wipe();
            }

            OnTimelineLoaded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sets the interpolations of every keyframe selected.
        /// </summary>
        public void SetSelectedKeyframeInterpolation(KeyframeType? interp)
        {
            if (!StagedKeyframesPresent) StageSelectedKeyframes();
            foreach (KeyValuePair<string, UncommittedRackChange> uChange in UncommittedRackChanges)
            {
                if (UncommittedRackChanges[uChange.Key] == null) continue;

                UncommittedRackChanges[uChange.Key].NewInterpolation = interp;
            }

            OnTrackheadChanged?.Invoke(this, new TrackheadChangedEventArgs(TrackheadPosition, TrackheadPosition));
        }

        /// <summary>
        /// Clears the keyframes currently marked for editing.
        /// </summary>
        public void ClearStagedKeyframes()
        {
            KeyValuePair<string, UncommittedRackChange>[] ucArray =
                UncommittedRackChanges.Cast<KeyValuePair<string, UncommittedRackChange>>().ToArray();

            for (int i = 0; i < ucArray.Length; i++)
            {
                UncommittedRackChanges[ucArray[i].Key]?.Dispose();
                UncommittedRackChanges[ucArray[i].Key] = null;
            }

            OnChangesCancelled?.Invoke(this, new ChangesCancelledEventArgs());

        }

        /// <summary>
        /// Changes the delta (position difference) for the staged keyframes by one.
        /// </summary>
        /// <param name="forward">Move forwards or backwards? Limited when any of the deltas can't move further.</param>
        public void MoveAllDeltas(bool forward)
        {
            bool canMove = UncommittedRackChanges.Where(x => x.Value != null)
                .All(x => x.Value?.CanMoveBackwards == true);
            if (!canMove) return;
            foreach (KeyValuePair<string, UncommittedRackChange> uChange in UncommittedRackChanges)
            {
                if (UncommittedRackChanges[uChange.Key] == null) continue;

                UncommittedRackChanges[uChange.Key].Delta += (forward ? 1 : -1);
            }
        }

        /// <summary>
        /// Changes the delta (position difference) for the staged keyframes to the specified value.
        /// </summary>
        /// <param name="forward">Move forwards or backwards? Limited when any of the deltas can't move further.</param>
        public void SetAllDeltas(int amount)
        {
            bool canMove = UncommittedRackChanges.Where(x => x.Value != null)
                .All(x => x.Value?.CanMoveBackwards == true);
            if (!canMove) return;
            foreach (KeyValuePair<string, UncommittedRackChange> uChange in UncommittedRackChanges)
            {
                if (UncommittedRackChanges[uChange.Key] == null) continue;

                UncommittedRackChanges[uChange.Key].Delta = amount;
            }
        }

        /// <summary>
        /// Commits all staged keyframes.
        /// </summary>
        public void CommitAllStaged()
        {
            KeyValuePair<string, UncommittedRackChange>[] ucArray =
                UncommittedRackChanges.ToArray();

            ReadOnlyDictionary<string, UncommittedRackChange> forEventChanges =
                new ReadOnlyDictionary<string, UncommittedRackChange>(UncommittedRackChanges);

            for (int i = 0; i < ucArray.Length; i++)
            {
                if (UncommittedRackChanges[ucArray[i].Key] == null) continue;

                KeyframeRacks[ucArray[i].Key].Commit(ucArray[i].Value);
                UncommittedRackChanges[ucArray[i].Key] = null;
            }

            OnChangesCommitted?.Invoke(this, new ChangesCommittedEventArgs(forEventChanges));
        }

        /// <summary>
        /// Removes all staged keyframes from their racks. Undo information: This is an operation that normally happens on only ONE rack.
        /// </summary>
        public void DeleteAllStaged()
        {
            KeyValuePair<string, UncommittedRackChange>[] ucArray =
                UncommittedRackChanges.ToArray();

            ReadOnlyDictionary<string, UncommittedRackChange> forEventChanges =
                new ReadOnlyDictionary<string, UncommittedRackChange>(UncommittedRackChanges);

            for (int i = 0; i < ucArray.Length; i++)
            {
                if (UncommittedRackChanges[ucArray[i].Key] == null) continue;

                if (History != null)
                {
                    RackKeyframeAddRemoveEventArgs info = new RackKeyframeAddRemoveEventArgs()
                    {
                        AffectedKeyframes = UncommittedRackChanges[ucArray[i].Key].Keyframes
                    };
                    History?.AddUndo(new KeyframeRemoveChange(KeyframeRacks[ucArray[i].Key], info));
                }

                foreach (Keyframe kf in UncommittedRackChanges[ucArray[i].Key].Keyframes)
                {
                    try
                    {
                        KeyframeRacks[ucArray[i].Key].Remove(kf);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Couldn't erase a keyframe from ID {ucArray[i].Key}: {e.Message}");
                    }
                }

                UncommittedRackChanges[ucArray[i].Key] = null;
            }

            OnKeyframeBulkChanged?.Invoke(this, new KeyframeBulkChangedEventArgs(forEventChanges, ChangeType.DELETION));

        }

        /// <summary>
        /// Adds a rack to this Timeline
        /// </summary>
        /// <param name="id">Rack ID</param>
        /// <param name="rack">Rack to add</param>
        public void AddRack(string id, KeyframeRack rack)
        {
            KeyframeRacks.Add(id, rack);
            rack.ChangeCommitted += OnRackCommit;
            rack.KeyframeAdded += OnRackKeyframeAdd;
            UncommittedRackChanges.Add(id, null);
            OnTracksChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Removes a rack from this Timeline. It is expected to never come back.
        /// </summary>
        /// <param name="id">Rack ID</param>
        public void RemoveRack(string id)
        {
            if (KeyframeRacks.ContainsKey(id))
            {
                KeyframeRack rack = KeyframeRacks[id];
                KeyframeRacks.Remove(id);
                rack.ChangeCommitted -= OnRackCommit;
                rack.KeyframeAdded -= OnRackKeyframeAdd;
                OnTracksChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Tells the rack by ID to add a new value at the rack.
        /// </summary>
        /// <param name="rack">Rack ID</param>
        public void AddValueToRack(string rack)
        {
            try
            {
                Keyframe kf = KeyframeRacks[rack].AddCurrentStateAtPosition(TrackheadPosition);
                OnKeyframeChanged?.Invoke(this, new KeyframeChangedEventArgs(kf, ChangeType.ADDITION, rack));
                RackKeyframeAddRemoveEventArgs info = new RackKeyframeAddRemoveEventArgs()
                {
                    AffectedKeyframes = new Keyframe[] { kf }
                };
                History?.AddUndo(new KeyframeAddChange(KeyframeRacks[rack], info));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception while trying to update rack with ID {rack}: {ex.Message}");
            }
        }

        /// <summary>
        /// Tells the rack by ID to return all keyframes within a range (inclusive)
        /// </summary>
        /// <param name="rack">Rack ID</param>
        public Keyframe[] GetKeyframesFromRackInRange(string rack, int frameStart, int frameEnd)
        {
            try
            {
                return ((IEnumerable<Keyframe>)KeyframeRacks[rack])
                    .Where(x => x.Position <= frameEnd && x.Position >= frameStart).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception while trying to select from rack ID {rack}: {ex.Message}");
                return new Keyframe[] { };
            }
        }

        public int KeyframesBeyondLimit() => KeyframeRacks.Values.Select(x => x.KeyframesBeyondLimit(Length)).Sum();

        public void ClipRacksBeyondEnd()
        {
            foreach (KeyframeRack rack in KeyframeRacks.Values)
            {
                rack.ClipAtLimit(Length);
            }
        }
        public void ScrubRacksBeyondEnd()
        {
            foreach (KeyframeRack rack in KeyframeRacks.Values)
            {
                rack.ScrubBeyondLimit(Length);
            }
        }

        public int GetImmediateKeyframePosition(bool forward)
        {
            /*  There's this meme about different programming languages rescuing a princess.
            *   C#'s parody depicted it as "trying to get the princess in a single LINQ query and then giving up"
            *   I didn't give up but the former part of that parody still rings true here lol
            *   
            *   OK, so, what this does basically is just take all the racks' immediate front (or back) keyframes,
            *   filter whether they're in front or behind of the trackhead, and select the frame number that
            *   has the least distance.
            */
            int[] _keys = KeyframeRacks.Select(x =>
                    (forward ? x.Value.ImmediateForwardKeyframe?.Position : x.Value.ImmediateBehindKeyframe?.Position))
                .Where(x => x != null && (forward ? x > TrackheadPosition : x < TrackheadPosition)).Cast<int>()
                .Distinct()
                .OrderBy(x => x).ToArray();

            if (!_keys.Any()) return TrackheadPosition;

            return (int)(forward ? _keys.First() : _keys.Last());
        }

        /// <summary>
        /// Gets an array of all generic keyframes found at the current position
        /// </summary>
        /// <returns>Array of keyframes found at the trackhead's position</returns>
        public Keyframe[] GetKeyframesAtTrackhead()
        {
            List<Keyframe> kfList = new List<Keyframe>();

            if (SelectedRacks.Count == 0)
            {
                if (ActiveRack == null) return new Keyframe[] { };
                else
                {
                    Keyframe foundKeyframe = KeyframeRacks[ActiveRack].GetKeyframeAtPosition((int)TrackheadPosition);
                    if (foundKeyframe != null) kfList.Add(foundKeyframe);
                }
            }
            else
            {
                foreach (string rack in SelectedRacks)
                {
                    Keyframe foundKeyframe = KeyframeRacks[rack].GetKeyframeAtPosition((int)TrackheadPosition);
                    if (foundKeyframe != null) kfList.Add(foundKeyframe);
                }
            }

            return kfList.ToArray();
        }

        /// <summary>
        /// Tells all the underlying Keyframe Racks to add a new keyframe at the trackhead
        /// </summary>
        public void AddAllCurrentValuesToRacks()
        {
            if (SelectedRacks.Count == 0)
            {
                if (ActiveRack == null) return;
                else
                {
                    try
                    {
                        AddValueToRack(ActiveRack);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception while trying to update rack with ID {ActiveRack}: {ex.Message}");
                    }
                }
            }
            else
            {
                foreach (string rack in SelectedRacks)
                {
                    try
                    {
                        AddValueToRack(rack);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception while trying to update rack with ID {rack}: {ex.Message}");
                    }
                }
            }
        }
    }
}
