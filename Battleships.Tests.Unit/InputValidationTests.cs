using Battleships;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class InputValidationTests
    {
        [Test]
        public void ValidateInput_Valid_Ok()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("A0");

            Assert.IsTrue(valid);
        }

        [Test]
        public void ValidateInput_Edge_Ok()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("J10");

            Assert.IsTrue(valid);
        }

        [Test]
        public void ValidateInput_LowerString_Ok()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("j10");

            Assert.IsTrue(valid);
        }

        [Test]
        public void ValidateInput_ColumnOutOfRange_Invalid()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("k10");

            Assert.IsFalse(valid);
        }

        [Test]
        public void ValidateInput_RowOutOfRange_Invalid()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("j20");

            Assert.IsFalse(valid);
        }

        [Test]
        public void ValidateInput_BothOutOfRange_Invalid()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("K20");

            Assert.IsFalse(valid);
        }

        [Test]
        public void ValidateInput_BadOrder_Invalid()
        {
            Validator validator = new Validator();

            bool valid = validator.ValidateCoordinates("2A");

            Assert.IsFalse(valid);
        }
    }
}