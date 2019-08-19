using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new Validator();

            char input = '1';
            while (input == '1')
            {
                var game = new Game();
                var ui = new ConsoleInterface(game, validator);

                ui.Start();

                Console.WriteLine("\nPress 1 to play again or any other key to exit.");
                input = Console.ReadKey().KeyChar;
            }

        }
    }
}
