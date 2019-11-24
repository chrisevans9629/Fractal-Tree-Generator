using System;
using System.Collections.Generic;
using BasicVector;

namespace VectorMath
{
    public abstract class Angle
    {
        public double Value { get; set; }

        public static implicit operator double(Angle v)
        {
            return v.Value;
        }
        
    }
    public class DegreeAngle : Angle
    {
        public DegreeAngle(double angle)
        {
            if(angle > 360 || angle < 0)
                throw new ArgumentOutOfRangeException(nameof(angle), angle.ToString());
            Value = angle;
        }

    }
    public class RadianAngle : Angle
    {
        public RadianAngle(double angle)
        {
            if(angle > Math.PI * 2 || angle < 0)
                throw new ArgumentOutOfRangeException(nameof(angle),angle.ToString());
            Value = angle;
        }
    }


    public class PenLine
    {
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
    }

    public class PenTool
    {

        public List<PenLine> Lines { get; set; } = new List<PenLine>();

        private Vector _vector = Vector.Zero;

        private RadianAngle radianAngle = new RadianAngle(0);
        public void MoveTo(double x, double y)
        {
            _vector = new Vector(x, y);
        }

        public RadianAngle ToRadian(DegreeAngle angle)
        {
            return new RadianAngle(angle / 57.2957795);
        }

        public void Rotate(DegreeAngle angle)
        {
            var radian = ToRadian(angle);
            radianAngle = radian; 
        }

        public void Rotate(RadianAngle angle)
        {
            radianAngle = angle;
        }

        public PenLine Draw(double length)
        {
            var drawVector = VectorUtil.Rotate(new Vector(length, 0), radianAngle);
            var line = new PenLine { X1 = _vector.X, Y1 = _vector.Y, X2 = drawVector.X, Y2 = drawVector.Y};
            Lines.Add(line);
            return line;
        }
    }
}