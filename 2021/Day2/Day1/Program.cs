using System;
using System.Collections.Generic;
using System.Linq;
using static System.Enum;

namespace Day1
{
    enum Direction
    {
        forward,
        down, 
        up
    }

    class Program
    {
        static void Main(string[] args)
        {
            int downwards = 0;
            int forwards = 0;
            int aim = 0;

            foreach (var line in System.IO.File.ReadLines(@".\input.txt"))
            {
                var splittedLine = line.Split(' ');
                _ = TryParse(splittedLine[0], out Direction dir);
                var pair = new KeyValuePair<Direction, int>(dir, Convert.ToInt16(splittedLine[1]));

                switch (pair.Key)
                {
                    case Direction.down:
                        aim += pair.Value;
                        break;
                    case Direction.up:
                        aim -= pair.Value;
                        break;
                    case Direction.forward:
                        forwards += pair.Value;
                        downwards += pair.Value * aim;
                        break;
                }
            }

            var total = downwards * forwards;

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
