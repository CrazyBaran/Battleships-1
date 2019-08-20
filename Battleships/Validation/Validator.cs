using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Battleships
{
    public class Validator
    {
        public bool ValidateCoordinates(string input)
        {
            Regex rgx = new Regex("^([a-jA-J])([0-9]|10)$");

            return rgx.IsMatch(input);
        }
    }
}