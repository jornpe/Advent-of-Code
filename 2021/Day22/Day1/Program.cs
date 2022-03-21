using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DaYmin
{

    public class Program
    {

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("---------------- Part 1 ----------------");
            SolveP1();
            
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Part 1 took {sw.Elapsed.Milliseconds} milliseconds to complete");
            Console.WriteLine();
            Console.WriteLine("---------------- Part 2 ----------------");
            sw.Reset();
            sw.Start();

            SolveP2();

            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Part 2 took {sw.Elapsed.Milliseconds} milliseconds to complete");
        }

        public static void SolveP1()
        {
            var inputs = File.ReadAllLines(@".\input.txt").ToList();


            var commands = new List<(bool State, List<int> RangeX, List<int> RangeY, List<int> RangeZ)>();

            foreach (var input in inputs)
            {

                (bool State, List<int> RangeX, List<int> RangeY, List<int> RangeZ) command;

                command.State = input.Split(' ')[0] == "on";

                var ranges = input.Split(' ')[1].Split(',').Select(x => x.Substring(2)).ToList();

                int minX = int.Parse(ranges[0].Split("..")[0]);
                int maxX = int.Parse(ranges[0].Split("..")[1]);

                int minY = int.Parse(ranges[1].Split("..")[0]);
                int maxY = int.Parse(ranges[1].Split("..")[1]);

                int minZ = int.Parse(ranges[2].Split("..")[0]);
                int maxZ = int.Parse(ranges[2].Split("..")[1]);

                if (maxX < -50 || maxY < -50 || maxZ < -50)
                {
                    continue;
                }
                if (minX > 50 || minY > 50 || minZ > 50)
                {
                    continue;
                }

                minX = minX < -50 ? -50 : minX;
                maxX = maxX >  50 ?  50 : maxX;

                minY = minY < -50 ? -50 : minY;
                maxY = maxY > 50 ? 50 : maxY;

                minZ = minZ < -50 ? -50 : minZ;
                maxZ = maxZ > 50 ? 50 : maxZ;

                command.RangeX = Enumerable.Range(minX, maxX - minX + 1).ToList();
                command.RangeY = Enumerable.Range(minY, maxY - minY + 1).ToList();
                command.RangeZ = Enumerable.Range(minZ, maxZ - minZ + 1).ToList();
                
                commands.Add(command);

            }
            

            var cubes = new Dictionary<(int x, int y, int z), bool>();

            foreach (var command in commands)
            {
                foreach (var x in command.RangeX)
                {
                    foreach (var y in command.RangeY)
                    {
                        foreach (var z in command.RangeZ)
                        {
                            if (!cubes.ContainsKey((x,y,z)))
                            {
                                cubes.Add((x,y,z), command.State);
                                continue;
                            }
                                
                            cubes[(x, y, z)] = command.State;
                            
                        }
                    }
                }
            }

            var count = cubes.Count(x => x.Value);

            Console.WriteLine(count);

        }

        public static void SolveP2()
        {
            var inputs = File.ReadAllLines(@".\input.txt").ToList();

            var cubes = new List<Cube>();

            foreach (var input in inputs)
            {
                var newCube = new Cube();
                var tempCubes = new List<Cube>();

                newCube.State = input.Split(' ')[0] == "on";

                var ranges = input.Split(' ')[1].Split(',').Select(x => x[2..]).ToList();

                newCube.Xmin = int.Parse(ranges[0].Split("..")[0]);
                newCube.Xmax = int.Parse(ranges[0].Split("..")[1]);

                newCube.Ymin = int.Parse(ranges[1].Split("..")[0]);
                newCube.Ymax = int.Parse(ranges[1].Split("..")[1]);

                newCube.Zmin = int.Parse(ranges[2].Split("..")[0]);
                newCube.Zmax = int.Parse(ranges[2].Split("..")[1]);
                

                foreach (var cube in cubes)
                {
                    if (cube.IsInsideThisCube(newCube))
                    {
                        tempCubes.AddRange(cube.SplitThisCube(newCube));
                    }
                    else
                    {
                        tempCubes.Add(cube);
                    }
                }

                if (newCube.State)
                {
                    tempCubes.Add(newCube);
                }

                cubes = tempCubes;

            }

            long count = 0;
            
            foreach (var cube in cubes)
            {
                count += (cube.Xmax - cube.Xmin + 1) * (cube.Ymax - cube.Ymin + 1) * (cube.Zmax - cube.Zmin + 1);

            }
                        
            Console.WriteLine(count);
            
        }


        public class Cube
        {

            public long Xmin { get; set; }
            public long Xmax { get; set; }
            public long Ymin { get; set; }
            public long Ymax { get; set; }
            public long Zmin { get; set; }
            public long Zmax { get; set; }
            public bool State { get; set; }
            
            public bool IsInsideThisCube(Cube c2)
            {
                return Xmax >= c2.Xmin && Xmin <= c2.Xmax && Ymax >= c2.Ymin && Ymin <= c2.Ymax && Zmax >= c2.Zmin && Zmin <= c2.Zmax;
            }

            public List<Cube> SplitThisCube(Cube c2)
            {
                var subCubes = new List<Cube>();
                
                //Create a cube to the left
                if (c2.Xmin > Xmin)
                {
                    subCubes.Add(new Cube {Xmin = Xmin, Xmax = c2.Xmin - 1, Ymin = Ymin, Ymax = Ymax, Zmax = Zmax, Zmin = Zmin, State = State});
                }

                //Create a cube to the Right
                if (c2.Xmax < Xmax)
                {
                    subCubes.Add(new Cube { Xmin = c2.Xmax + 1, Xmax = Xmax, Ymin = Ymin, Ymax = Ymax, Zmax = Zmax, Zmin = Zmin, State = State });
                }

                //Create a cube to the back
                if (c2.Ymin > Ymin)
                {
                    subCubes.Add(new Cube { Xmin = Math.Max(Xmin, c2.Xmin), Xmax = Math.Min(Xmax, c2.Xmax), Ymin = Ymin, Ymax = c2.Ymin - 1, Zmax = Zmax, Zmin = Zmin, State = State });
                }

                //Create a cube to the front
                if (c2.Ymax < Ymax)
                {
                    subCubes.Add(new Cube { Xmin = Math.Max(Xmin, c2.Xmin), Xmax = Math.Min(Xmax, c2.Xmax), Ymin = c2.Ymax + 1, Ymax = Ymax, Zmax = Zmax, Zmin = Zmin, State = State });
                }


                //Create a cube to the below
                if (c2.Zmin > Zmin)
                {
                    subCubes.Add(new Cube { Xmin = Math.Max(Xmin, c2.Xmin), Xmax = Math.Min(Xmax, c2.Xmax), Ymin = Math.Max(Ymin, c2.Ymin), Ymax = Math.Min(Ymax, c2.Ymax), Zmin = Zmin, Zmax = c2.Zmin - 1, State = State });
                }

                //Create a cube to the above
                if (c2.Zmax < Zmax)
                {
                    subCubes.Add(new Cube { Xmin = Math.Max(Xmin, c2.Xmin), Xmax = Math.Min(Xmax, c2.Xmax), Ymin = Math.Max(Ymin, c2.Ymin), Ymax = Math.Min(Ymax, c2.Ymax), Zmin = c2.Zmax + 1, Zmax = Zmax, State = State });
                }



                return subCubes;
            }
        }
    }
        
        
}
