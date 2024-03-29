﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Puzzle
    {
        public PuzzleValue[,] values { get; set; }

        private Stack<(int, int, int)> suggestions;
        private Queue<(int, int, int)> playedValues;

        /// <summary>
        /// Shows the depth of the brute force 
        /// </summary>
        public int Depth { get; set; } = 0;


        public bool IsSolved
        {
            get
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (values[i, j].Value == string.Empty)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public Puzzle(bool LoadFromFile) 
        { 
            values = new PuzzleValue[9,9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    values[i, j] = new PuzzleValue();
            }

            suggestions = new Stack<(int, int, int)>();
            playedValues = new Queue<(int, int, int)>();

            if (LoadFromFile)
            {
                LoadPlayedValues();
            }
        }

        public void SavePlayedValues()
        {
            string json = JsonConvert.SerializeObject(playedValues);
            File.WriteAllText("playQueue.json", json);
        }

        private void LoadPlayedValues()
        {
            try
            {
                string json = File.ReadAllText("playQueue.json");
                playedValues = JsonConvert.DeserializeObject<Queue<(int, int, int)>>(json);
                foreach (var value in playedValues)
                {                    
                    this.SetValue(value.Item1, value.Item2, value.Item3, true, true);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Copy the puzzle
        /// </summary>
        /// <returns>The new puzzle</returns>
        private Puzzle Copy()
        {
            Puzzle p = new Puzzle(false);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    p.values[i, j] = values[i, j].Copy();
            }
            p.Depth = Depth;
            return p;
        }

        /// <summary>
        /// Automatically solve the puzzle
        /// </summary>
        /// <returns>true on success</returns>
        public bool SolvePuzzle()
        {
            while (GenerateSuggestions())
            {
                PlaySuggestion();
            }
            if (!IsSolved)
            {
                // create list of count of possible values for each element
                List<(int, int, int)> possibleValuesCount = new List<(int, int, int)>();
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (values[i, j].PossibleValues.Count(x => x) > 1)
                        {
                            // todo voeg opties toe niet count
                            possibleValuesCount.Add((i, j, values[i, j].PossibleValues.Count(x => x)));
                        }
                    }
                }

                // probeer slechts de opties van 1 punt, dat zou de oplossing moeten zijn
                var val = possibleValuesCount.OrderBy(x => x.Item3).ThenBy(y => y.Item1).ThenBy(z => z.Item2).First();
                var possibleValues = values[val.Item1, val.Item2].ShowPossibleValues().Replace(" ", "");

                bool subSolved = false;
                int index = 0;
                while (!subSolved && index < possibleValues.Count())
                {
                    Puzzle subPuzzle = Copy();
                    subPuzzle.Depth += 1;
                    subPuzzle.SetValue(val.Item1, val.Item2, int.Parse(possibleValues[index].ToString()), true, true);
                    try
                    {
                        if (subPuzzle.SolvePuzzle())
                        {
                            this.values = subPuzzle.values;
                            this.Depth = subPuzzle.Depth;
                            subSolved = true;
                        }

                    }
                    catch (Exception)
                    {
                        index++;
                    }
                }
            }
            return IsSolved;
        }


        /// <summary>
        /// Generate a list of suggestions to play for the puzzle
        /// </summary>
        /// <returns></returns>
        public bool GenerateSuggestions()
        {
            suggestions = new Stack<(int, int, int)>();

            // check for each empty value if there is only one possible value left
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (values[i, j].Value == string.Empty)
                    {
                        int last = values[i, j].LastPossibleValue();
                        if (last != -1)
                        {
                            suggestions.Push((i, j, last));
                        }
                    }
                }
            }

            // only if there are no suggestions do the next step
            if (suggestions.Count == 0)
            {
                // voor elke regel
                for (int line = 0; line < 9; line++)
                {
                    // voor elke kolom
                    for (int column = 0; column < 9; column++)
                    {
                        // als het element nog niet gezet is
                        if (values[line, column].Value == string.Empty)
                        {
                            // check if there is a value that is only possible in this element
                            // doorloop de waarden van 1 tot 9
                            for (int pos = 0; pos < 9; pos++)
                            {
                                // indien de waarde nog mogelijk is voor dit element en zolang niets gevonden          
                                if (values[line, column].PossibleValues[pos] == true)
                                {
                                    bool only = true;
                                    // kijk in de sectie of er een ander element is waar deze waarde ook mogelijk is
                                    int xStart = line / 3 * 3;
                                    int yStart = column / 3 * 3;
                                    for (int l = xStart; l < xStart + 3; l++)
                                    {
                                        for (int m = yStart; m < yStart + 3; m++)
                                        {
                                            // indien het niet de huidige positie is en de waarde nog mogelijk is in de de verifieren positie
                                            if (only && !(l == line && m == column) && values[l, m].PossibleValues[pos])
                                            {
                                                only = false;
                                            }
                                        }
                                    }
                                    if (only)
                                    {
                                        suggestions.Push((line, column, pos + 1));
                                    }
           
                                    // kijk in de kolom of er een ander element is waar deze waarde ook mogelijk is
                                    // check column
                                    only = true;
                                    for (int lineToCheck = 0; lineToCheck < 9; lineToCheck++)
                                    {
                                        if (only && lineToCheck != line && values[lineToCheck, column].PossibleValues[pos])
                                        {
                                            only = false;
                                        }
                                    }
                                    if (only)
                                    {
                                        suggestions.Push((line, column, pos + 1));
                                    }
           
                                    // kijk in de rij of er een ander element is waar deze waarde ook mogelijk is
                                    // check line
                                    only = true;
                                    for (int colToCheck = 0; colToCheck < 9; colToCheck++)
                                    {
                                        if (only && colToCheck != column && values[line, colToCheck].PossibleValues[pos])
                                        {
                                            only = false;
                                        }
                                    }
                                    if (only)
                                    {
                                        suggestions.Push((line, column, pos + 1));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return suggestions.Count > 0;
        }

        public void PlaySuggestion()
        {
            while (suggestions.Count > 0)
            {
                var suggestion = suggestions.Pop();
                SetValue(suggestion.Item1, suggestion.Item2, suggestion.Item3, true, true);
            }
        }

        public void Print(bool showPossibleValues)
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
                if (showPossibleValues)
                {
                    Console.WriteLine(lineValues);
                }
            }

            Console.WriteLine("Suggestions:");
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine($"x: {suggestion.Item1+1}; y: {suggestion.Item2+1}; val: {suggestion.Item3}");
            }

            Console.WriteLine("\nRij is x, kolom is y");
        }

        /// <summary>
        /// Set the value in the puzzle to be definite
        /// </summary>
        /// <param name="x">row</param>
        /// <param name="y">line</param>
        /// <param name="value">The value to set</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetValue(int x, int y, int value, bool skipEnqueue = false, bool skipGenerateSuggestions = false)
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

            if (!skipEnqueue)
                playedValues.Enqueue((x, y, value));

            if (!skipGenerateSuggestions)
                GenerateSuggestions();
        }
    }
}
