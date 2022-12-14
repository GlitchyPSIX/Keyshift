using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Keyshift.Core.Classes;

namespace Keyshift.Forms.Controls.VisualAid {
    public static class KeyframeShapes {
        private static bool _initialized = false;

        private static GraphicsPath linearKeyframePath,
            holdKeyframePath,
            slowKeyframePath,
            fastKeyframePath,
            smoothKeyframePath,
            sharpKeyframePath,
            bezierKeyframePath,
            shakeKeyframePath;

        public static GraphicsPath LinearKeyframePath { get
            {
                if (!_initialized) {
                    Initialize();
                }
                return linearKeyframePath;
            }
        }

        public static GraphicsPath HoldKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return holdKeyframePath;
            }
        }

        public static GraphicsPath SlowKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return slowKeyframePath;
            }
        }

        public static GraphicsPath FastKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return fastKeyframePath;
            }
        }

        public static GraphicsPath SmoothKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return smoothKeyframePath;
            }
        }

        public static GraphicsPath SharpKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return sharpKeyframePath;
            }
        }

        public static GraphicsPath BezierKeyframePath
        {
            get
            {
                if (!_initialized)
                {
                    Initialize();
                }
                return bezierKeyframePath;
            }
        }

        private static void Initialize() {
            linearKeyframePath = new GraphicsPath();
            linearKeyframePath.StartFigure();
            linearKeyframePath.AddPolygon(new PointF[] {
                new(12, 2),
                new(2, 12),
                new(12, 22),
                new( 22, 12)
            });
            linearKeyframePath.CloseFigure();

            holdKeyframePath = new GraphicsPath();
            holdKeyframePath.StartFigure();
            holdKeyframePath.AddRectangle(new(2,2,20,20));
            holdKeyframePath.CloseFigure();

            smoothKeyframePath = new GraphicsPath();
            smoothKeyframePath.StartFigure();
            smoothKeyframePath.AddPolygon(new PointF[] {
                new(2, 2),
                new(22,2),
                new(22,6),
                new(16f, 12f),
                new(22, 18),
                new(22, 22),
                new(2, 22),
                new(2, 18),
                new(7.5f, 12f),
                new(2, 6)
            });
            smoothKeyframePath.CloseFigure();

            sharpKeyframePath = new GraphicsPath();
            sharpKeyframePath.StartFigure();
            sharpKeyframePath.AddPolygon(new PointF[] {
                new(2, 2),
                new(22,2),
                new(22,6),
                new(16f, 6),
                new(22, 12),
                new(16f, 18),
                new(22, 18),
                new(22, 22),
                new(2, 22),
                new(2, 18),
                new(8, 18f),
                new(2, 12),
                new(8, 6f),
                new(2, 6)
            });
            sharpKeyframePath.CloseFigure();

            fastKeyframePath = new GraphicsPath();
            fastKeyframePath.StartFigure();
            fastKeyframePath.AddEllipse(2,2,20,20);
            fastKeyframePath.CloseFigure();

            slowKeyframePath = new GraphicsPath();
            slowKeyframePath.StartFigure();
            slowKeyframePath.AddLines(new PointF[] {
                new(2, 2),
                new(22, 2),
                new(22, 5)
            });
            slowKeyframePath.AddArc(15, 5, 14, 14, -90 ,-180);
            slowKeyframePath.AddLines(new PointF[] {
                new(22, 22),
                new(2, 22),
            });
            slowKeyframePath.AddArc(-5, 5, 14, 14, -90*3, -180);
            slowKeyframePath.CloseFigure();
        }

        public static Dictionary<KeyframeType, VectorShape> Shapes = new() {
            {KeyframeType.Linear, new VectorShape() {
                Path = LinearKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.LightGray
            }},
            {KeyframeType.Hold, new VectorShape() {
                Path = HoldKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.LightCoral
            }},
            {KeyframeType.Slow, new VectorShape() {
                Path = SlowKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.LightSalmon
            }},
            {KeyframeType.Fast, new VectorShape() {
                Path = FastKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.LightYellow
            }},
            {KeyframeType.Smooth, new VectorShape() {
                Path = SmoothKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.LightSteelBlue
            }},
            {KeyframeType.Sharp, new VectorShape() {
                Path = SharpKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.CornflowerBlue
            }},
            {KeyframeType.Bezier, new VectorShape() {
                Path = BezierKeyframePath,
                Pen = new Pen(Color.Black, 2.5f),
                Brush = Brushes.DodgerBlue
            }},

        };
    }

    public struct VectorShape {
        public GraphicsPath Path;
        public Pen Pen;
        public Brush Brush;
    }
}