using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day1
{

    class Program
    {
        static void Main(string[] args)
        {

            var lines = System.IO.File.ReadAllLines(@".\input.txt");

            long points = 0;

            //Part 1
            foreach (var line in lines)
            {
                var tempLine = line;
                var oldLine = "";

                while (!oldLine.Equals(tempLine))
                {
                    oldLine = tempLine;
                    tempLine = tempLine
                        .Replace("()", "")
                        .Replace("[]", "")
                        .Replace("{}", "")
                        .Replace("<>", "");
                }
                tempLine = tempLine
                    .Replace("(", "")
                    .Replace("[", "")
                    .Replace("{", "")
                    .Replace("<", "");

                if (tempLine.Length > 0)
                {
                    points += tempLine[0] switch
                    {
                        ')' => 3,
                        ']' => 57,
                        '}' => 1197,
                        '>' => 25137,
                        _ => 0
                    };

                }

            }
            Console.WriteLine($"Points for part 1 {points}");

            //Part 2
            points = 0;
            var scoreBoard = new List<long>();

            foreach (var line in lines)
            {
                var tempLine = line;
                var oldLine = "";

                while (!oldLine.Equals(tempLine))
                {
                    oldLine = tempLine;
                    tempLine = tempLine
                        .Replace("()", "")
                        .Replace("[]", "")
                        .Replace("{}", "")
                        .Replace("<>", "");
                }
                oldLine = tempLine
                    .Replace("(", "")
                    .Replace("[", "")
                    .Replace("{", "")
                    .Replace("<", "");
                

                if (!string.IsNullOrWhiteSpace(oldLine)) continue;

                foreach (var c in tempLine.Reverse())
                {
                    points *= 5;
                    points += c switch
                    {
                        '(' => 1,
                        '[' => 2,
                        '{' => 3,
                        '<' => 4,
                        _ => 0
                    };
                }

                scoreBoard.Add(points);
                points = 0;

            }

            scoreBoard.Sort();


            Console.WriteLine($"Points for part 2: {scoreBoard[scoreBoard.Count/2]}");


        }
    }
}

