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

            string template = inputs.First();
            string template2 = inputs.First();

            inputs.RemoveAt(0);
            inputs.RemoveAt(0);

            var Pairs = new List<Pair>();

            foreach (var input in inputs)
            {
                var t1 = input.Split(" -> ");
                Pairs.Add(new Pair{ Rule = t1[0], Insert = t1[1]});
            }

            // Step 1
            for (int i = 0; i < 10; i++)
            {
                var newTemplate = "";

                for (int j = 0; j < template.Length - 1; j++)
                {
                    var pair = Pairs.First(p => p.Rule == template.Substring(j, 2));

                    newTemplate += pair.Rule[0] + pair.Insert;

                    if (j + 1 == template.Length - 1)
                        newTemplate += template.Last();
                }

                template = newTemplate;
            }

            var counts = template.ToCharArray().ToList().GroupBy(x=>x).Select(x => x.Count());

            Console.WriteLine(counts.Max() - counts.Min());



            // Step 2


            var pairCount = new List<UpdatedPairs>();


            foreach (var pair in Pairs)
            {
                pairCount.Add(new UpdatedPairs { Rule = pair.Rule, Count = 0});
            }

            for (int i = 0; i < template2.Length - 1; i++)
            {
                var pair = template2.Substring(i, 2);

                pairCount.First(p => p.Rule == pair).Count++;

            }

            for (int i = 0; i < 40; i++)
            {

                var newPairCount = new List<UpdatedPairs>();

                foreach (var pair in pairCount.Where(p => p.Count > 0))
                {
                    long count = pair.Count;
                    var insertion = Pairs.First(p => p.Rule == pair.Rule).Insert;

                    var newPair1 = pair.Rule[0] + insertion;
                    var newPair2 = insertion + pair.Rule[1];

                    if (newPairCount.FirstOrDefault(x => x.Rule == newPair1) is {} toUpdate1)
                    {
                        toUpdate1.Count += count;
                    }
                    else
                    {
                        newPairCount.Add(new UpdatedPairs { Rule = newPair1, Count = count });
                    }

                    if (newPairCount.FirstOrDefault(x => x.Rule == newPair2) is { } toUpdate2)
                    {
                        toUpdate2.Count += count;
                    }
                    else
                    {
                        newPairCount.Add(new UpdatedPairs { Rule = newPair2, Count = count });
                    }
                    
                }

                pairCount = newPairCount;

            }

            var totalCount = new List<Counts>();

            foreach (var pair in pairCount)
            {
                
                if (totalCount.FirstOrDefault(p => p.Character == pair.Rule[0]) is {} count1)
                {
                    count1.Count += pair.Count;
                }
                else
                {
                    totalCount.Add(new Counts { Character = pair.Rule[0], Count = pair.Count });
                }

            }

            totalCount.FirstOrDefault(x => x.Character == template2.Last()).Count++;

            long maximum = totalCount.Select(x => x.Count).Max();
            long minimum = totalCount.Select(x => x.Count).Min();
            
            Console.WriteLine(maximum - minimum);

        }

        public class Counts
        {
            public char Character { get; set; }
            public long Count { get; set; }
        }


        public class UpdatedPairs
        {
            public string Rule { get; set; }
            public long Count { get; set; }

        }


        public class Pair
        {
            public string Rule { get; set; }
            public string Insert { get; set; }

        }
    }
}

