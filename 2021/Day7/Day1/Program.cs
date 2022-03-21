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

            var position = inputs
                .Split(',')
                .Select(int.Parse)
                .ToList();

            List<int> usedFuel = new();

            var inn = 16;
            var too = 5;
            var test3 = Enumerable.Range(1, Math.Max(inn, too) - Math.Min(inn, too));
            var test4 = test3.Sum();

            

            for (int i = position.Min(); i < position.Max(); i++)
            {
                var fuel = position.Sum(pos => Enumerable.Range(1, Math.Max(i, pos) - Math.Min(i, pos)).Sum());

                usedFuel.Add(fuel);
            }

            Console.WriteLine(usedFuel.Min());
        }
    }
}

