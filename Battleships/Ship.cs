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
        public bool Destroyed => HealthySquares.Count == 0;
        public int Length => HealthySquares.Count + SinkedSquares.Count;
    }
}
