using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{

    public partial class Program
    {

        static void Main(string[] args)
        {
            var inputs = System.IO.File.ReadAllLines(@".\input.txt").ToList();

            var pQueue = new PriorityQueue<Point, long>();

            var points = new List<Point>();

            var rows = inputs.Count;
            var cols = inputs[0].Length;

            Point end = null;

            for (int row = 0; row < inputs.Count; row++)
            {
                for (int col = 0; col < inputs[row].Length; col++)
                {
                    int risk = int.Parse(inputs[row][col].ToString());
                    
                    for (int rrow = 0; rrow < 5; rrow++)
                    {
                        for (int ccol = 0; ccol < 5; ccol++)
                        {
                            int tileRisk = (risk + rrow + ccol - 1) % 9 + 1;
                            points.Add(new Point { X = col + inputs[0].Length * ccol, Y = row + inputs.Count * rrow, Cost = tileRisk, Value = long.MaxValue });
                        }
                    }
                }
            }
            
            foreach (var point in points)
            {
                point.connections = GetNeighbours(point);
            }
            
            points.First(p => p.X == 0 && p.Y == 0).Value = 0;

            pQueue.Enqueue(points.First(), 0);

            int count = 0;

            while (pQueue.TryDequeue(out Point minDist, out long pri))
            {
                minDist.Visited = true;

                Console.WriteLine(count++);

                foreach (var neighbour in minDist.connections.Where(p => p.Visited == false))
                {
                    var newValue = minDist.Value + neighbour.Cost;

                    if (newValue < neighbour.Value)
                    {
                        neighbour.Value = newValue;
                        pQueue.Enqueue(neighbour, newValue);
                    }
                }
            }
            
            Console.WriteLine();
            Console.WriteLine(points.Last().Value);

            List<Point> GetNeighbours(Point point)
            {
                var neigbours = new List<Point>();

                int row = point.X;
                int col = point.Y;

                if (points.FirstOrDefault(p => p.X == point.X && p.Y == point.Y - 1) is { } up)
                {
                    neigbours.Add(up);
                }
                if (points.FirstOrDefault(p => p.X == point.X && p.Y == point.Y + 1) is { } down)
                {
                    neigbours.Add(down);
                }
                if (points.FirstOrDefault(p => p.X == point.X - 1 && p.Y == point.Y) is { } left)
                {
                    neigbours.Add(left);
                }
                if (points.FirstOrDefault(p => p.X == point.X + 1 && p.Y == point.Y) is { } right)
                {
                    neigbours.Add(right);
                }

                return neigbours;
            }
        }
    }
}

    


