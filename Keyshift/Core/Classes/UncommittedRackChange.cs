using System;
using System.Collections.Generic;
using System.Linq;

namespace Keyshift.Core.Classes
{
    public class UncommittedRackChange : IDisposable
    {
        private Keyframe[] _affectedKeyframes;
        private Dictionary<Keyframe, KeyframeType> _oldInterpolations;

        public Keyframe[] Keyframes => _affectedKeyframes;
        public Dictionary<Keyframe, KeyframeType> OldInterpolations => _oldInterpolations;

        public KeyframeType? NewInterpolation { get; set; }

        private int _delta;

        /// <summary>
        /// By how much would the affected keyframes move? Limited to the very beginning of the Timeline relative to the first keyframe.
        /// </summary>
        public int Delta
        {
            get => _delta;
            set
            {
                if (value + EarliestFrame < 0)
                {
                    _delta = -EarliestFrame;
                }
                else
                {
                    _delta = value;
                }
            }
        }

        public UncommittedRackChange(Keyframe[] toChange)
        {
            _affectedKeyframes = toChange;
            _oldInterpolations = _affectedKeyframes
                .Select(x => x)
                .ToDictionary(x => x, x => x.InterpolationType);
        }

        public bool CanMoveBackwards => (Delta + EarliestFrame) >= 0;

        public int EarliestFrame => (int)_affectedKeyframes.Select(x => x.Position).Min();

        public void Dispose()
        {
            _affectedKeyframes = new Keyframe[]{};
        }

        public override string ToString() {
            string move =
                $"Move {_affectedKeyframes.Length} keyframes {(_delta > 0 ? "forward" : "backward")} by {Math.Abs(_delta)} frames";
            string interp = $"Change {_affectedKeyframes.Length} keyframes to {NewInterpolation} interpolation.";
            return $"Change keyframes:\n{(Delta == 0 ? "No movement" : move)}, {(NewInterpolation == null ? "No interpolation change" : interp)}";
        }
    }
}
