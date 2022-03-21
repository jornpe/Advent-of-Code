using System;
using System.Collections.Generic;

namespace Day1
{
    public static class CalculateDistances
    {

        public static List<Program.Distance> getDistances(Program.Beacon b1, Program.Beacon b2)
        {
            var distances = new List<Program.Distance>();

            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X - b2.X),
                Y = Math.Abs(b1.Y - b2.Y),
                Z = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X - b2.X),
                X = Math.Abs(b1.Y + b2.Y),
                Z = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X + b2.X),
                Y = Math.Abs(b1.Y + b2.Y),
                Z = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X + b2.X),
                X = Math.Abs(b1.Y - b2.Y),
                Z = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X - b2.X),
                Z = Math.Abs(b1.Y - b2.Y),
                X = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X + b2.X),
                Z = Math.Abs(b1.Y - b2.Y),
                Y = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X + b2.X),
                Z = Math.Abs(b1.Y - b2.Y),
                X = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X - b2.X),
                Z = Math.Abs(b1.Y - b2.Y),
                Y = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X - b2.X),
                X = Math.Abs(b1.Y - b2.Y),
                Y = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X - b2.X),
                Y = Math.Abs(b1.Y - b2.Y),
                X = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X - b2.X),
                X = Math.Abs(b1.Y + b2.Y),
                Y = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X - b2.X),
                Y = Math.Abs(b1.Y + b2.Y),
                X = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X - b2.X),
                X = Math.Abs(b1.Y - b2.Y),
                Z = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X + b2.X),
                Y = Math.Abs(b1.Y - b2.Y),
                Z = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X + b2.X),
                X = Math.Abs(b1.Y + b2.Y),
                Z = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X - b2.X),
                Y = Math.Abs(b1.Y + b2.Y),
                Z = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X - b2.X),
                Z = Math.Abs(b1.Y + b2.Y),
                Y = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X - b2.X),
                Z = Math.Abs(b1.Y + b2.Y),
                X = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                X = Math.Abs(b1.X + b2.X),
                Z = Math.Abs(b1.Y + b2.Y),
                Y = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Y = Math.Abs(b1.X + b2.X),
                Z = Math.Abs(b1.Y + b2.Y),
                X = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X + b2.X),
                Y = Math.Abs(b1.Y - b2.Y),
                X = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X + b2.X),
                X = Math.Abs(b1.Y + b2.Y),
                Y = Math.Abs(b1.Z - b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X + b2.X),
                Y = Math.Abs(b1.Y + b2.Y),
                X = Math.Abs(b1.Z + b2.Z)

            });
            distances.Add(new Program.Distance
            {
                Z = Math.Abs(b1.X + b2.X),
                X = Math.Abs(b1.Y - b2.Y),
                Y = Math.Abs(b1.Z + b2.Z)

            });


            return distances;
        }
    }
}