using System;
using System.Collections.Generic;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Rand
    {
        [Test]
        [TestCase(123456u, new[] { 123456u, 848462657u, 1590858150u, 479153279u })]
        [TestCase(999999999u, new[] { 99999999u, 2858630044u, 3310420109u, 2988158114u })]
        [TestCase(0u, new[] { 0u, 1u, 1812433254u, 1900727103u })]
        public void Initialize(uint seed, Array expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            var result = Map_Generator.Undermine.Rand.Seeds[Map_Generator.Undermine.Rand.StateType.Default];

            //result
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Scope()
        {
            const uint seed = 123456u;
            const Map_Generator.Undermine.Rand.StateType expected = Map_Generator.Undermine.Rand.StateType.Layout;
            const Map_Generator.Undermine.Rand.StateType expected2 = Map_Generator.Undermine.Rand.StateType.Default;

            Map_Generator.Undermine.Rand.Initialize(seed);
            using (new Map_Generator.Undermine.Rand.Scope(Map_Generator.Undermine.Rand.StateType.Layout))
            {
                var result = Map_Generator.Undermine.Rand.GetCurrentScope();
                Assert.AreEqual(expected, result);
            }

            var result2 = Map_Generator.Undermine.Rand.GetCurrentScope();
            Assert.AreEqual(expected2, result2);
        }

        [Test]
        public void Scope2()
        {
            const uint seed = 87654321u;
            const Map_Generator.Undermine.Rand.StateType expected = Map_Generator.Undermine.Rand.StateType.Layout;
            const Map_Generator.Undermine.Rand.StateType expected2 = Map_Generator.Undermine.Rand.StateType.Prayer;
            const Map_Generator.Undermine.Rand.StateType expected3 = Map_Generator.Undermine.Rand.StateType.Default;

            Map_Generator.Undermine.Rand.Initialize(seed);
            using (new Map_Generator.Undermine.Rand.Scope(Map_Generator.Undermine.Rand.StateType.Layout))
            {
                var result = Map_Generator.Undermine.Rand.GetCurrentScope();
                Assert.AreEqual(expected, result);
                using (new Map_Generator.Undermine.Rand.Scope(Map_Generator.Undermine.Rand.StateType.Prayer))
                {
                    var result2 = Map_Generator.Undermine.Rand.GetCurrentScope();
                    Assert.AreEqual(expected2, result2);
                }
            }

            var result3 = Map_Generator.Undermine.Rand.GetCurrentScope();
            Assert.AreEqual(expected3, result3);
        }


        [Test]
        [TestCase(123456u,328449612u)]
        [TestCase(87654321u,2250309519u)]
        [TestCase(9345u,2940916796u)]
        [TestCase(4278345u,3684289151u)]
        [TestCase(8234u,2540998805u)]
        [TestCase(845235u,1035656343u)]
        [TestCase(23467u,466276756u)]
        [TestCase(85634252u,3618699448u)]
        public void NextUint(uint seed,uint expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.NextUInt();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(87654321u,468106955u, 10000u)]
        [TestCase(634637u,2155268432u, 12345u)]
        [TestCase(90646u,2849628341u, 2u)]
        [TestCase(86823234u,3475001881u, 753u)]
        public void NextUintLoop(uint seed,uint expected, uint max)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);

            uint actual = 0;
            for (var i = 0; i < max; i++)
                actual = Map_Generator.Undermine.Rand.NextUInt();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(12398746u, 3228673587u)]
        [TestCase(18746u, 2367945926u)]
        [TestCase(6762u, 3206317567u)]
        [TestCase(987u, 2475376425u)]
        public void PeekNextUInt(uint seed, uint expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);

            Map_Generator.Undermine.Rand.NextUInt();
            Map_Generator.Undermine.Rand.PeekNextUInt();
            Map_Generator.Undermine.Rand.NextUInt();
            Map_Generator.Undermine.Rand.PeekNextUInt();
            Map_Generator.Undermine.Rand.NextUInt();
            Map_Generator.Undermine.Rand.PeekNextUInt();

            uint actual = Map_Generator.Undermine.Rand.PeekNextUInt();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(87654321u, 2250309519u)]
        [TestCase(457234u, 1129168036u)]
        [TestCase(862345u, 1453339741u)]
        [TestCase(2341u, 993600154u)]
        [TestCase(8242u, 3978126312u)]
        public void PeekNextUInt2(uint seed, uint expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);

            var actual = Map_Generator.Undermine.Rand.PeekNextUInt();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(123456u, 0u, 250u, 211.43881225585938f)]
        [TestCase(123456u, 0u, 100u, 84.575523376464844f)]
        [TestCase(54353u, 654u, 4567u, 2046.8533935546875f)]
        [TestCase(23482u, 123u, 456u, 167.31465148925781f)]
        [TestCase(756354u, 2u, 7u, 2.9482026100158691f)]
        [TestCase(12356774u, 4u, 12u, 10.12775993347168f)]
        [TestCase(512893u, 0u, 1u, 0.21798837184906006f)]
        [TestCase(7345u, 0u, 0u, 0)]
        [TestCase(567123u, 7u, 29u, 24.6774712f)]
        public void RangeFloat(uint seed, uint min, uint max, float expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.RangeFloat(min, max); //0,1 is default
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void RangeFloatThrows()
        {
            const uint seed = 567123u;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.RangeFloat(29, 7));
        }

        [Test]
        [TestCase(567123u, 0.19647860527038574f)]
        [TestCase(12398746u, 0.0239715576171875f)]
        [TestCase(4234u, 0.26319098472595215f)]
        [TestCase(87654321u, 0.25779891014099121f)]
        [TestCase(6475234u, 0.93906128406524658f)]
        [TestCase(645678234u, 0.98971831798553467f)]
        public void Value(uint seed, float expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Value();
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        [TestCase(12398746u, 0.33404195308685303f, 1_000_000u)]
        [TestCase(4234u, 0.95212173461914063f, 1_000_000u)]
        public void ValueLoop(uint seed, float expected, uint max)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);

            float actual = 0;
            for (int i = 0; i < max; i++)
                actual = Map_Generator.Undermine.Rand.Value();


            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        [TestCase(12398746u, 0.88788437843322754f)]
        [TestCase(4234u, 0.60884082317352295f)]
        [TestCase(87654321u, 0.50034165382385254f)]
        public void PeekValue(uint seed, float expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);

            Map_Generator.Undermine.Rand.Value();
            Map_Generator.Undermine.Rand.PeekValue();
            Map_Generator.Undermine.Rand.Value();
            Map_Generator.Undermine.Rand.PeekValue();
            Map_Generator.Undermine.Rand.Value();
            Map_Generator.Undermine.Rand.PeekValue();
            float actual = Map_Generator.Undermine.Rand.PeekValue();


            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        [TestCase(12398746u, 2u, 7u, 7)]
        [TestCase(4234u, 2u, 7u, 7)]
        [TestCase(87654321u, 2u, 7u, 5)]
        [TestCase(87654321u, 0u, 2u, 0)]
        [TestCase(87654321u, 0u, 0u, 0)]
        [TestCase(8761u, 0u, 1u, 0)]
        [TestCase(874321u, 0u, 3u, 0)]
        [TestCase(8654321u, 60u, 423u, 108)]
        [TestCase(87321u, 60u, 423u, 192)]
        public void RangeInclusiveUint(uint seed, uint min, uint max, int expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(12398746u, 2, 7, 7)]
        [TestCase(4234u, 2, 7, 7)]
        [TestCase(87654321u, 2, 7, 5)]
        [TestCase(87654321u, 0, 2, 0)]
        [TestCase(87654321u, 0, 0, 0)]
        [TestCase(8761u, 0, 1, 0)]
        [TestCase(874321u, 0, 3, 0)]
        [TestCase(8654321u, 60, 423, 108)]
        [TestCase(87321u, 60, 423, 192)]
        public void RangeInclusiveInt(uint seed, int min, int max, int expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase(1234u, 0u, 103u, 88)]
        [TestCase(588672u, 11u, 12u, 11)]
        [TestCase(86783426u, 1u, 8u, 1)]
        [TestCase(4231u, 0u, 1u, 0)]
        public void RangUint(uint seed, uint min, uint max, int expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeUintThrows()
        {
            const uint seed = 4231u;
            const uint min = 1;
            const uint max = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        [TestCase(1234u, 0, 103, 88)]
        [TestCase(588672u, 11, 12, 11)]
        [TestCase(86783426u, 1, 8, 1)]
        [TestCase(4231u, 0, 1, 0)]
        public void RangeInt(uint seed, int min, int max, int expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeIntThrows()
        {
            const uint seed = 4231u;
            const int min = 1;
            const int max = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        [TestCase(1234u, 0.5f, false)]
        [TestCase(588672u, 0.5f, false)]
        [TestCase(86783426u, 0.5f, false)]
        [TestCase(7654221u, 0.5f, false)]
        [TestCase(4231u, 0.0f, false)]
        [TestCase(4231u, 1.0f, true)]
        [TestCase(4231u, 0.2f, true)]
        public void Chance(uint seed, float chance, bool expected)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Chance(chance);
            Assert.AreEqual(expected, actual);
        }


        [Test]
        [TestCase(221u, 2.0f)]
        [TestCase(221u, 7.0f)]
        [TestCase(1234u, -7.0f)]
        [TestCase(1234u, -7.0f)]
        public void ChanceThrows(uint seed, float chance)
        {
            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Chance(chance));
        }

        [Test]
        public void GetWeightedElement()
        {
            const uint seed = 1234u;
            var elements = new List<Map_Generator.Parsing.Json.Classes.Encounter.WeightedDoor>
            {
                new()
                {
                    Weight = 5,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 5,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 2,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 7,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 2,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 8,
                    Skip = false,
                    Door = Door.None
                },
            };
            var expected = elements[4];

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.GetWeightedElement(elements, out var result);
            Assert.IsTrue(actual);
            Assert.AreEqual(expected, result);
            Assert.IsTrue(result.Skip);
        }

        [Test]
        public void GetWeightedElement2()
        {
            const uint seed = 8434436u;

            var elements = new List<Map_Generator.Parsing.Json.Classes.Encounter>
            {
                new()
                {
                    Weight = 9,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 20,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 7,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = false,
                    Door = Door.None
                },
                new()
                {
                    Weight = 35,
                    Skip = false,
                    Door = Door.None
                },
            };
            var expected = elements[6];

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.GetWeightedElement(elements, out var result, false);
            Assert.IsTrue(actual);
            Assert.AreEqual(expected, result);
            Assert.IsFalse(result.Skip);
        }

        [Test]
        public void GetWeightedElement3()
        {
            const uint seed = 8434436u;

            var elements = new List<Map_Generator.Parsing.Json.Classes.Encounter>
            {
                new()
                {
                    Weight = 9,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 20,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 7,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 1,
                    Skip = true,
                    Door = Door.None
                },
                new()
                {
                    Weight = 35,
                    Skip = true,
                    Door = Door.None
                },
            };
            var expected = default(Encounter);

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.GetWeightedElement(elements, out var result);
            Assert.IsFalse(actual);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWeightedElement4()
        {
            const uint seed = 42346u;
            var elements = new List<IWeight>();
            var expected = default(IWeight);

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.GetWeightedElement(elements, out var result);
            Assert.IsFalse(actual);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWeightedElement5()
        {
            const uint seed = 42346u;
            List<IWeight>? elements = null;
            var expected = default(IWeight);

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.GetWeightedElement(elements, out var result);
            Assert.IsFalse(actual);
            Assert.AreEqual(expected, result);
        }
    }
}