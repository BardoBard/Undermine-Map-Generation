using Map_Generator.Math;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class DirectionExtension
    {
        [Test]
        [TestCase(Direction.North, Direction.South)]
        [TestCase(Direction.South, Direction.North)]
        [TestCase(Direction.East, Direction.West)]
        [TestCase(Direction.West, Direction.East)]
        [TestCase(Direction.Up, Direction.Down)]
        [TestCase(Direction.Down, Direction.Up)]
        public void Opposite(Direction direction,
            Direction expected)
        {
            var actual = direction.Opposite();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(Direction.North, 0, 1)]
        [TestCase(Direction.South, 0, -1)]
        [TestCase(Direction.East, 1, 0)]
        [TestCase(Direction.West, -1, 0)]
        public void DirectionToVector(Direction direction, int x, int y)
        {
            var expected = new Vector2Int(x, y);
            var actual = direction.DirectionToVector();
            Assert.AreEqual(expected, actual);
        }
    }
}