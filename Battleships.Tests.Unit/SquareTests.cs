using Battleships;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class SquareTests
    {
        [Test]
        public void Square_WithEqual_IsEqual()
        {
            var square1 = new Square(1, 1);
            var square2 = new Square(1, 1);

            Assert.AreEqual(square1, square2);
        }

        [Test]
        public void Square_WithOther_AreNotEqual()
        {
            var square1 = new Square(1, 1);
            var square2 = new Square(1, 2);

            Assert.AreNotEqual(square1, square2);
        }

        [Test]
        public void Square_StringCtor_GetsValidSquare()
        {
            var square1 = new Square("A0");
            var square2 = new Square(0, 0);

            Assert.AreEqual(square1.Col, square2.Col);
            Assert.AreEqual(square1.Row, square2.Row);
        }
        [Test]
        public void Square_StringCtorWithLongerRow_GetsValidSquare()
        {
            var square1 = new Square("A10");
            var square2 = new Square(0, 10);

            Assert.AreEqual(square1.Col, square2.Col);
            Assert.AreEqual(square1.Row, square2.Row);
        }
    }
}