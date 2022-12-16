using System;

namespace Keyshift.Core.Classes
{
    public class RackCommitEventArgs : EventArgs
    {
        public UncommittedRackChange ChangePerformed;
        public Keyframe[] Collisions;
    }
}