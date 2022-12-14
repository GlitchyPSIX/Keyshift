using Keyshift.Core.Classes;

namespace Keyshift.Core.Structs {
    public struct RackCommitInfo {
        public UncommittedRackChange ChangePerformed;
        public Keyframe[] Collisions;
    }
}