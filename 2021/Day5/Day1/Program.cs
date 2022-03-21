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

            var inputs = System.IO.File.ReadLines(@".\input.txt").ToList();

            Day5 test = new Day5();
            var solution = test.SolvePart2(System.IO.File.ReadAllText(@".\input.txt"));

            var lines = new List<Line>();  
            
            foreach (var line in inputs)
            {
                lines.Add(new Line(line));
            }

            var VorHpoints = new List<Point>();

            foreach (var line in lines)
            {
                if (line.Point1.X == line.Point2.X)
                {
                    foreach (var numb in GetorderedNumbers(line.Point1.Y, line.Point2.Y))
                    {
                        VorHpoints.Add(new Point(line.Point1.X, numb));
                    }
                }
                else if (line.Point1.Y == line.Point2.Y)
                {
                    foreach (var numb in GetorderedNumbers(line.Point1.X, line.Point2.X))
                    {
                        VorHpoints.Add(new Point(numb, line.Point1.Y));
                    }
                }
                else if (Math.Abs(line.Point1.X - line.Point2.X) == Math.Abs(line.Point1.Y - line.Point2.Y))
                {
                    VorHpoints.AddRange(GetdiagonalPoints(line));
                }
            }

            Console.WriteLine(VorHpoints.GroupBy(x => x).Count(x => x.Count() > 1));
        }

        public class Day5
        {
            public string SolvePart1(string input) => Solve(input, false);
            public string SolvePart2(string input) => SolveSteps(input, true);

            string Solve(string input, bool includeDiagonals) =>
                input
                .Split("\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s =>
                    s.Split(" -> "))
                .Select(a =>
                    (x1(a), y1(a), x2(a), y2(a)))
                .Where(t => includeDiagonals || t.Item1 == t.Item3 || t.Item2 == t.Item4)
                .SelectMany(t => Enumerable
                    .Range(0, Math.Max(Math.Abs((int)(t.Item1 - t.Item3)), Math.Abs((int)(t.Item2 - t.Item4))) + 1)
                    .Select(i => (
                        t.Item1 > t.Item3 ? t.Item3 + i : t.Item1 < t.Item3 ? t.Item3 - i : t.Item3,
                        t.Item2 > t.Item4 ? t.Item4 + i : t.Item2 < t.Item4 ? t.Item4 - i : t.Item4)))
                .GroupBy(k => k)
                .Count(k => Enumerable.Count(k) >= 2)
                .ToString();

            string SolveSteps(string input, bool includeDiagonals)
            {

                var step1 = input.Split("\n");
                var step2 = step1.Where(s => !string.IsNullOrEmpty(s));
                var step3 = step2.Select(s => s.Split(" -> "));
                var step33 = step3.Select(a => (x1(a), y1(a), x2(a), y2(a)));

                var step4 = step33.Where(t => includeDiagonals || t.Item1 == t.Item3 || t.Item2 == t.Item4);
                var step5 = step4.SelectMany(t => Enumerable
                    .Range(0, Math.Max(Math.Abs((int)(t.Item1 - t.Item3)), Math.Abs((int)(t.Item2 - t.Item4))) + 1)
                    .Select(i => (
                        t.Item1 > t.Item3 ? t.Item3 + i : t.Item1 < t.Item3 ? t.Item3 - i : t.Item3,
                        t.Item2 > t.Item4 ? t.Item4 + i : t.Item2 < t.Item4 ? t.Item4 - i : t.Item4)));
                var step6 = step5.GroupBy(k => k);
                var step7 = step6.Count(k => Enumerable.Count(k) >= 2);
                return step7.ToString();
            }


            private int x1(string[] a) => int.Parse(a[0].Split(",")[0]);
            private int y1(string[] a) => int.Parse(a[0].Split(",")[1]);
            private int x2(string[] a) => int.Parse(a[1].Split(",")[0]);
            private int y2(string[] a) => int.Parse(a[1].Split(",")[1]);
        }

        private static List<Point> GetdiagonalPoints(Line line)
        {
            var points = new List<Point>();

            var xCoordinates = GetorderedNumbers(line.Point1.X, line.Point2.X);
            var yCoordinates = GetorderedNumbers(line.Point1.Y, line.Point2.Y);

            for (int i = 0; i < xCoordinates.Count; i++)
            {
                points.Add(new Point(xCoordinates[i], yCoordinates[i]));
            }

            return points;
        }

        public static List<int> GetorderedNumbers(int start, int end)
        {
            if (start > end)
            {
                var temp = end;
                end = start + 1;
                start = temp;

                return Enumerable
                .Range(start, end - start)
                .OrderBy(x => x)
                .ToList();
            }
            else
            {
                end++;
                return Enumerable
                .Range(start, end - start)
                .OrderByDescending(x => x)
                .ToList();
            }
        }
    }

    public class Line 
    {

        public Line(string line)
        {
            var points = line.Replace(" -> ", ",").Split(',');

            Point1 = new Point(int.Parse(points[0]), int.Parse(points[1]));
            Point2 = new Point(int.Parse(points[2]), int.Parse(points[3]));
        }

        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
    }
}
