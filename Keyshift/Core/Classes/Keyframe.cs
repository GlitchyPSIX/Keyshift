using System;
using System.ComponentModel;
using Keyshift.Core.Interfaces;

namespace Keyshift.Core.Classes
{
    public abstract class Keyframe
    {
        public int Position { get; set; }

        public KeyframeType InterpolationType { get; set; }

        public int Distance(Keyframe target)
        {
            return (int)Math.Abs(target.Position - this.Position);
        }

    }
    public class Keyframe<TKeytype> : Keyframe, IKeyframe<TKeytype>
    {

        public Keyframe<TKeytype> SubordinateOf { get; set; }
        public Keyframe<TKeytype>[] Subordinates { get; set; }
        public virtual TKeytype CurrentValue { get; set; }
        

        public Keyframe(TKeytype initialValue)
        {
            CurrentValue = initialValue;
        }

        public static implicit operator TKeytype(Keyframe<TKeytype> x)
        {
            return x.CurrentValue;
        }

        public override string ToString()
        {
            return $"{InterpolationType} Keyframe\nFrame {Position}\nValue: {CurrentValue}";
        }
    }

    public enum KeyframeType
    {
        [Description("Linear")]
        Linear,
        [Description("Fast")]
        Fast,
        [Description("Slow")]
        Slow,
        [Description("Smooth")]
        Smooth,
        [Description("Sharp")]
        Sharp,
        [Description("Hold")]
        Hold,
        [Description("Bezier")]
        Bezier,
        [Description("Random Shake")]
        Shake,
        [Description("Dependent")]
        Subordinate
    }
}
