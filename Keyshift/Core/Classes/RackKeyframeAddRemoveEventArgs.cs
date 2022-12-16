using System;

namespace Keyshift.Core.Classes
{
    public class RackKeyframeAddRemoveEventArgs : EventArgs
    {
        public Keyframe[] AffectedKeyframes;
    }
}