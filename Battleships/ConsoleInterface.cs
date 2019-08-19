using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Battleships
{
    public class ConsoleInterface
    {
        private Game _game;
        private Validator _validator;

        public ConsoleInterface(Game game, Validator validator)
        {
            _game = game;
            _validator = validator;
        }

        public void Start()
        {
            string input = String.Empty;
            while (!string.Equals(input.ToUpper(), "EXIT"))
            {
                if (_validator.ValidateCoordinates(input))
                {

                }

                Console.Clear();
                PrintBoards();

                PrintMenu();

                input = Console.ReadLine();
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nYour turn!\n");
            Console.WriteLine("To shoot enter column and row (in the form of A0) and press enter!");
            Console.WriteLine("To exit the game enter \"exit\" and press enter.\n");
        }

        private void PrintBoards()
        {
            // empty column
            string e = "   ";

            char healthySquare = '■';
            char hitSquare = '□';
            char miss = '○';

            string playerBoard = $"{e}|{e + e + e + e}Player Board{e + e + e + e + e}|";
            string enemyBoard = $"{e}|{e + e + e + e}Enemy Board {e + e + e + e + e}|";

            string columns = $"{e}| A | B | C | D | E | F | G | H | I | J |";
            string hLine = "---+---+---+---+---+---+---+---+---+---+---+";

            var playerHealthySquares = _game.PlayerShips.SelectMany(s => s.HealthySquares);
            var playerSinkedSquares = _game.PlayerShips.SelectMany(s => s.SinkedSquares);
            var enemyMisses = _game.NPCShots.Where(s => s.Result == ShotResultEnum.Miss).Select(s => s.Square);

            var enemyHealthySquares = _game.NPCShips.SelectMany(s => s.HealthySquares);
            var enemySinkedSquares = _game.NPCShips.SelectMany(s => s.SinkedSquares);
            var playerMisses = _game.PlayerShots.Where(s => s.Result == ShotResultEnum.Miss).Select(s => s.Square);

            Console.WriteLine($"{playerBoard}\t{enemyBoard}");
            Console.WriteLine($"{columns}\t{columns}");
            char mark;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{hLine}\t{hLine}");
                string playerLine = $" {i} ";
                string enemyLine = $" {i} ";
                for (int j = 0; j < 10; j++)
                {
                    var square = new Square(j, i);

                    mark = ' ';
                    if (playerHealthySquares.Contains(square))
                    {
                        mark = healthySquare;
                    }
                    else if (playerSinkedSquares.Contains(square))
                    {
                        mark = hitSquare;
                    }
                    else if (enemyMisses.Contains(square))
                    {
                        mark = miss;
                    }
                    playerLine += $"| {mark} ";

                    mark = ' ';
                    if (enemyHealthySquares.Contains(square))
                    {
                        mark = healthySquare;
                    }
                    else if (enemySinkedSquares.Contains(square))
                    {
                        mark = hitSquare;
                    }
                    else if (playerMisses.Contains(square))
                    {
                        mark = miss;
                    }
                    enemyLine += $"| {mark} ";
                }
                Console.WriteLine($"{playerLine}|\t{enemyLine}|");
            }
            Console.WriteLine($"{hLine}\t{hLine}");
        }
    }
}