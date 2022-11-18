using Day01;
using System.Diagnostics;

Console.WriteLine("Advent Of Code");
Console.WriteLine("=================================");

var input = File.ReadAllLines(@".\input.txt");


Stopwatch stopwatch1 = Stopwatch.StartNew();
var result1 = Part1.Solution(input);
stopwatch1.Stop();

Stopwatch stopwatch2 = Stopwatch.StartNew();
var result2 = Part2.Solution(input);
stopwatch2.Stop();

Console.WriteLine();
Console.WriteLine("Part 1:");
Console.WriteLine($"Result: {result1}");
Console.WriteLine($"Execution time: {stopwatch1.ElapsedMilliseconds} milliseconds");

Console.WriteLine();
Console.WriteLine("Part 2:");
Console.WriteLine($"Result: {result2}");
Console.WriteLine($"Execution time: {stopwatch2.ElapsedMilliseconds} milliseconds");

Console.ReadKey();