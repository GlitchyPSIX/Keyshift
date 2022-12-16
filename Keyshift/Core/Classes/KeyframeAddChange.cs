using System;
using Keyshift.Core.Interfaces;
using Keyshift.Core.Structs;

namespace Keyshift.Core.Classes {
    public class KeyframeAddChange : IReversibleChange {
        private KeyframeRack _affectedRack;
        public KeyframeRack AffectedRack => _affectedRack;
        
        private RackKeyframeAddRemoveEventArgs _affectedKeyframes;
        public RackKeyframeAddRemoveEventArgs AffectedKeyframes => _affectedKeyframes;

        public KeyframeAddChange(KeyframeRack affectedRack, RackKeyframeAddRemoveEventArgs affectedKeyframes)
        {
            _affectedKeyframes = affectedKeyframes;
            _affectedRack = affectedRack;
        }

        public void Undo() {
            foreach (Keyframe kf in _affectedKeyframes.AffectedKeyframes) {
                _affectedRack.Remove(kf);
            }
        }

        public void Redo() {
            foreach (Keyframe kf in _affectedKeyframes.AffectedKeyframes)
            {
                _affectedRack.ReAdd(kf);
            }
        }

        public override string ToString() {
            return $"Add {_affectedKeyframes.AffectedKeyframes.Length} keyframes";
        }
    }
}