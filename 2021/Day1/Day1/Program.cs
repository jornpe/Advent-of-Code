using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int? previous = null;
            int count = 0;

            var input = System.IO.File.ReadLines(@".\input.txt").Select(line => Convert.ToInt16(line)).Select(dummy => (int) dummy).ToList();
            var input2 = new List<int>();

            for (int i = 0; i < input.Count - 2; i++)
            {
                input2.Add(input[i] + input[i+1] + input[i+2]);
            }

            foreach (var current in input2)
            {
                    if (previous != null)
                    {
                        if (previous < current)
                        {
                            count++;
                        }
                    }

                    previous = current;
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
