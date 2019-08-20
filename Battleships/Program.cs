using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new Validator();
            var game = new Game();
            var ui = new ConsoleInterface(game, validator);

            ui.Start();
        }
    }
}
