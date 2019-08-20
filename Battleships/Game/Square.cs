using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public struct Square : IEquatable<Square>
    {
        public int Col;
        public int Row;

        public Square(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public Square(string ColRow)
        {
            ColRow = ColRow.ToUpper();

            var cols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int col = cols.IndexOf(ColRow[0]);
            if (col < 0)
            {
                throw new ArgumentOutOfRangeException($"There is no column {ColRow[0]}.");
            }

            if (!Int32.TryParse(ColRow.Substring(1), out int row))
            {
                throw new ArgumentOutOfRangeException($"There is no row {ColRow.Substring(1)}.");
            }

            Col = col;
            Row = row;
        }

        public bool Equals(Square other)
        {
            return this.Col == other.Col && this.Row == other.Row;
        }

        public override string ToString()
        {
            var cols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return cols[Col] + Row.ToString();
        }
    }
}