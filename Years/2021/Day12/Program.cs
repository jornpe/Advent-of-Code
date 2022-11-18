using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{

    public class Program
    {

        static void Main(string[] args)
        {

            var inputs = System.IO.File.ReadAllLines(@".\input.txt")
                .Select(x=>x.Split('-'))
                .Select(x=>  new {From = x[0], To = x[1]}) 
                .ToList();

            var caves = new List<Cave>();

            foreach (var input in inputs)
            {
                var cave1 = caves.FirstOrDefault(x => x.Name == input.From);
                var cave2 = caves.FirstOrDefault(x => x.Name == input.To);

                if (cave1 is {})
                {
                    cave1.Connections.Add(input.To);
                }
                else
                {
                    caves.Add(new Cave{Name = input.From, Connections = new List<string> {input.To}, IsBigCave = char.IsUpper(input.From[0])});
                }

                if (cave2 is { })
                {
                    cave2.Connections.Add(input.From);
                }
                else
                {
                    caves.Add(new Cave { Name = input.To, Connections = new List<string> { input.From }, IsBigCave = char.IsUpper(input.To[0]) });
                }
            }

            caves.ForEach(x=>x.Connections.RemoveAll(c=>c.Contains(x.Name)));
            caves.ForEach(x=>x.Connections.Sort());

            
            var validRoutesPart1 = new List<string>();
            var validRoutesPart2 = new List<string>();

            var startCave = caves.First(x => x.Name == "start");

            var test1 = CanGoToNext(new List<string> {"start", "A", "b", "A", "b", "A"}, new Cave{ Name = "b", IsBigCave = false});
            var test2 = CanGoToNext(new List<string> {"start", "A", "b", "A", "b", "A"}, new Cave { Name = "c", IsBigCave = false });
            var test3 = CanGoToNext(new List<string> {"start", "A", "b", "A", "b", "A", "c", "A"}, new Cave { Name = "c", IsBigCave = false });




            RouteBranch1(new List<string>(), startCave);
            RouteBranch2(new List<string>(), startCave);
            
            

            //Part 1
            void RouteBranch1(List<string> Currentroute, Cave currentCave)
            {
                Currentroute.Add(currentCave.Name);

                if (currentCave.Name == "end")
                {
                    var rute = Currentroute.Aggregate((a, b) => $"{a},{b}");
                    if (!validRoutesPart1.Contains(rute))
                    {
                        validRoutesPart1.Add(rute);
                    }
                    return;
                }

                foreach (var next in currentCave.Connections)
                {
                    var nextCave = caves.First(c => c.Name == next);

                    if (nextCave.IsBigCave || Currentroute.All(x => x != nextCave.Name))
                    {
                        RouteBranch1(Currentroute.ToList(), nextCave);
                    }
                }
            }


            //Part 2
            void RouteBranch2(List<string> Currentroute, Cave currentCave)
            {
                Currentroute.Add(currentCave.Name);

                if (currentCave.Name == "end")
                {
                    var rute = Currentroute.Aggregate((a, b) => $"{a},{b}");
                    if (!validRoutesPart2.Contains(rute))
                    {
                        validRoutesPart2.Add(rute);
                    }
                    return;
                }

                foreach (var next in currentCave.Connections)
                {
                    var nextCave = caves.First(c => c.Name == next);
                    
                    if (CanGoToNext(Currentroute.ToList(), nextCave))
                    {
                        RouteBranch2(Currentroute.ToList(), nextCave);
                    }
                }
            }
            
            Console.WriteLine("Part 1: " + validRoutesPart1.Count);
            Console.WriteLine();
            Console.WriteLine("Part 2: " + validRoutesPart2.Count);

        }

        public static bool CanGoToNext(List<string> path, Cave nextCave)
        {

            path.Add(nextCave.Name);
            path.RemoveAll(x => char.IsUpper(x[0]));

            if (nextCave.Name == "start")
            {
                return false;
            }

            if (nextCave.IsBigCave)
            {
                return true;
            }

            var moreThanOnce = path.GroupBy(x=>x).Where(x => x.Count() > 1).ToList();

            if (moreThanOnce.Count > 1)
            {
                return false;
            }

            var moreThanTwice = path.GroupBy(x => x).Where(x => x.Count() > 2).ToList();

            if (moreThanTwice.Count > 0)
            {
                return false;
            }

            return true;
        }

        public class Cave
        {
            public string Name { get; set; }
            public List<string> Connections { get; set; }
            public bool IsBigCave { get; set; }
            
        }
    }
}

