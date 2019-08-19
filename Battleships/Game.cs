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

        public Game()
        {
            PlayerShips = GenerateShips(2, 1);
            NPCShips = GenerateShips(2, 1);
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

            Random rand = new Random();

            int startCol = rand.Next(0, 10);
            int startRow = rand.Next(0, 10);

            // Pick direction randomly
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
        /// <param name="target">Target square</param>
        /// <param name="player">Player flag: true - player; false - npc</param>
        /// <returns></returns>
        public Shot Shoot(Square target, bool player)
        {
            throw new NotImplementedException();
        }
    }
}
