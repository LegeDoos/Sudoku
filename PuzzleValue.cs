using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Value of an element in the puzzle
    /// </summary>
    public class PuzzleValue
    {
        /// <summary>
        /// Possible values the element can have
        /// </summary>
        bool[] possibleValues;

        /// <summary>
        /// Constructor
        /// </summary>
        public PuzzleValue()
        {
            // pos 0 = 1, etc
            possibleValues = [true, true, true, true, true, true, true, true, true];
        }

        /// <summary>
        /// Format a string with the possible values for the element
        /// </summary>
        /// <returns></returns>
        public string ShowPossibleValues()
        {
            string retVal = string.Empty;
            for (int i = 0;i < 9; i++)
            {
                retVal = $"{retVal}{PosToVal(i)}";
            }

            return retVal;
        }

        /// <summary>
        /// Helper function to translate the boolean to a number
        /// </summary>
        /// <param name="pos">The position in the array</param>
        /// <returns>The readable value of the element</returns>
        private string PosToVal(int pos)
        {
            return possibleValues[pos] ? $"{pos + 1}" : " ";
        }


    }
}
