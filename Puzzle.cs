using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Puzzle
    {
        public PuzzleValue[,] values { get; set; }

        public Puzzle() 
        { 
            values = new PuzzleValue[9,9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    values[i, j] = new PuzzleValue();
            }
        }

        public void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                string line = string.Empty;
                for (int j = 0; j < 9; j++)
                {
                    var c = j % 3 == 0 && j != 0 ? '|' : ' ';
                    line = $"{line}{c}{values[i, j].ShowPossibleValues()}";
                }
                line = $"{line} ";
                Console.WriteLine("".PadLeft(line.Count(), i % 3 == 0 && i != 0 ? '=' : ' '));
                Console.WriteLine(line);
            }

        }
    }
}
