using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class Ship
    {
        public List<int[]> HealthySquares;
        public string Type;
        public List<int[]> SinkedSquares;

        public Ship(string type)
        {
            this.Type = type;
            this.HealthySquares = new List<int[]>();
            this.SinkedSquares = new List<int[]>();
        }

        public bool Destroyed => HealthySquares.Count == 0;
        public int Length => HealthySquares.Count + SinkedSquares.Count;
    }
}
