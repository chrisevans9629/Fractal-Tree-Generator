using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using BasicVector;
using HelixToolkit.Wpf;

namespace VectorMath
{
    public class CylinderVector
    {
        private readonly HelixViewport3D _viewport3D;
        private readonly Vector3D _begin;
        private readonly Vector3D _end;

        public RectangleVisual3D CreateCube()
        {
            var diff = _end - _begin;
           var cube = new RectangleVisual3D();
            cube.Origin = _begin.ToPoint3D();
            cube.Length = 100;
            cube.Width = 100;
            cube.Material = Materials.DarkGray;
            
            return cube;
        }
        public void Draw()
        {
            _viewport3D.Children.Add(CreateCube());
        }
        public CylinderVector(HelixViewport3D viewport3D, Vector3D begin, Vector3D end)
        {
            _viewport3D = viewport3D;
            _begin = begin;
            _end = end;
            
        }
    }
    public static class VectorMath
    {
        public static Vector Abs(this Vector a)
        {
            return new Vector(Math.Abs(a.X), Math.Abs(a.Y));
        }
    }
    public class LineVector
    {
        public double StrokeThickness = 1;
       static Random r = new Random();
        public static implicit operator Line(LineVector a)
        {
            Color randomColor = new Color();
            randomColor.A = 255; //alpha channel of the color
            randomColor.R = (byte)r.Next(0, 10); //red channel
            randomColor.G = (byte)r.Next(0, 255); //green channel
            randomColor.B = (byte)r.Next(0, 50);
            var line = a._line;
            line.X1 = a._begin.X;
            line.Y1 = a._begin.Y;
            line.X2 = a._end.X;
            line.Y2 = a._end.Y;
            line.Stroke = new SolidColorBrush(randomColor);
            line.StrokeThickness = a.StrokeThickness;
            line.StrokeEndLineCap = PenLineCap.Round;
            return line;
        }

        public static implicit operator LineVector(Line a)
        {
            var canvas = a.Parent as Canvas;
            if (canvas != null)
            {
                return new LineVector(canvas, new Vector(a.X1,a.Y1), new Vector(a.X2, a.Y2) );
            }
            return null;
        }
        private readonly Canvas _grid;
        private readonly Vector _begin;
        private readonly Vector _end;
        private Line _line;

        public LineVector(HelixViewport3D viewport3D, Vector begin, Vector end)
        {
            _line = new Line();
            _begin = begin;
            _end = end;
        }
        public LineVector(Canvas grid, Vector begin, Vector end)
        {
            _line = new Line();
            _grid = grid;
            _begin = begin;
            _end = end;
        }
        Random random = new Random();
        public double LengthTolerance = 2;
        public double Angle = Math.PI / 4;
        public double LengthChange = 0.63;
        public LineVector Left()
        {

            Vector vect;
            if (Init(out vect)) return null;
            var rot = VectorUtil.Rotate(vect, Angle * RandomVariation());
            var newvect = _end + rot;
            //Draw(_end, newvect);
            return new LineVector(_grid, _end, newvect);
        }
        public int VariationMin = 60;
        public int VariationMax = 99;
        private double RandomVariation()
        {
            return Convert.ToDouble(random.Next(VariationMin, VariationMax)) / 100;
        }

        public LineVector Right()
        {
            Vector vect;
            if (Init(out vect)) return null;
            var rot = VectorUtil.Rotate(vect, -Angle  * RandomVariation());
            var newvect = _end + rot;
            //Draw(_end, newvect);
            
            return new LineVector(_grid, _end, newvect);

        }

        private bool Init(out Vector vect)
        {
            vect = _end - _begin;
            vect = VectorUtil.SetLength(vect, vect.Length * LengthChange * RandomVariation());
            if (vect.Length < LengthTolerance)
            {
                return true;
            }
            return false;
        }

        public int slowdown = 10;
        public bool MultiThread = true;
        private bool finished = false;

        public async Task Draw()
        {
            Line line = this;
            _grid.Children.Add(line);
            var len = _end - _begin;
            var step = len / slowdown;
            var i = Vector.Zero;
            CompositionTarget.Rendering += (sender, args) =>
            {
                if ((i.Abs() - len.Abs()).Length > 2)
                {
                    i += step;
                    var place = _begin + i;
                    line.X2 = place.X;
                    line.Y2 = place.Y;
                }
                else
                {
                    finished = true;
                }
            };
           await Task.Factory.StartNew(() =>
            {
                if (MultiThread != true)
                {
                    //Thread.Sleep(1);
                    while (finished != true)
                    {

                    }
                }
               
            });
        }

       
    }
}