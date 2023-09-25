using System;
using System.Collections.Generic;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Vector2IntTest
    {
        [Test]
        [TestCase(0, 0, 0, 0, true)]
        [TestCase(1, 1, 1, 1, true)]
        [TestCase(1, 1, 0, 0, false)]
        [TestCase(0, 0, 1, 1, false)]
        [TestCase(1, 0, 0, 1, false)]
        [TestCase(0, 1, 1, 0, false)]
        [TestCase(1, 0, 1, 0, true)]
        [TestCase(0, 1, 0, 1, true)]
        [TestCase(1, 0, 0, 0, false)]
        [TestCase(-24, 1, -3, 54, false)]
        [TestCase(-24, 1, -24, 1, true)]
        public void TestEquals(int x1, int y1, int x2, int y2, bool expected)
        {
            var a = new Vector2Int(x1, y1);
            var b = new Vector2Int(x2, y2);
            Assert.AreEqual(expected, a == b);
        }

        [Test]
        public void TestObjectEquals()
        {
            var a = new Vector2Int(-1, 1);
            var b = new Vector2Int(1, -1);
            Assert.IsTrue(a.Equals(a));
            Assert.IsFalse(b.Equals(a));
            Assert.IsFalse(a.Equals(b));
            Assert.AreSame(a, a);
            Assert.AreNotSame(a, b);
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 2, 2)]
        [TestCase(1, 1, 0, 0, 1, 1)]
        [TestCase(0, 0, 1, 1, 1, 1)]
        [TestCase(1, 0, 0, 1, 1, 1)]
        [TestCase(-1, 0, 0, 1, -1, 1)]
        [TestCase(0, -1, 0, 1, 0, 0)]
        [TestCase(0, 0, -1, 1, -1, 1)]
        [TestCase(1000000, 0, 0, 1, 1000000, 1)]
        [TestCase(0, 1000000, 0, 1, 0, 1000001)]
        [TestCase(53463, 7884, 1236, 423, 54699, 8307)]
        [TestCase(53463, 7884, -1236, 423, 52227, 8307)]
        public void Add(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var b = new Vector2Int(x2, y2);
            var expected = new Vector2Int(expectedX, expectedY);
            Assert.AreEqual(expected, a + b);
        }
        [Test]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 2, 2)]
        [TestCase(1, 1, 0, 1, 1)]
        [TestCase(0, 0, 1, 1, 1)]
        [TestCase(1, 0, 0, 1, 0)]
        [TestCase(-1, 0, 1, 0, 1)]
        [TestCase(0, -1, 1, 1, 0)]
        [TestCase(0, 0, -1, -1, -1)]
        [TestCase(1000000, 0, 1, 1000001, 1)]
        [TestCase(53463, 7884, 1236, 54699, 9120)]
        [TestCase(53463, 7884, -1236, 52227, 6648)]
        public void Add(int x1, int y1, int n, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var expected = new Vector2Int(expectedX, expectedY);
            Assert.AreEqual(expected, a + n);
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 0, 0)]
        [TestCase(1, 1, 0, 0, 1, 1)]
        [TestCase(0, 0, 1, 1, -1, -1)]
        [TestCase(1, 0, 0, 1, 1, -1)]
        [TestCase(-1, 0, 0, 1, -1, -1)]
        [TestCase(0, -1, 0, 1, 0, -2)]
        [TestCase(0, 0, -1, 1, 1, -1)]
        [TestCase(1000000, 0, 0, 1, 1000000, -1)]
        [TestCase(0, 1000000, 0, 1, 0, 999999)]
        [TestCase(53463, 7884, 1236, 423, 52227, 7461)]
        [TestCase(53463, 7884, -1236, 423, 54699, 7461)]
        public void Subtract(int x1, int y1, int x2, int y2, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var b = new Vector2Int(x2, y2);
            var expected = new Vector2Int(expectedX, expectedY);
            Assert.AreEqual(expected, a - b);
        }
        
        [Test]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 0, 0)]
        [TestCase(1, 1, 0, 1, 1)]
        [TestCase(0, 0, 1, -1, -1)]
        [TestCase(1, 0, -1, 2, 1)]
        [TestCase(-1, 0, 0, -1, 0)]
        [TestCase(0, -1, 0, 0, -1)]
        [TestCase(0, 0, -1,  1, 1)]
        [TestCase(1000000, 0, -1,  1000001, 1)]
        [TestCase(0, 1000000, 0, 0, 1000000)]
        [TestCase(53463, 7884, 1236, 52227, 6648)]
        [TestCase(53463, 7884, -1236, 54699, 9120)]
        public void Subtract(int x1, int y1, int n, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var expected = new Vector2Int(expectedX, expectedY);
            Assert.AreEqual(expected, a - n);
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 1, 0, 0, 0)]
        [TestCase(-1, 1, 1, -1, 1)]
        [TestCase(1, 0, 1, 1, 0)]
        [TestCase(-1, 0, 1, -1, 0)]
        [TestCase(0, -1, -1, 0, 1)]
        [TestCase(0, 0, -1, 0, 0)]
        [TestCase(6457534, 747453, -234, -1511062956, -174904002)]
        [TestCase(0, -53, -2, 0, 106)]
        public void Multiply(int x1, int y1, int n, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var expected = new Vector2Int(expectedX, expectedY);
            Assert.AreEqual(expected, a * n);
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 1, 0, 0, 0)]
        [TestCase(0, 0, 1, 0, 0)]
        [TestCase(1, 0, 0, 0, 0)]
        [TestCase(-1, 0, 0, 0, 0)]
        [TestCase(0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, 0, 0)]
        [TestCase(1000000, 0, 0, 1000000, 0)]
        [TestCase(0, 1000000, 0, 0, 0)]
        [TestCase(53463, 7884, 1236, 43, 6)]
        [TestCase(53463, 7884, -1236, -43, -6)]
        public void Divide(int x1, int y1, int n, int expectedX, int expectedY)
        {
            var a = new Vector2Int(x1, y1);
            var expected = new Vector2Int(expectedX, expectedY);

            if (n == 0)
            {
                Assert.Throws<DivideByZeroException>(() => _ = a / n);
                return;
            }

            Assert.AreEqual(expected, a / n);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0,1)]
        [TestCase(1, 1)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        [TestCase(1000000, 1000000)]
        [TestCase(-1000000, -1000000)]
        [TestCase(53463, 7884)]
        [TestCase(-53463, -7884)]
        [TestCase(-123, 456)]
        [TestCase(123, -456)]
        public void Negative(int x1, int y1)
        {
            var a = new Vector2Int(x1, y1);
            var expected = new Vector2Int(-x1, -y1);
            Assert.AreEqual(expected, -a);
        }
        [Test]
        public void StaticValues()
        {
            Assert.AreEqual(new Vector2Int(0, 0), Vector2Int.Zero);
            Assert.AreEqual(new Vector2Int(1, 1), Vector2Int.One);
            Assert.AreEqual(new Vector2Int(0, 1), Vector2Int.Up);
            Assert.AreEqual(new Vector2Int(0, -1), Vector2Int.Down);
            Assert.AreEqual(new Vector2Int(-1, 0), Vector2Int.Left);
            Assert.AreEqual(new Vector2Int(1, 0), Vector2Int.Right);
        }
    }
}