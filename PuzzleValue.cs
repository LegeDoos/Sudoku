using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class PuzzleValue
    {
        bool[] possibleValues;

        public PuzzleValue()
        {
            // pos 0 = 1, etc
            possibleValues = [true, true, true, true, true, true, true, true, true];
        }

        public string ShowPossibleValues()
        {
            string retVal = string.Empty;
            for (int i = 0;i < 9; i++)
            {
                retVal = $"{retVal}{PosToVal(i)}";
            }

            return retVal;
        }

        private string PosToVal(int pos)
        {
            return possibleValues[pos] ? $"{pos + 1}" : " ";
        }


    }
}
