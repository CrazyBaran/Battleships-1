using System;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var validator = new Validator();
            var ui = new ConsoleInterface(game, validator);

            ui.Start();
        }
    }
}
