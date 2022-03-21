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

            var inputs = System.IO.File.ReadAllText(@".\input.txt");

            var fishes = new List<LanternFish>();

            for (int i = 0; i < 9; i++)
            {
                fishes.Add(new LanternFish { DaySycle = i, NumberOfFishes = 0});
            }

            inputs
                .Split(',')
                .GroupBy(x => double.Parse(x))
                .Select(x => fishes.First(y => y.DaySycle == x.Key).NumberOfFishes = x.Count())
                .ToList();


            for (int i = 1; i <= 256; i++)
            {
                var toReset = fishes[0];
                fishes.RemoveAt(0);
                fishes.ForEach(x => x.DaySycle--);
                fishes[6].NumberOfFishes += toReset.NumberOfFishes;
                fishes.Add(toReset);
                Console.WriteLine($"After {i} days there are {fishes.Sum(x => x.NumberOfFishes)} fishes");
            }
        }

        public class LanternFish
        {
            public int DaySycle { get; set; }
            public double NumberOfFishes { get; set; }
        }
    }
}

