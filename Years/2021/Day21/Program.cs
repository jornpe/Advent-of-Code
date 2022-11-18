using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
{

    public partial class Program
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
            var input = File.ReadAllLines(@".\input.txt").ToList();

            var playerOne = new Player { Position = int.Parse(input[0].Split(':')[1]), Score = 0};
            var playerTwo = new Player { Position = int.Parse(input[1].Split(':')[1]), Score = 0};

            int diceNumber = 0;
            int diceRolls = 0;

            while (true)
            {
                var throws = GetThrows(3);
                playerOne.UpdatePosition(throws);

                if (playerOne.Score >= 1000)
                {
                    break;
                }

                throws = GetThrows(3);
                playerTwo.UpdatePosition(throws);

                if (playerTwo.Score >= 1000)
                {
                    break;
                }
            }

            if (playerOne.Score >= 1000)
            {
                Console.WriteLine(playerTwo.Score * diceRolls);
            }
            else
            {
                Console.WriteLine(playerOne.Score * diceRolls);
            }


            int GetThrows(int numberOfThrows)
            {
                diceRolls += numberOfThrows;
                var returnMove = 0;
                for (var i = 0; i < numberOfThrows; i++)
                {
                    if (diceNumber == 100)
                    {
                        diceNumber = 1;
                    }
                    else
                    {
                        diceNumber++;
                    }

                    returnMove += diceNumber;
                }

                return returnMove;
            }
        }

        public static void SolveP2()
        {
            var input = File.ReadAllLines(@".\input.txt").ToList();

            var playerOne = new Player { Position = int.Parse(input[0].Split(':')[1]), Score = 0 };
            var playerTwo = new Player { Position = int.Parse(input[1].Split(':')[1]), Score = 0 };
            
            var gameStates = new Dictionary<(int, int, int, int), (long, long)>();
            

            var (p1, p2) = play(playerOne.Position, 0, playerTwo.Position, 0);

            Console.WriteLine($"Player 1 wins {p1}");
            Console.WriteLine($"Player 2 wins {p2}");


            (long, long) play(int player1, int score1, int player2, int score2)
            {
                if (score1 >= 21)
                {
                    return (1, 0);
                }
                if (score2 >= 21)
                {
                    return (0, 1);
                }


                if (gameStates.ContainsKey((player1, score1, player2, score2)))
                {
                    gameStates.TryGetValue((player1, score1, player2, score2), out var value);

                    return value;
                }

                (long p1, long p2) winners = (0, 0);

                for (int r1 = 1; r1 <= 3; r1++)
                {
                    for (int r2 = 1; r2 <= 3; r2++)
                    {
                        for (int r3 = 1; r3 <= 3; r3++)
                        {
                            var newp1 = (player1 + r1 + r2 + r3);

                            if (newp1 > 10)
                            {
                                newp1 %= 10;
                            }

                            var newS1 = score1 + newp1;
                            var (wP2, wP1) = play(player2, score2, newp1, newS1);

                            winners.p1 += wP1;
                            winners.p2 += wP2;

                        }
                    }
                }
                
                gameStates.Add((player1, score1, player2, score2), winners);
                

                return winners;
            }

        }


        public class Player
        {
            public long Winns { get; set; }
            public int Position { get; set; }
            public int Score { get; set; }

            public void UpdatePosition(int move)
            {
                for (int i = 0; i < move; i++)
                {
                    if (Position == 10)
                    {
                        Position = 1;
                    }
                    else
                    {
                        Position++;
                    }
                }

                Score += Position;
            }
        }
        
    }
}

    


