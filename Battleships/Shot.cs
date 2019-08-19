using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    public class Shot
    {
        public ShotResultEnum Result { get; set; }
        public Square Square { get; set; }

        public Shot(Square square, ShotResultEnum result)
        {
            Square = square;
            Result = result;
        }
    }
}
