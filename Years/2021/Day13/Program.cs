using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day1
{

    public class Program
    {

        static void Main(string[] args)
        {
            var points = new List<Point>();
            var folds = new List<Fold>();

            var inputs = System.IO.File.ReadAllLines(@".\input.txt");
            

            foreach (var input in inputs)
            {
                if (input.Contains('='))
                {
                    var f = input.Split('=');
                    folds.Add(new Fold { Dir = f[0].Last().ToString(), Index = int.Parse(f[1]) });
                }
                else if (input.Contains(','))
                {
                    var p = input.Split(',');
                    points.Add(new Point{ X= int.Parse(p[0]), Y = int.Parse(p[1])});
                }
            }


            foreach (var fold in folds)    
            {
                if (fold.Dir.ToLower() == "x")
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (points[i].X > fold.Index)
                        {
                            points[i].X = fold.Index - (points[i].X - fold.Index);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (points[i].Y > fold.Index)
                        {
                            points[i].Y = fold.Index - (points[i].Y - fold.Index);
                        }
                    }
                }
                points = points.GroupBy(x => x, new PointComparer()).Select(p => p.Key).ToList();

                Console.WriteLine(points.GroupBy(x => x).Count());

                
            }
            int maxX = points.Select(p => p.X).Max();
            int maxY = points.Select(p => p.Y).Max();

            for (int i = 0; i <= maxY; i++)
            {
                string line = "";
                for (int j = 0; j <= maxX; j++)
                {
                    var point = points.FirstOrDefault(p => p.X == j && p.Y == i);

                    line += (point is null)
                        ? " "
                        : "#";
                }

                Console.WriteLine(line);
            }


        }

        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

        }

        public class Fold
        {
            public string Dir { get; set; }
            public int Index { get; set; }

        }

        class PointComparer : IEqualityComparer<Point>
        {
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

