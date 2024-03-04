

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sudoku solver!");

            Puzzle p = new Puzzle(false);

            bool running = true;
            while (running)
            {
                p.Print();
                switch (ShowMenu())
                {
                    case 1:
                        p = new Puzzle(false);
                        break;
                    case 2:
                        // load last puzzle
                        p = new Puzzle(true);
                        break;
                    case 3:
                        // manual input
                        bool continueInput = true;
                        while (continueInput)
                        {
                            p.Print();
                            Console.WriteLine("Enter row (negative value to stop): ");
                            int x = int.Parse(Console.ReadLine()) - 1;
                            if (x < 0)
                            {
                                continueInput = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter column: ");
                                int y = int.Parse(Console.ReadLine()) - 1;
                                Console.WriteLine("Enter value: ");
                                int value = int.Parse(Console.ReadLine());
                                bool go = true;
                                if (value < 1 || value > 9)
                                {
                                    Console.WriteLine("Invalid value");
                                    go = false;
                                }
                                if (x < 0 || x > 8 || y < 0 || y > 8)
                                {
                                    Console.WriteLine("Invalid row or column");
                                    go = false;
                                }
                                if (go)
                                {
                                    p.SetValue(x, y, value);
                                }
                            }
                        }
                        break;


                    case 4: 
                        // play suggestions
                        while (p.GenerateSuggestions())
                        {
                            p.PlaySuggestion();
                        }
                        break;
                    case 9:
                        // einde
                        running = false;
                        break;
                    default:
                        break;
                }


            }
        }


        /// <summary>
        /// Console menu
        /// </summary>
        /// <returns></returns>
        private static int ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Nieuwe lege puzzle");
            Console.WriteLine("2. Laatste puzzle laden");
            Console.WriteLine("3. Handmatig punten ingeven");
            Console.WriteLine("4. Suggesties spelen");
            Console.WriteLine("9. Einde");
            Console.WriteLine();
            Console.WriteLine("Maak een keuze: ");

            try 
            {
                return int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                return ShowMenu();
            }            
        }
    }
}
