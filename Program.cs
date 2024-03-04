
namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Puzzle p = new Puzzle();
                        
            do
            {
                p.Print();
                Console.WriteLine("Enter row: ");
                int x = int.Parse(Console.ReadLine())-1;
                Console.WriteLine("Enter column: ");
                int y = int.Parse(Console.ReadLine())-1;
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

            } while (true);

        }
    }
}
