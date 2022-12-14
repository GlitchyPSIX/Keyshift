using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Keyshift.Core.Classes.Rack.Types;

namespace Keyshift.Core.Extensions
{
    class MathExtensions
    {
        public static Vector3 Lerp(Vector3 start, Vector3 end, float progress) =>
            ((1 - progress) * start) + progress * end;

        public static float Lerp(float start, float end, float progress) =>
            ((1 - progress) * start) + progress * end;

        public static float BezierInterpolate(float[] points, float progress)
        {
            float[] centers = new float[points.Length];
            Array.Copy(points, centers, points.Length);

            while (centers.Length > 1)
            {
                float[] newCenters = new float[centers.Length - 1];
                for (int i = 0; i < centers.Length - 1; i++)
                {
                    newCenters[i] = Lerp(centers[i], centers[i + 1], progress);
                }

                centers = newCenters;
            }

            return centers[0];
        }

        public static Vector3 BezierInterpolate(Vector3[] points, float progress)
        {
            Vector3[] centers = new Vector3[points.Length];
            Array.Copy(points, centers, points.Length);

            while (centers.Length > 1)
            {
                Vector3[] newCenters = new Vector3[centers.Length - 1];
                for (int i = 0; i < centers.Length - 1; i++)
                {
                    newCenters[i] = Lerp(centers[i], centers[i + 1], progress);
                }

                centers = newCenters;
            }

            return centers[0];
        }

        public static XYAngle BezierInterpolate(XYAngle[] points, float progress)
        {
            XYAngle[] centers = new XYAngle[points.Length];
            Array.Copy(points, centers, points.Length);

            while (centers.Length > 1)
            {
                XYAngle[] newCenters = new XYAngle[centers.Length - 1];
                for (int i = 0; i < centers.Length - 1; i++)
                {
                    newCenters[i] = XYAngle.Lerp(centers[i], centers[i + 1], progress);
                }

                centers = newCenters;
            }

            return centers[0];
        }

        public static List<PointF> vec3ToPointFs(Vector3[] _in)
        {
            List<PointF> list = new List<PointF>();
            foreach (Vector3 v3 in _in)
            {
                list.Add(new PointF(v3.X, v3.Y));
            }

            return list;
        }
    }
}
