using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
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
            var inputs = File.ReadAllLines(@".\test.txt").ToList();

            inputs.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            var scanners = new List<Scanner>();

            Scanner tempScanner = null;

            foreach (var input in inputs)
            {
                if (input.StartsWith("---"))
                {
                    if (tempScanner != null)
                    {
                        scanners.Add(tempScanner);
                    }

                    tempScanner = new Scanner { ID = int.Parse(input.Replace("--- scanner ", "").Replace(" ---", "")) };

                    if (tempScanner.ID != 0) continue;

                    tempScanner.Position.X = 0;
                    tempScanner.Position.Y = 0;
                    tempScanner.Position.Z = 0;
                }
                else
                {
                    var points = input.Split(',');

                    tempScanner?.Beacons.Add(new Beacon { X = int.Parse(points[0]), Y = int.Parse(points[1]), Z = int.Parse(points[2]) });
                }
            }

            scanners.Add(tempScanner);

            foreach (var scanner in scanners)
            {
                for (int i = 0; i < scanner.Beacons.Count; i++)
                {   
                    for (int j = 0; j < scanner.Beacons.Count; j++)
                    {
                        if (i == j) continue;
                        var d = (int)CalculateDistance(scanner.Beacons[i].X, scanner.Beacons[i].Y, scanner.Beacons[i].Z, scanner.Beacons[j].X,
                            scanner.Beacons[j].Y, scanner.Beacons[j].Z);
                            //(int)Math.Sqrt(Math.Pow(scanner.Beacons[i].X, 2) + Math.Pow(scanner.Beacons[i].Y, 2) + Math.Pow(scanner.Beacons[i].Z, 2));
                        scanner.Beacons[i].RelDist.Add(new Distance{ Dist = d});
                        //scanner.Beacons[i].RelDist.AddRange(CalculateDistances.getDistances(scanner.Beacons[i], scanner.Beacons[j]));
                        //var test = CalculateDistances.getDistances(new Beacon{ X = 0, Y = 0, Z = 0}, new Beacon { X = 1, Y = 2, Z = 3 });
                    }
                }
            }

            var uniqueBeacons = new List<Beacon>();

            uniqueBeacons.AddRange(scanners.FirstOrDefault(x => x.ID == 0).Beacons);

            for (int s = 1; s < scanners.Count; s++)
            {
                foreach (var beacon in scanners[s].Beacons)
                {
                    if (isNewBeacon(beacon))
                    {
                        uniqueBeacons.Add(beacon);
                    }
                }
            }

            //Console.WriteLine(scanners.Sum(s => s.Beacons.Count));
            Console.WriteLine(uniqueBeacons.Count);

            double CalculateDistance(double x0, double y0, double z0, double x1, double y1, double z1)
            {
                double deltaX = x1 - x0;
                double deltaY = y1 - y0;
                double deltaZ = z1 - z0;

                return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            }

            bool isNewBeacon(Beacon b1)
            {
                foreach (var beacon in uniqueBeacons.ToList())
                {
                    var overlapping = 0;

                    foreach (var b1Dist in b1.RelDist)
                    {
                        overlapping += beacon.RelDist.Count(d => d.Dist == b1Dist.Dist);

                    }
                    
                    if (overlapping > 11)
                    {
                        return false;
                    }

                }

                return true;
            }

        }

        public class Scanner
        {
            public Scanner()
            {
                Beacons = new List<Beacon>();
                Position = new Point();
            }
            public int ID { get; set; }
            public Point Position { get; set; }
            public List<Beacon> Beacons { get; set; }

        }

        public class Distance : Point
        {
            public int Dist { get; set; }
        }

        public class Beacon : Point
        {
            public Beacon()
            {
                RelDist = new List<Distance>();
            }
            public List<Distance> RelDist { get; set; }

        }
    }
}

    


