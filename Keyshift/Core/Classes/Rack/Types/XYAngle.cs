using System;
using System.Numerics;

namespace Keyshift.Core.Classes.Rack.Types
{
    public struct XYAngle : IEquatable<XYAngle>
    {
        public const float DEG_TO_RAD = 0.01745329251f;
        private float _x, _y;

        public float UnrestrictedX
        {
            get => _x;
            set => value = _x;
        }

        public float UnrestrictedY
        {
            get => _y;
            set => value = _y;
        }
        public float X
        {
            get => _x;
            set
            {
                _x = value;
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                _y = value;
            }
        }

        public XYAngle(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public static XYAngle FromVectorPair(Vector3 start, Vector3 lookat)
        {
            Vector3 origVec = lookat - start;
            if (origVec.Z == 0) origVec.Z = 0.001f;
                origVec /= origVec.Length();
            float distance = Vector3.Distance(start, lookat);
            return new XYAngle(
                (float) (Math.Atan2(origVec.X, origVec.Z)) * 57.2958f,
                (float) (Math.Asin(origVec.Y)) * 57.2958f
                );
        }

        public bool Equals(XYAngle other)
        {
            return (_x == other.X && _x == other.Y);
        }

        public static XYAngle Lerp(XYAngle start, XYAngle end, float progress)
        {
            return ((1 - progress) * start) + progress * end;
        }

        public static XYAngle operator +(XYAngle a, XYAngle b)
        {
            return new XYAngle(a.UnrestrictedX + b.UnrestrictedX, a.UnrestrictedY + b.UnrestrictedY);
        }

        public static XYAngle operator -(XYAngle a, XYAngle b)
        {
            return new XYAngle(a.UnrestrictedX - b.UnrestrictedX, a.UnrestrictedY - b.UnrestrictedY);
        }

        public static bool operator ==(XYAngle a, XYAngle b)
        {
            return (a.UnrestrictedX == b.UnrestrictedX && a.UnrestrictedY == b.UnrestrictedY);
        }

        public static bool operator !=(XYAngle a, XYAngle b)
        {
            return (a.X != b.X && a.Y != b.Y);
        }

        public static XYAngle operator *(float scalar, XYAngle a)
        {
            return a * scalar;
        }

        public static XYAngle operator *(XYAngle a, float scalar)
        {
            return new XYAngle(a.UnrestrictedX * scalar, a.UnrestrictedY * scalar);
        }

        public Vector3 LookAtFromPosition(Vector3 start, float length = 200)
        {
            Vector3 ang = new Vector3(
                (float) (Math.Sin(UnrestrictedX * DEG_TO_RAD) * Math.Cos(-UnrestrictedY * DEG_TO_RAD)) * length,
                (float) (Math.Sin(UnrestrictedY * DEG_TO_RAD)) * length,
                (float) (Math.Cos(UnrestrictedX * DEG_TO_RAD) * Math.Cos(UnrestrictedY * DEG_TO_RAD)) * length);

            return ang + start;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"<{X}º, {Y}º>";
        }
    }
}
