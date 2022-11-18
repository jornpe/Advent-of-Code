using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{

    class Program
    {
        static void Main(string[] args)
        {

            var inputs = System.IO.File.ReadAllLines(@".\input.txt").ToList();
            

            int matches = 0;
            foreach (var line in inputs)
            {
                var signals = line.Split('|')[0];
                var output = line.Split('|')[1];

                var getValues = Detect(signals.Split(' ').ToList());

                string value = "";

                foreach (var outp in output.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    value += getValues.FirstOrDefault(x => x.Value.Equals(string.Concat(outp.OrderBy(x => x)))).Key.ToString();
                }

                matches += int.Parse(value);

            }

            Console.WriteLine(matches);
        }

        public static Dictionary<int, string> Detect(List<string> input)
        {
            var detected = new Dictionary<int, string>();

            var one = input.First(x => x.Length is 2);
            var four = input.First(x => x.Length is 4);
            var seven = input.First(x => x.Length is 3);
            var eight = input.First(x => x.Length is 7);
            var tree = input.First(x => x.Length is 5 && x.Contains(one[0]) && x.Contains(one[1]));
            var nine = input.First(x => x.Length is 6 && x.Contains(four[0]) && x.Contains(four[1]) && x.Contains(four[2]) && x.Contains(four[3]));

            var left2 = eight.Except(nine.ToCharArray()).First();

            var two = input.First(x=>x.Length is 5 && !x.Equals(tree) && x.Contains(left2)); 
            var five = input.First(x => x.Length is 5 && !x.Equals(tree) && !x.Equals(two));

            var zero = input.First(x => x.Length is 6 && !x.Equals(nine) && x.Contains(one[0]) && x.Contains(one[1]));
            
            var six = input.First(x => x.Length is 6 && !x.Equals(zero) && !x.Equals(nine));

            detected.Add(1, string.Concat(one.OrderBy(x=>x)));
            detected.Add(2, string.Concat(two.OrderBy(x => x)));
            detected.Add(3, string.Concat(tree.OrderBy(x => x)));
            detected.Add(4, string.Concat(four.OrderBy(x => x)));
            detected.Add(5, string.Concat(five.OrderBy(x => x)));
            detected.Add(6, string.Concat(six.OrderBy(x => x)));
            detected.Add(7, string.Concat(seven.OrderBy(x => x)));
            detected.Add(8, string.Concat(eight.OrderBy(x => x)));
            detected.Add(9, string.Concat(nine.OrderBy(x => x)));
            detected.Add(0, string.Concat(zero.OrderBy(x => x)));


            return detected;
        }
    }
}

