using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1
{

    class Program
    {
        static void Main(string[] args)
        {

            var inputs = System.IO.File.ReadLines(@".\input.txt").ToList();

            var draw = inputs.First().Split(',').Select(int.Parse);


            inputs.Remove(inputs.First());
            inputs.RemoveAll(string.IsNullOrWhiteSpace);

            var bingoBoards = new List<Board>();
            int numberofboards = inputs.Count / 5;

            var boards = inputs.Select(input => input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()).Select(test => test.Select(int.Parse).ToList()).ToList();


            for (int i = 0; i < numberofboards; i++)
            {
                bingoBoards.Add(new Board(boards.GetRange(i * 5, 5)));
            }

            bingoBoards.ForEach(b => b.Draw(draw.ToList()));

            var lowestDraw = bingoBoards.Max(s => s.NumberOfDraws);
            var winner = bingoBoards.First(x => x.NumberOfDraws.Equals(lowestDraw));

            Console.WriteLine(winner.Sum);
            Console.ReadKey();
        
        }


        public class Board
        {
            public List<List<int>> Rows { get; set; } = new List<List<int>>();
            public List<List<int>> Collumns { get; set; } = new List<List<int>>();
            public int NumberOfDraws { get; set; } = 0;
            public int Sum { get; set; } = 0;

            public Board(List<List<int>> rows)
            {
                Rows.AddRange(rows);

                for (var j = 0; j < 5; j++)
                {
                    Collumns.Add(rows.Select(num => num[j]).ToList());
                }
                
            }

            public void Draw(List<int> input)
            {
                int lastNumber = 0;

                foreach (var toDraw in input)
                {
                    NumberOfDraws++;
                    Collumns.ForEach(s => s.RemoveAll(i => i.Equals(toDraw)));
                    Rows.ForEach(s => s.RemoveAll(i => i.Equals(toDraw)));

                    if (Collumns.Any(s=>s.Count == 0) || Rows.Any(s => s.Count == 0))
                    {
                        lastNumber = toDraw;
                        break;
                    }
                }
                
                foreach (var row in Rows)
                {
                    Sum += row.Sum();
                }

                Sum *= lastNumber;
            }
        }
    }
}
