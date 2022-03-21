using System.Collections.Generic;

namespace Day1
{

    public partial class Program
    {
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Cost { get; set; }
            public long Value { get; set; }

            public List<Point> connections { get; set; }

            public bool Visited { get; set; }

            public bool Equals(Point a, Point b)
            {
                return a?.X == b?.X;
            }

            public int GetHashCode(Point point)
            {
                return point.X.GetHashCode()
                       ^ point.Y.GetHashCode();
            }

        }
    }
}

