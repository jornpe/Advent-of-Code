using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day1
{

    class Program
    {
        static void Main(string[] args)
        {

            var map = System.IO.File.ReadAllLines(@".\input.txt");

            var lowPoints = new List<int>();
            var basins = new List<int>();

            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (col != 0 && map[row][col] >= map[row][col - 1])
                    {
                        continue;
                    }
                    if (col != map[row].Length - 1 && map[row][col] >= map[row][col + 1])
                    {
                        continue;
                    }
                    if (row != 0 && map[row][col] >= map[row - 1][col])
                    {
                        continue;
                    }
                    if (row != map.Length - 1 && map[row][col] >= map[row + 1][col])
                    {
                        continue;
                    }

                    lowPoints.Add(int.Parse(map[row][col].ToString()));

                    basins.Add(GetBasinNeigbour(map, new List<Point>(), row, col));

                }
            }

            var risk = lowPoints.Aggregate(lowPoints.Count, (a, b) => a + b);
            
            var part2 = basins.OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b);

            Console.WriteLine(risk);
        }


        public static int GetBasinNeigbour(string[] map, List<Point> basin, int row, int col)
        {
            basin.Add(new Point(row, col));

            if (col != 0 && map[row][col - 1] != '9' && !basin.Contains(new Point(row, col - 1)))
            {
                GetBasinNeigbour(map, basin, row, col - 1);
            }
            if (col != map[row].Length - 1 && map[row][col + 1] != '9' && !basin.Contains(new Point(row, col + 1)))
            {
                GetBasinNeigbour(map, basin, row, col + 1);
            }
            if (row != 0 && map[row - 1][col] != '9' && !basin.Contains(new Point(row - 1, col)))
            {
                GetBasinNeigbour(map, basin, row - 1, col);
            }
            if (row != map.Length - 1 && map[row + 1][col] != '9' && !basin.Contains(new Point(row + 1, col)))
            {
                GetBasinNeigbour(map, basin, row + 1, col);
            }

            return basin.Count;
        }

    }
}

