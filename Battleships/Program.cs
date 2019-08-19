using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var ui = new ConsoleInterface(game);

            ui.Start();

            Console.ReadKey();
        }
    }
}
