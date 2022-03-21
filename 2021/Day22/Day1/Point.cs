using System.Collections.Generic;

namespace Day1
{

    public partial class Program
    {
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public bool Equals(Point a, Point b)
            {
                return a?.X == b?.X && a?.Y == b?.Y && a?.Z == b?.Z;
            }

            public int GetHashCode(Point point)
            {
                return point.X.GetHashCode()
                       ^ point.Y.GetHashCode();
            }

        }
    }
}

