using Battleships;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class GameTests
    {
        [Test]
        public void Game_OnInit_Generates2DestroyersForPlayer()
        {
            Game game = new Game();

            int destroyers = game.PlayerShips.Count(s => string.Equals(s.Type, "Destroyer"));

            Assert.AreEqual(2, destroyers);
        }

        [Test]
        public void Game_OnInit_Generates1BattleshipForPlayer()
        {
            Game game = new Game();

            int battleships = game.PlayerShips.Count(s => string.Equals(s.Type, "Battleship"));

            Assert.AreEqual(1, battleships);
        }

        [Test]
        public void Game_OnInit_Generates2DestroyersForNPC()
        {
            Game game = new Game();

            int destroyers = game.NPCShips.Count(s => string.Equals(s.Type, "Destroyer"));

            Assert.AreEqual(2, destroyers);
        }

        [Test]
        public void Game_OnInit_Generates1BattleshipForNPC()
        {
            Game game = new Game();

            int battleships = game.NPCShips.Count(s => string.Equals(s.Type, "Battleship"));

            Assert.AreEqual(1, battleships);
        }

        [Test]
        public void Game_OnInit_AllBattleshipsAre5Squares()
        {
            Game game = new Game();

            var playerBattleships = game.PlayerShips.Where(s => string.Equals(s.Type, "Battleship")).Select(bs => bs.Length);
            var npcBattleships = game.NPCShips.Where(s => string.Equals(s.Type, "Battleship")).Select(bs => bs.Length);

            var invalidShips = playerBattleships.Count(l => l != 5) + npcBattleships.Count(l => l != 5);

            Assert.AreEqual(0, invalidShips);
        }

        [Test]
        public void Game_OnInit_AllDestroyersAre4Squares()
        {
            Game game = new Game();

            var playerBattleships = game.PlayerShips.Where(s => string.Equals(s.Type, "Destroyer")).Select(bs => bs.Length);
            var npcBattleships = game.NPCShips.Where(s => string.Equals(s.Type, "Destroyer")).Select(bs => bs.Length);

            var invalidShips = playerBattleships.Count(l => l != 4) + npcBattleships.Count(l => l != 4);

            Assert.AreEqual(0, invalidShips);
        }

        [Test]
        public void DoesCollide_WithShip_Yes()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();
            ships.Add(new Ship("RegularShip")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1),
                    new Square(1,2),
                    new Square(1,3),
                    new Square(1,4)
                }
            });

            Ship collidingShip = new Ship("Collider")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,2),
                    new Square(2,2),
                    new Square(3,2),
                    new Square(4,2)
                }
            };

            bool doesCollide = game.DoesCollide(collidingShip, ships);

            Assert.IsTrue(doesCollide);
        }

        [Test]
        public void DoesCollide_WithShip_No()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();
            ships.Add(new Ship("RegularShip")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1),
                    new Square(1,2),
                    new Square(1,3),
                    new Square(1,4)
                }                 
            });

            Ship notCollidingShip = new Ship("NiceShip")
            {
                HealthySquares = new List<Square>
                {
                    new Square(2,1),
                    new Square(2,2),
                    new Square(2,3),
                    new Square(2,4)
                }
            };

            bool doesCollide = game.DoesCollide(notCollidingShip, ships);

            Assert.IsFalse(doesCollide);
        }

        [Test]
        public void DoesCollide_OutOfGrid_Yes()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();

            Ship collidingShip = new Ship("Out")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,10),
                    new Square(2,10),
                    new Square(3,10),
                    new Square(4,10)
                }
            };

            bool doesCollide = game.DoesCollide(collidingShip, ships);

            Assert.IsTrue(doesCollide);
        }

        [Test]
        public void DoesCollide_OutOfGrid_No()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();

            Ship notCollidingShip = new Ship("Inside")
            {
                HealthySquares = new List<Square>
                {
                    new Square(2,1),
                    new Square(2,2),
                    new Square(2,3),
                    new Square(2,4)
                }
            };

            bool doesCollide = game.DoesCollide(notCollidingShip, ships);

            Assert.IsFalse(doesCollide);
        }

        [Test]
        public void DoesCollide_OnTheEdge_No()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();

            Ship collidingShip = new Ship("Out")
            {
                HealthySquares = new List<Square>
                {
                    new Square(9,9),
                    new Square(8,9),
                    new Square(7,9),
                    new Square(5,9)
                }
            };

            bool doesCollide = game.DoesCollide(collidingShip, ships);

            Assert.IsFalse(doesCollide);
        }

        [Test]
        public void DoesCollide_NegativeRow_Yes()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();

            Ship collidingShip = new Ship("Out")
            {
                HealthySquares = new List<Square>
                {
                    new Square(9,-1),
                    new Square(8,-1),
                    new Square(7,-1),
                    new Square(5,-1)
                }
            };

            bool doesCollide = game.DoesCollide(collidingShip, ships);

            Assert.IsTrue(doesCollide);
        }

        [Test]
        public void DoesCollide_NegativeCol_Yes()
        {
            Game game = new Game();

            List<Ship> ships = new List<Ship>();

            Ship collidingShip = new Ship("Out")
            {
                HealthySquares = new List<Square>
                {
                    new Square(-1,9),
                    new Square(-1,8),
                    new Square(-1,7),
                    new Square(-1,5)
                }
            };

            bool doesCollide = game.DoesCollide(collidingShip, ships);

            Assert.IsTrue(doesCollide);
        }
    }
}