using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    public class Ship
    {
        public List<Square> HealthySquares;
        public string Type;
        public List<Square> SinkedSquares;

        public Ship(string type)
        {
            this.Type = type;
            this.HealthySquares = new List<Square>();
            this.SinkedSquares = new List<Square>();
        }

        public bool Destroyed => HealthySquares.Count == 0;
        public int Length => HealthySquares.Count + SinkedSquares.Count;

        public void Destroy(Square square)
        {
            var hit = HealthySquares.Remove(square);

            if (hit)
            {
                SinkedSquares.Add(square);
            }
        }
    }
}
