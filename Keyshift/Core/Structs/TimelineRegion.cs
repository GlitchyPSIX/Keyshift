using System;

namespace Keyshift.Core.Structs
{
    public class TimelineRegion : ICloneable
    {
        public string Name { get; set; }
        private int _start, _end;
        public int FrameStart {
            get => Math.Max(0, Math.Min(_start, _end));
            set => _start = value;
        }

        public int FrameEnd {
            get => Math.Max(0, Math.Max(_start, _end));
            set => _end = value;
        }

        public int Length => FrameEnd - FrameStart;

        public bool IsPositionInsideRegion(int position) {
            if (position < 0) return false;
            if (Length == 0) return false;

            return position >= FrameStart && position <= FrameEnd;
        }


        public object Clone() {
            return new TimelineRegion() {
                Name = Name,
                FrameStart = _start,
                FrameEnd = _end
            };
        }
    }
}
