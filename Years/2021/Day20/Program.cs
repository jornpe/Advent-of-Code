using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Schema;

namespace Day1
{

    public partial class Program
    {

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            SolveP1();
            
            sw.Stop();
            Console.WriteLine($"Part 1 took {sw.Elapsed.Milliseconds} milliseconds to complete");

            sw.Reset();
            sw.Start();

            SolveP2();

            sw.Stop();
            Console.WriteLine($"Part 2 took {sw.Elapsed.Milliseconds} milliseconds to complete");
        }

        public static void SolveP1()
        {
            var input = File.ReadAllLines(@".\input.txt").ToList();


            var algorith = input[0].Select(c => c == '#').ToList();

            input.RemoveAt(0);
            input.RemoveAt(0);


            int count = 0;

            bool infinitySquare = false;

            var output = new List<string>();

            for (int step = 0; step < 2; step++)
            {
                output = new List<string>();

                char toAdd = infinitySquare ? '#' : '.';

                input = input.Prepend(new string(toAdd, input[0].Length)).Prepend(new string(toAdd, input[0].Length)).ToList();
                input = input.Append(new string(toAdd, input[0].Length)).Append(new string(toAdd, input[0].Length)).ToList();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i] = toAdd.ToString() + toAdd + input[i] + toAdd + toAdd;
                }


                for (int row = 1; row < input.Count - 1; row++)
                {
                    string outputLine = "";

                    for (int col = 1; col < input[row].Length - 1; col++)
                    {

                        var square = "";

                        if (row < 1 || row > input.Count - 1 || col < 1 || col > input[row].Length - 1)
                        {
                            outputLine += infinitySquare ? '#' : '.';
                        }
                        else
                        {
                            square += input[row - 1][col - 1] == '#' ? "1" : "0";
                            square += input[row - 1][col] == '#' ? "1" : "0";
                            square += input[row - 1][col + 1] == '#' ? "1" : "0";
                            square += input[row][col - 1] == '#' ? "1" : "0";
                            square += input[row][col] == '#' ? "1" : "0";
                            square += input[row][col + 1] == '#' ? "1" : "0";
                            square += input[row + 1][col - 1] == '#' ? "1" : "0";
                            square += input[row + 1][col] == '#' ? "1" : "0";
                            square += input[row + 1][col + 1] == '#' ? "1" : "0";

                            var dec = Convert.ToInt32(square, 2);

                            outputLine += algorith[dec] ? '#' : '.';
                        }

                    }
                    output.Add(outputLine);
                }

                if (algorith[0])
                {
                    infinitySquare = !infinitySquare;
                }

                input = output;
            }

            foreach (var s in input)
            {
                //Console.WriteLine(s);
                count += s.Count(c => c == '#');
            }

            Console.WriteLine(count);
        }

        public static void SolveP2()
        {
            var input = File.ReadAllLines(@".\input.txt").ToList();


            var algorith = input[0].Select(c => c == '#').ToList();

            input.RemoveAt(0);
            input.RemoveAt(0);


            int count = 0;

            bool infinitySquare = false;

            var output = new List<string>();

            for (int step = 0; step < 50; step++)
            {
                output = new List<string>();

                char toAdd = infinitySquare ? '#' : '.';

                input = input.Prepend(new string(toAdd, input[0].Length)).Prepend(new string(toAdd, input[0].Length)).ToList();
                input = input.Append(new string(toAdd, input[0].Length)).Append(new string(toAdd, input[0].Length)).ToList();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i] = toAdd.ToString() + toAdd + input[i] + toAdd + toAdd;
                }


                for (int row = 1; row < input.Count - 1; row++)
                {
                    string outputLine = "";

                    for (int col = 1; col < input[row].Length - 1; col++)
                    {

                        var square = "";

                        if (row < 1 || row > input.Count - 1 || col < 1 || col > input[row].Length - 1)
                        {
                            outputLine += infinitySquare ? '#' : '.';
                        }
                        else
                        {
                            square += input[row - 1][col - 1] == '#' ? "1" : "0";
                            square += input[row - 1][col] == '#' ? "1" : "0";
                            square += input[row - 1][col + 1] == '#' ? "1" : "0";
                            square += input[row][col - 1] == '#' ? "1" : "0";
                            square += input[row][col] == '#' ? "1" : "0";
                            square += input[row][col + 1] == '#' ? "1" : "0";
                            square += input[row + 1][col - 1] == '#' ? "1" : "0";
                            square += input[row + 1][col] == '#' ? "1" : "0";
                            square += input[row + 1][col + 1] == '#' ? "1" : "0";

                            var dec = Convert.ToInt32(square, 2);

                            outputLine += algorith[dec] ? '#' : '.';
                        }

                    }
                    output.Add(outputLine);
                }

                if (algorith[0])
                {
                    infinitySquare = !infinitySquare;
                }

                input = output;
            }

            foreach (var s in input)
            {
                //Console.WriteLine(s);
                count += s.Count(c => c == '#');
            }

            Console.WriteLine(count);
        }
    }
}

    


