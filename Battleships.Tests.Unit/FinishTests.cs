using Battleships;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class FinishTests
    {
        [Test]
        public void IsFinished_PlayerAndNPCHaveShips_No()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            game.NPCShips = new Ship[1];
            game.NPCShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            Assert.IsFalse(game.IsFinished);
        }

        [Test]
        public void IsFinished_PlayerLostAllShips_Yes()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Sinked")
            {
                SinkedSquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            game.NPCShips = new Ship[1];
            game.NPCShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            Assert.IsTrue(game.IsFinished);
        }

        [Test]
        public void IsFinished_NPCLostAllShips_Yes()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            game.NPCShips = new Ship[1];
            game.NPCShips[0] = new Ship("Sinked")
            {
                SinkedSquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            Assert.IsTrue(game.IsFinished);
        }
    }
}