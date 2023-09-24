using System.Collections.Generic;
using Map_Generator.Undermine;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class ListExtensions
    {
        [Test]
        [TestCase(1234u, new[] { 1, 2, 3, 4, 5 }, new[] { 5, 3, 2, 1, 4 })]
        [TestCase(4321u, new[] { 5, 3, 2, 1, 4 }, new[] { 3, 1, 5, 2, 4 })]
        [TestCase(654321u,
            new[]
            {
                2, 3, 6, 7, 8, 2, 1, 6, 7, 8, 4, 2, 6, 8, 9, 3, 2, 26, 6, 7, 3, 2, 64, 9, 4, 2, 3, 6, 75, 3, 3, 2, 234
            }, new[]
            {
                64, 7, 3, 6, 8, 6, 4, 6, 9, 3, 3, 9, 2, 3, 26, 8, 3, 75, 234, 6, 3, 2, 2, 2, 6, 7, 2, 2, 4, 1,
                2, 8, 7
            })]
        public void Shuffle<T>(uint seed, IList<T> list, IList<T> expected)
        {
            Assert.AreEqual(list.Count, expected.Count);

            Map_Generator.Undermine.Rand.Initialize(seed);
            list.Shuffle();

            Assert.AreEqual(expected, list);
        }
    }
}