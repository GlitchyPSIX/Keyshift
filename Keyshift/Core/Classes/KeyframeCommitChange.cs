using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes
{
    public class KeyframeCommitChange : IReversibleChange {
        private KeyframeRack _affectedRack;
        private RackCommitEventArgs _change;
        public KeyframeRack AffectedRack => _affectedRack;
        public RackCommitEventArgs Changes => _change;
        public KeyframeCommitChange(KeyframeRack affectedRack, RackCommitEventArgs changes) {
            _affectedRack = affectedRack;
            _change = changes;
        }

        

        public void Redo() {
            AffectedRack.ReCommit(_change.ChangePerformed);
        }

        public void Undo() {
            AffectedRack.Revert(_change);
        }

        public override string ToString() {
            return $"{_change.ChangePerformed}{(_change.Collisions.Length > 0 ? $"\n({_change.Collisions.Length} overwritten keyframes)" : "")}";
        }
    }
}