using System.Collections.Generic;
using Map_Generator.Undermine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ListExtensions
    {
        [Test]
        public void Shuffle()
        {
            const uint seed = 1234u;
            List<int> expected = new() { 5, 3, 2, 1, 4 };

            Map_Generator.Undermine.Rand.Initialize(seed);
            List<int> list = new() { 1, 2, 3, 4, 5 };
            list.Shuffle();

            Assert.AreEqual(expected, list);
        }

        [Test]
        public void Shuffle2()
        {
            const uint seed = 4321u;
            List<int> expected = new() { 3, 1, 5, 2, 4 };

            Map_Generator.Undermine.Rand.Initialize(seed);
            List<int> list = new() { 5, 3, 2, 1, 4 };
            list.Shuffle();

            Assert.AreEqual(expected, list);
        }

        [Test]
        public void Shuffle3()
        {
            const uint seed = 654321u;
            List<int> expected = new()
            {
                64, 7, 3, 6, 8, 6, 4, 6, 9, 3, 3, 9, 2, 3, 26, 8, 3, 75, 234, 6, 3, 2, 2, 2, 6, 7, 2, 2, 4, 1,
                2, 8, 7
            };

            Map_Generator.Undermine.Rand.Initialize(seed);
            List<int> list = new()
            {
                2, 3, 6, 7, 8, 2, 1, 6, 7, 8, 4, 2, 6, 8, 9, 3, 2, 26, 6, 7, 3, 2, 64, 9, 4, 2, 3, 6, 75, 3, 3, 2, 234
            };
            list.Shuffle();

            Assert.AreEqual(expected, list);
        }
    }
}