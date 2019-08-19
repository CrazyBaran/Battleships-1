using Battleships;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class ShootingTests
    {
        [Test]
        public void Shoot_ByPlayer_AddsToCollection()
        {
            Game game = new Game();
            game.PlayerShots = new List<Shot>();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            var shot = game.Shoot(new Square(1, 2), true);

            Assert.Contains(shot, game.PlayerShots);
        }

        [Test]
        public void Shoot_ByNPC_AddsToCollection()
        {
            Game game = new Game();
            game.NPCShots = new List<Shot>();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            var shot = game.Shoot(new Square(1, 2), false);

            Assert.Contains(shot, game.NPCShots);
        }

        [Test]
        public void Shoot_HitsValidSquare()
        {
            Game game = new Game();
            game.NPCShots = new List<Shot>();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1)
                }
            };

            var shot = game.Shoot(new Square(1, 2), true);

            Assert.AreEqual(new Square(1, 2), shot.Square);
        }

        [Test]
        public void Shoot_HitsTarget_ReturnsHit()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,1),
                    new Square(1,2),
                    new Square(1,3),
                    new Square(1,4)
                }
            };

            var shot = game.Shoot(new Square(1, 2), true);

            Assert.AreEqual(ShotResultEnum.Hit, shot.Result);
        }

        [Test]
        public void Shoot_HitsTarget_ReturnsSink()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,2),
                }
            };

            var shot = game.Shoot(new Square(1, 2), true);

            Assert.AreEqual(ShotResultEnum.Sink, shot.Result);
        }


        [Test]
        public void Shoot_MissesTarget_ReturnsMiss()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,2),
                }
            };

            var shot = game.Shoot(new Square(2, 2), true);

            Assert.AreEqual(ShotResultEnum.Miss, shot.Result);
        }

        [Test]
        public void Shoot_SinksTarget_ShipIsSinked()
        {
            Game game = new Game();

            game.PlayerShips = new Ship[1];
            game.PlayerShips[0] = new Ship("Dummy")
            {
                HealthySquares = new List<Square>
                {
                    new Square(1,2),
                },
                SinkedSquares = new List<Square>
                {
                    new Square(1,3),
                    new Square(1,4),
                    new Square(1,5),
                }
            };

            game.Shoot(new Square(1, 2), true);

            var sinkedShips = game.PlayerShips.Count(s => s.Destroyed);

            Assert.AreEqual(1, sinkedShips);
        }
    }
}