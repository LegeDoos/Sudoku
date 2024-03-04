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
            Console.Clear();

            
            for (int i = 0; i < 9; i++)
            {
                int y = i + 1;
                string lineSet = y.ToString().PadRight(3);
                string lineValues = string.Empty.PadRight(3);
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0 && i == 0)
                    {
                        string header = string.Empty.PadRight(4);
                        for (int h = 0; h < 9; h++)
                        {
                            int x = h + 1;
                            header = $"{header}{x.ToString().PadRight(10)}";
                        }
                        // print header
                        Console.WriteLine(header);
                    }
                    var c = j % 3 == 0 && j != 0 ? '|' : ' ';
                    lineSet = $"{lineSet}{c}{values[i, j].Value.PadRight(9)}";
                    lineValues = $"{lineValues}{c}{values[i, j].ShowPossibleValues()}";
                }
                lineSet = $"{lineSet} ";
                lineValues = $"{lineValues} ";
                Console.WriteLine("".PadLeft(lineValues.Count(), i % 3 == 0 && i != 0 ? '=' : ' '));
                Console.WriteLine(lineSet);
                Console.WriteLine(lineValues);
            }

            Console.WriteLine("\nRij is x, kolom is y");

            // todo options
        }

        /// <summary>
        /// Set the value in the puzzle to be definite
        /// </summary>
        /// <param name="x">row</param>
        /// <param name="y">line</param>
        /// <param name="value">The value to set</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetValue(int x, int y, int value)
        {
            // checks
            if (x < 0 || x > 8 || y < 0 || y > 8)
                throw new ArgumentOutOfRangeException("Invalid position");

            if (value < 1 || value > 9)
                throw new ArgumentOutOfRangeException("Invalid value");

            // set the value
            values[x, y].SetValue(value);

            // remove the value from other elements
            // line and column
            for (int i = 0; i < 9; i++)
            {
                if (i != x)
                    values[i, y].RemoveOption(value);
                if (i != y)
                    values[x, i].RemoveOption(value);
            }
            // section
            int xStart = x / 3 * 3;
            int yStart = y / 3 * 3;
            for (int i = xStart; i < xStart + 3; i++)
            {
                for (int j = yStart; j < yStart + 3; j++)
                {
                    if (i != x && j != y)
                        values[i, j].RemoveOption(value);
                }
            }

        }
    }
}
