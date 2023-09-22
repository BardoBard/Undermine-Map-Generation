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
        public void Initialize()
        {
            const uint seed = 123456u;
            var expected = new List<uint>() { 123456u, 848462657u, 1590858150u, 479153279u };

            Map_Generator.Undermine.Rand.Initialize(seed);
            var result = Map_Generator.Undermine.Rand.Seeds[Map_Generator.Undermine.Rand.StateType.Default];

            //result
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Initialize2()
        {
            const uint seed = 999999999u;
            var expected = new List<uint>() { 99999999u, 2858630044u, 3310420109u, 2988158114u };

            Map_Generator.Undermine.Rand.Initialize(seed);
            var result = Map_Generator.Undermine.Rand.Seeds[Map_Generator.Undermine.Rand.StateType.Default];

            //result
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Initialize3()
        {
            const uint seed = 0u;
            var expected = new List<uint>() { 0, 1u, 1812433254u, 1900727103u };

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
        public void NextUint()
        {
            const uint seed = 123456u;
            const uint expected = 328449612u;
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.NextUInt();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NextUint2()
        {
            const uint seed = 87654321u;
            const uint expected = 2250309519u;
            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.NextUInt();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeFloat()
        {
            const uint seed = 567123u;
            const float expected = 0.803521395f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.RangeFloat(0, 1); //0,1 is default
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void RangeFloat2()
        {
            const uint seed = 567123u;
            const float expected = 24.6774712f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.RangeFloat(7, 29);
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
        public void Value()
        {
            const uint seed = 567123u;
            const float expected = 0.19647860527038574f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Value();
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void Value2()
        {
            const uint seed = 12345678u;
            const float expected = 0.85980749130249023f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Value();
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void Value3()
        {
            const uint seed = 87654321u;
            const float expected = 0.25779891014099121f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Value();
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void Value4()
        {
            const uint seed = 12398746;
            const float expected = 0.0239715576171875f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Value();
            Assert.AreEqual(expected, actual, float.Epsilon);
        }

        [Test]
        public void RangeInclusiveUint()
        {
            const uint seed = 567123u;
            const uint min = 5;
            const uint max = 7;

            const int expected = 5;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInclusiveUint2()
        {
            const uint seed = 5954643u;
            const uint min = 2;
            const uint max = 8;

            const int expected = 2;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInclusiveUint3()
        {
            const uint seed = 7123u;
            const uint min = 1;
            const uint max = 9;

            const int expected = 6;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInclusiveInt()
        {
            const uint seed = 567123u;
            const int min = 5;
            const int max = 7;

            const int expected = 5;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInclusiveInt2()
        {
            const uint seed = 5954643u;
            const int min = 2;
            const int max = 8;

            const int expected = 2;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInclusiveInt3()
        {
            const uint seed = 7123u;
            const int min = 1;
            const int max = 9;

            const int expected = 6;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.RangeInclusive(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangUint()
        {
            const uint seed = 1234u;
            const uint min = 0;
            const uint max = 103;

            const int expected = 88;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangUint2()
        {
            const uint seed = 588672u;
            const uint min = 12;
            const uint max = 12;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        public void RangUint3()
        {
            const uint seed = 86783426u;
            const uint min = 1;
            const uint max = 8;

            const int expected = 1;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangUint4()
        {
            const uint seed = 4231u;
            const uint min = 0;
            const uint max = 1;

            const int expected = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeUint5()
        {
            const uint seed = 4231u;
            const uint min = 1;
            const uint max = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        public void RangeInt()
        {
            const uint seed = 1234u;
            const int min = 0;
            const int max = 103;

            const int expected = 88;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInt2()
        {
            const uint seed = 588672u;
            const int min = 12;
            const int max = 12;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        public void RangeInt3()
        {
            const uint seed = 86783426u;
            const int min = 1;
            const int max = 8;

            const int expected = 1;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInt4()
        {
            const uint seed = 4231u;
            const int min = 0;
            const int max = 1;

            const int expected = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            int actual = Map_Generator.Undermine.Rand.Range(min, max);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RangeInt5()
        {
            const uint seed = 4231u;
            const int min = 1;
            const int max = 0;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Range(min, max));
        }

        [Test]
        public void Chance()
        {
            const uint seed = 1234u;
            const float chance = 0.5f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Chance(chance);
            Assert.IsFalse(actual);
        }

        [Test]
        public void Chance2()
        {
            const uint seed = 7654221u;
            const float chance = 0.2f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Chance(chance);
            Assert.IsFalse(actual);
        }

        [Test]
        public void Chance3()
        {
            const uint seed = 7922251u;
            const float chance = 0.6f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Chance(chance);
            Assert.IsFalse(actual);
        }

        [Test]
        public void Chance4()
        {
            const uint seed = 221u;
            const float chance = 1.0f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            var actual = Map_Generator.Undermine.Rand.Chance(chance);
            Assert.IsTrue(actual);
        }

        [Test]
        public void Chance5()
        {
            const uint seed = 221u;
            const float chance = 2.0f;

            Map_Generator.Undermine.Rand.Initialize(seed);
            Assert.Throws<ArgumentOutOfRangeException>(() => Map_Generator.Undermine.Rand.Chance(chance));
        }

        [Test]
        public void Chance6()
        {
            const uint seed = 221u;
            const float chance = -1.0f;

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