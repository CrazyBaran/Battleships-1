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
            Console.Clear();
            PrintBoards();
            string input = String.Empty;
            string playerMessage;
            string enemyMessage;
            string errorMessage;

            while (!string.Equals(input.ToUpper(), "EXIT"))
            {
                playerMessage = String.Empty;
                enemyMessage = String.Empty;
                errorMessage = String.Empty;

                if (_validator.ValidateCoordinates(input))
                {
                    var playerShot = _game.Shoot(new Square(input), true);

                    playerMessage = $"You shot square {input} with a {playerShot.Result}!";

                    var npcShot = _game.Shoot(null, false);
                    enemyMessage = $"Your enemy shot square {npcShot.Square.ToString()} with a {npcShot.Result}!";

                }
                else if (!string.IsNullOrEmpty(input))
                {
                    errorMessage = "Please select valid column.";
                }

                Console.Clear();
                PrintBoards();
                PrintLegend();

                PrintMessage(playerMessage.Length > 0 ? playerMessage + Environment.NewLine + enemyMessage : string.Empty);
                PrintMessage(errorMessage);


                if (_game.IsFinished)
                {
                    break;
                }

                PrintMenu();

                input = Console.ReadLine();
            }

            if (string.Equals(input.ToUpper(), "EXIT"))
            {
                return;
            }

            Console.WriteLine("\nGame Finished!");
            string finishMessage = _game.DidPlayerWin ? "Congratulations! You won!" : "Computer won! Good luck next time!";
            Console.WriteLine(finishMessage);
            Console.WriteLine();
            Console.WriteLine($"Player hits:{_game.PlayerShots.Count(s => s.Result == ShotResultEnum.Hit)}");
            Console.WriteLine($"Player sinks:{_game.PlayerShots.Count(s => s.Result == ShotResultEnum.Sink)}");
            Console.WriteLine();
            Console.WriteLine($"Computer hits:{_game.NPCShots.Count(s => s.Result == ShotResultEnum.Hit)}");
            Console.WriteLine($"Computer sinks:{_game.NPCShots.Count(s => s.Result == ShotResultEnum.Sink)}");


        }

        private void PrintMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine();
                Console.WriteLine(message);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private void PrintLegend()
        {
            Console.WriteLine("■ - Ship square");
            Console.WriteLine("X - Hit Ship square");
            Console.WriteLine("o - Missed shot");
        }

        private void PrintMenu()
        {
            Console.WriteLine("Enter coordinate to shoot (in the form of A0)");
            Console.WriteLine("Enter \"exit\" to exit the game\n");
            Console.Write("Your move:");
        }

        private void PrintBoards()
        {
            // empty column
            string e = "   ";

            char healthySquare = '■';
            char hitSquare = 'X';
            char miss = 'o';

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
                        mark = 's';
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