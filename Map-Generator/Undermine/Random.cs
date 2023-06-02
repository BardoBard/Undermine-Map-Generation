using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Map_Generator
{
    static class Random
    {
        private static List<uint> seeds = new();
        private static Stack<List<uint>> seedsStack = new Stack<List<uint>>();

        public static uint NextUInt()
        {
            uint x = seeds[0];
            x ^= x << 11;
            x ^= x >> 8;

            uint y = seeds[3];
            y ^= y >> 19;
            y = x ^ y;

            seeds.RemoveAt(0);
            seeds.Add(y);

            return y;
        }

        public static float RangeFloat(uint min = 0, uint max = 1)
        {
            float range = max - min;
            float value = (float)(1 - NextUInt() << 9) / 0xFFFFFFFF;
            return range * value + min;
        }

        public static float Value()
        {
            float range = RangeFloat();
            return 1 - range;
        }

        public static bool Chance(float chance)
        {
            if (chance != 1f)
            {
                if (chance != 0f)
                {
                    var v = Value();
                    // Console.WriteLine(v);
                    return chance > v;
                }

                return false;
            }

            return true;
            // return chance == 1.0f || (chance != 0.0f && chance > Value());
        }

        public static int RangeInclusive(uint min, uint max)
        {
            return Range((uint)min, max + 1);
        }

        public static int RangeInclusive(int min, int max)
        {
            return Range(min, max + 1);
        }

        public static int Range(uint min, uint max)
        {
            return (int)(NextUInt() % (max - min) + min);
        }

        public static int Range(int min, int max)
        {
            return (int)(NextUInt() % (max - min) + min);
        }

        public static void Initialize(uint seed)
        {
            seeds.Clear();
            seeds.Add(seed);
            seeds.Add(seed * 1812433253 + 1);
            seeds.Add(seeds[1] * 1812433253 + 1);
            seeds.Add(seeds[2] * 1812433253 + 1);
        }

        public static bool GetWeightedElement<T>(List<T> elements, out T result) where T : IWeigh
        {
            if (elements == null || elements.Count == 0)
            {
                NextUInt();
                result = default;
                return false;
            }

            var elems = elements.Where(element => !element.Skip);
            int num = elems.Aggregate(0, (current, element) => current + element.Weight);

            if (num == 0)
            {
                NextUInt();
                result = default;
                return false;
            }

            int num2 = RangeInclusive(1, num);
            foreach (var element2 in elems)
            {
                num2 -= element2.Weight;
                if (num2 <= 0)
                {
                    result = element2;
                    element2.Skip = true;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public static IDisposable CreateScope()
        {
            var clonedSeeds = new List<uint>(seeds);
            seedsStack.Push(seeds);
            seeds = clonedSeeds;

            return new ScopeDisposable();
        }

        private class ScopeDisposable : IDisposable
        {
            private bool disposed = false;

            public void Dispose()
            {
                if (!disposed)
                {
                    seeds = seedsStack.Pop();
                    disposed = true;
                }
            }
        }
    }
}