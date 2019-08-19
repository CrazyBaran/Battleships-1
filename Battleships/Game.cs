using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    public class Game
    {
        public Ship[] PlayerShips { get; set; }
        public Ship[] NPCShips { get; set; }

        public List<Shot> PlayerShots { get; set; }
        public List<Shot> NPCShots { get; set; }

        public bool IsFinished => PlayerShips.All(s => s.Destroyed) || NPCShips.All(s => s.Destroyed);
        public bool DidPlayerWin => NPCShips.All(s => s.Destroyed);

        public Game()
        {
            PlayerShips = GenerateShips(2, 1);
            NPCShips = GenerateShips(2, 1);

            PlayerShots = new List<Shot>();
            NPCShots = new List<Shot>();
        }

        /// <summary>
        /// Generates ships in random positions
        /// </summary>
        /// <param name="destroyers">Number of destroyers</param>
        /// <param name="battleships"> Number of battleships</param>
        /// <returns></returns>
        public Ship[] GenerateShips(int destroyers, int battleships)
        {
            List<Ship> ships = new List<Ship>();

            // Generate destroyers
            while (destroyers > 0)
            {
                Ship ship = GenerateShip("Destroyer", 4);

                if (!DoesCollide(ship, ships))
                {
                    ships.Add(ship);
                    destroyers--;
                }
            }

            // Generate battleships
            while (battleships > 0)
            {
                Ship ship = GenerateShip("Battleship", 5);

                if (!DoesCollide(ship, ships))
                {
                    ships.Add(ship);
                    battleships--;
                }
            }

            return ships.ToArray();
        }

        /// <summary>
        /// Checks if new ship is out of the grid or collides with other ships
        /// </summary>
        /// <param name="ship">New ship</param>
        /// <param name="ships">Existing ships</param>
        /// <returns></returns>
        public bool DoesCollide(Ship ship, List<Ship> ships)
        {
            var occupiedSquares = ships.SelectMany(s => s.HealthySquares);

            bool outOfGrid = ship.HealthySquares.Any(p => p.Col > 9 || p.Row > 9 || p.Col < 0 || p.Row < 0);
            bool collidesWithOtherShips = occupiedSquares.Intersect(ship.HealthySquares).Any();

            return outOfGrid || collidesWithOtherShips;
        }

        /// <summary>
        /// Generates ship with random position
        /// </summary>
        /// <param name="name">Ship name</param>
        /// <param name="length">Ship length</param>
        /// <returns></returns>
        public Ship GenerateShip(string name, int length)
        {
            if (length > 10)
            {
                throw new ArgumentException("Ship is too long to fit in the grid.");
            }

            Square startingSquare = GetRandomSquare();
            int startCol = startingSquare.Col;
            int startRow = startingSquare.Row;

            // Pick direction randomly
            Random rand = new Random();
            int direction = rand.Next(0, 10);

            Ship ship = new Ship(name);

            if (direction < 5)
            {
                // horizontal
                for (int i = 0; i < length; i++)
                {
                    ship.HealthySquares.Add(new Square(startCol + i, startRow));
                }
            }
            else
            {
                // vertical
                for (int i = 0; i < length; i++)
                {
                    ship.HealthySquares.Add(new Square(startCol, startRow + i));
                }
            }

            return ship;
        }

        /// <summary>
        /// Shoots given target
        /// </summary>
        /// <param name="targetSquare">Target square</param>
        /// <param name="player">Player flag: true - player; false - npc</param>
        /// <returns></returns>
        public Shot Shoot(Square? targetSquare, bool player)
        {
            Ship[] targetShips;
            List<Shot> shotsCollection;

            targetShips = player ? this.PlayerShips : this.NPCShips;
            shotsCollection = player ? this.PlayerShots : this.NPCShots;

            if (!targetSquare.HasValue)
            {
                var alreadyShotSquares = shotsCollection.Select(s => s.Square);

                Square randomSquare = GetRandomSquare();
                while (alreadyShotSquares.Contains(randomSquare))
                {
                    randomSquare = GetRandomSquare();
                }
                targetSquare = randomSquare;
            }

            ShotResultEnum shotResult = ShotResultEnum.Miss;
            foreach (var ship in targetShips)
            {
                if (ship.HealthySquares.Contains(targetSquare.Value))
                {
                    ship.Destroy(targetSquare.Value);
                    shotResult = ship.Destroyed ? ShotResultEnum.Sink : ShotResultEnum.Hit;
                    break;
                }
            }

            var shot = new Shot(targetSquare.Value, shotResult);

            shotsCollection.Add(shot);

            return shot;
        }

        /// <summary>
        /// Gets random sqare within 0-10 range
        /// </summary>
        /// <returns></returns>
        public Square GetRandomSquare()
        {
            Random rand = new Random();

            int col = rand.Next(0, 10);
            int row = rand.Next(0, 10);

            return new Square(col, row);
        }
    }
}
