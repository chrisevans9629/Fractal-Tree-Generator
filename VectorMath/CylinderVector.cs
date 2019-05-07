using System.Windows.Media.Media3D;
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
}