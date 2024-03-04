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
        /// The definite value of the element
        /// </summary>
        public string Value { get; private set; } = string.Empty;

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

            return retVal.PadRight(9);
        }

        /// <summary>
        /// Helper function to translate the boolean to a number
        /// </summary>
        /// <param name="pos">The position in the array</param>
        /// <returns>The readable value of the element</returns>
        private string PosToVal(int pos)
        {
            return possibleValues[pos] ? $"{pos + 1}" : "";
        }

        /// <summary>
        /// Set a value as not possible anymore for the element
        /// </summary>
        /// <param name="value"></param>
        public void RemoveOption(int value)
        {
            possibleValues[value - 1] = false;
            if (InvalidValue())
                throw new InvalidValueException("No possible values left");
        }

        /// <summary>
        /// Set the definite value for the element
        /// </summary>
        /// <param name="value">The definite value</param>
        /// <exception cref="InvalidValueException">Value not possible</exception>
        public void SetValue(int value)
        {
            if (possibleValues[value - 1] == false)
                throw new InvalidValueException("Value not possible");


            for (int i = 0; i < 9; i++)
            {
                if (i == value - 1)
                    possibleValues[i] = true;
                else
                    possibleValues[i] = false;
            }

            Value = value.ToString();
        }

        /// <summary>
        /// Value is invalid in case there are no possible values left
        /// </summary>
        /// <returns>true when invalid</returns>
        private bool InvalidValue()
        {
            return possibleValues.Count(x => x) == 0;
        }

    }
}
