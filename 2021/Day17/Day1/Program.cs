using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Day1
{

    public partial class Program
    {

        static void Main(string[] args)
        {
            var inputs = System.IO.File.ReadAllLines(@".\input.txt").ToList();

            var input = inputs[0]
                .Split(',')
                .Select(p=>p.Replace("target area: x=", "").Replace("y=", ""))
                .ToList();

            var targetUpperLeft = new Point { X = int.Parse(input[0].Split("..")[0]), Y = int.Parse(input[1].Split("..")[1]) };
            var targetLowerRight = new Point { X = int.Parse(input[0].Split("..")[1]), Y = int.Parse(input[1].Split("..")[0]) };
            

            List<Point> bestTrejectory = null;
            int countNumberOfTrejectories = 0;

            for (int velX = 1; velX <= targetLowerRight.X; velX++)
            {
                for (int velY = targetLowerRight.Y; velY <= Math.Abs(targetLowerRight.Y); velY++)
                {
                    var trejectory = new List<Point>();
                    var currentPosition = new Point{X = 0, Y = 0, VelocityX = velX, VelocityY = velY};
                    bool isOutsideBoundery = false;

                    while (!isOutsideBoundery)
                    {
                        trejectory.Add(new Point { X = currentPosition.X, Y = currentPosition.Y, VelocityX = currentPosition.VelocityX, VelocityY = currentPosition.VelocityY});
                        UpdatePosition(currentPosition);

                        if ((currentPosition.X > targetLowerRight.X) || (currentPosition.Y < targetLowerRight.Y))
                        {
                            isOutsideBoundery = true;
                        }
                    }

                    if (trejectory.Any(p => p.X >= targetUpperLeft.X && p.X <= targetLowerRight.X && p.Y <= targetUpperLeft.Y && p.Y >= targetLowerRight.Y))
                    {
                        if (bestTrejectory is null || (bestTrejectory[0].VelocityY >= 0 && bestTrejectory[0].VelocityY <= trejectory[0].VelocityY))
                        {
                            bestTrejectory = trejectory;
                        }

                        countNumberOfTrejectories++;
                    }
                }
            }

            var highest = 0; 
            bestTrejectory?.ForEach(t => highest = t.Y > highest ? t.Y : highest);

            Console.WriteLine($"Highest: {highest}");
            Console.WriteLine($"Number of Trajectories: {countNumberOfTrejectories}");

        }

        public static void UpdatePosition(Point p)
        {
            if (p.VelocityX > 0)
            {
                p.X += p.VelocityX;
                p.VelocityX--;
            }
            else if (p.VelocityX < 0)
            {
                p.X -= p.VelocityX;
                p.VelocityX++;
            }
            
            p.Y += p.VelocityY;
            p.VelocityY--;
        }


    }
}

    


