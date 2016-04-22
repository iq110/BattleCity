using System;
using System.Drawing;

namespace Model.Entity.AnimationPac
{
    public static class ExtendedPoint
    {
        public static PointF Substract(this PointF start, PointF finish)
        {
            return new PointF(finish.X - start.X, finish.Y - start.Y);
        }
        public static PointF Normalize(this PointF point)
        {
            double distance = GetLenght(point);
            return new PointF((float)(point.X / distance), (float)(point.Y / distance));
        }
        public static PointF Addition(this PointF p1, PointF p2)
        {
            return new PointF(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static PointF Multiplication(this PointF p1, float x)
        {
            return new PointF(p1.X * x, p1.Y * x);
        }

        public static PointF GetDirectionVector(this PointF start, PointF destination)
        {
            PointF substraction = destination.Substract(start);

            return substraction.Normalize(); // <<<< I need something for this
        }
        public static float GetAngle(this PointF one, PointF two)
        {

            double dist1 = GetLenght(one);
            double dist2 = GetLenght(two);

            double cos = Scalar(one, two) / (dist1 * dist2);
            if (two.Y <= one.Y)
                return (float)(Math.Acos(cos) * 180 / Math.PI);
            else
            {
                double d = 180 - Math.Acos(cos) * 180 / Math.PI;
                return (float)(180 + d);
            }
        }

        public static double GetLenght(this PointF point)
        {
            double distance = Math.Sqrt(point.X * point.X + point.Y * point.Y);
            return distance;
        }

        public static double Scalar(this PointF p1, PointF p2)
        {
            double result = p1.X * p2.X + p1.Y * p2.Y;
            return result;
        }

    }
}