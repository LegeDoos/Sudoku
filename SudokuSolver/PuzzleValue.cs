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
        public bool[] PossibleValues { get; private set; }

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
            PossibleValues = [true, true, true, true, true, true, true, true, true];
        }

        /// <summary>
        /// Copy the puzzle value
        /// </summary>
        /// <returns>The copied value</returns>
        public PuzzleValue Copy()
        {
            PuzzleValue retVal = new PuzzleValue();
            for (int i = 0; i < 9; i++)
            {
                retVal.PossibleValues[i] = PossibleValues[i];
            }
            retVal.Value = Value;
            return retVal;
        }

        /// <summary>
        /// Format a string with the possible values for the element
        /// </summary>
        /// <returns></returns>
        public string ShowPossibleValues()
        {
            string retVal = string.Empty;
            if (string.IsNullOrEmpty(Value))
            {
                for (int i = 0; i < 9; i++)
                {
                    retVal = $"{retVal}{PosToVal(i)}";
                }
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
            return PossibleValues[pos] ? $"{pos + 1}" : "";
        }

        /// <summary>
        /// Set a value as not possible anymore for the element
        /// </summary>
        /// <param name="value"></param>
        public void RemoveOption(int value)
        {
            PossibleValues[value - 1] = false;
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
            if (PossibleValues[value - 1] == false)
                throw new InvalidValueException("Value not possible");


            for (int i = 0; i < 9; i++)
            {
                if (i == value - 1)
                    PossibleValues[i] = true;
                else
                    PossibleValues[i] = false;
            }

            Value = value.ToString();
        }

        /// <summary>
        /// Value is invalid in case there are no possible values left
        /// </summary>
        /// <returns>true when invalid</returns>
        private bool InvalidValue()
        {
            return PossibleValues.Count(x => x) == 0;
        }

        /// <summary>
        /// Return the last possible value for the element
        /// </summary>
        /// <returns>the last value</returns>
        public int LastPossibleValue()
        {
            if (PossibleValues.Count(x => x).Equals(1))
            {
                // return the last value
                for (int i = 0; i < 9; i++)
                {
                    if (PossibleValues[i])
                        return i + 1;
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }

    }
}
