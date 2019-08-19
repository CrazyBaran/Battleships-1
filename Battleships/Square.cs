using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public struct Square :IEquatable<Square>
    {
        public int Col;
        public int Row;

        public Square(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public bool Equals(Square other)
        {
            return this.Col == other.Col && this.Row == other.Row;
        }
    }
}