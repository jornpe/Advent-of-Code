using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Day1
{

    class Program
    {
        static void Main(string[] args)
        {

            var map = System.IO.File.ReadAllLines(@".\input.txt");

            var pointMap = new List<MapPoint>();
            
            //Create a 2 point map
            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    pointMap.Add(new MapPoint { Point = new Point(row, col), Value = int.Parse(map[row][col].ToString()) });
                }
            }

            int score = 0;

            bool allFlashes = false;
            int step = 0;

            while(!allFlashes)
            {
                step++;
                foreach (var point in pointMap)
                {
                    point.Value++;
                }

                bool updated = true;
                while (updated)
                {
                    updated = false;

                    foreach (var point in pointMap.Where(point => point.Value == 10))
                    {
                        point.Value = 0;
                        point.LockValue = true;
                        updated = true;
                        score++;

                        //Høyre
                        if (point.Point.X > 0)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X - 1 && x.Point.Y == point.Point.Y);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Venstre
                        if (point.Point.X < map[0].Length - 1)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X + 1 && x.Point.Y == point.Point.Y);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Opp
                        if (point.Point.Y > 0)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.Y == point.Point.Y - 1 && x.Point.X == point.Point.X);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Ned
                        if (point.Point.Y < map.Length - 1)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.Y == point.Point.Y + 1 && x.Point.X == point.Point.X);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Opp-venste
                        if (point.Point.X > 0 && point.Point.Y > 0)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X - 1 && x.Point.Y == point.Point.Y - 1);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Opp-Høyre
                        if (point.Point.X < map[0].Length - 1 && point.Point.Y > 0)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X + 1 && x.Point.Y == point.Point.Y - 1);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }
                        //Ned venste
                        if (point.Point.X > 0 && point.Point.Y < map.Length - 1)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X - 1 && x.Point.Y == point.Point.Y + 1);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }

                        //Ned Høyre
                        if (point.Point.X < map[0].Length - 1 && point.Point.Y < map.Length - 1)
                        {
                            var p = pointMap.FirstOrDefault(x => x.Point.X == point.Point.X + 1 && x.Point.Y == point.Point.Y + 1);

                            if (p is { Value: < 10 })
                            {
                                p.Value++;
                            }
                        }
                    }

                    if (pointMap.All(p=>p.Value == pointMap[0].Value))
                    {
                        allFlashes = true;
                    }

                }

                

                foreach (var point in pointMap.Where(point => point.LockValue))
                {
                    point.LockValue = false;
                }
            }
            
            Console.WriteLine(step);
        }

        public class MapPoint
        {
            private int _value;
            public Point Point { get; set; }

            public int Value
            {
                get => _value;
                set
                {
                    if (!LockValue)
                    {
                        _value = value;
                    }
                }
            }

            public bool LockValue { get; set; }
        }
    }
}

