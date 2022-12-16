using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes {
    public class KeyframeRemoveChange : IReversibleChange {
        private KeyframeRack _affectedRack;
        public KeyframeRack AffectedRack => _affectedRack;

        private RackKeyframeAddRemoveEventArgs _affectedKeyframes;
        public RackKeyframeAddRemoveEventArgs AffectedKeyframes => _affectedKeyframes;

        public KeyframeRemoveChange(KeyframeRack affectedRack, RackKeyframeAddRemoveEventArgs affectedKeyframes) {
            _affectedRack = affectedRack;
            _affectedKeyframes = affectedKeyframes;
        }

        public void Undo()
        {
            foreach (Keyframe kf in _affectedKeyframes.AffectedKeyframes)
            {
                _affectedRack.ReAdd(kf);
            }
        }

        public void Redo()
        {
            foreach (Keyframe kf in _affectedKeyframes.AffectedKeyframes)
            {
                _affectedRack.Remove(kf);
            }
        }

        public override string ToString()
        {
            return $"Remove {_affectedKeyframes.AffectedKeyframes.Length} keyframes";
        }
    }
}
