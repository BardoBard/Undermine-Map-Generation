using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Parsing.Json.Interfaces;

namespace Map_Generator.Undermine
{
    static class Rand
    {
        public enum StateType
        {
            Default,
            Layout,

            // Relic,
            // Blessing,
            // Blueprint,
            // BasicItem,
            // Potion,
            // Health,
            // Resource,
            // ShopRelic,
            // ShopPotion,
            // ShopHealth,
            // ShopBasicItem,
            // Legendary,
            // Dibble,
            // Misc,
            // Shop,
            // MajorCurse,
            // MinorCurse,
            Familiar,
            Upgrade,
            Chaos,
            Table,
            Hex,
            ItemPackage,

            // Shoguul,
            // Transmute,
            Prayer
        }

        private static readonly Dictionary<StateType, List<uint>> seeds = new();
        private static readonly Stack<StateType> stateStack = new();


        public static uint NextUInt()
        {
            StateType currentScope = GetCurrentScope();

            uint x = seeds[currentScope][0];
            x ^= x << 11;
            x ^= x >> 8;

            uint y = seeds[currentScope][3];
            y ^= y >> 19;
            y = x ^ y;

            seeds[currentScope].RemoveAt(0);
            seeds[currentScope].Add(y);

            return y;
        }

        public static float RangeFloat(uint min = 0, uint max = 1)
        {
            float range = max - min;
            float value = (float)(1 - NextUInt() << 9) / 0xFFFFFFFF;
            return range * value + min;
        }

        public static float Value() => 1 - RangeFloat();

        public static int RangeInclusive(uint min, uint max) => Range(min, max + 1);

        public static int RangeInclusive(int min, int max) => Range(min, max + 1);

        public static int Range(uint min, uint max) => (int)(NextUInt() % (max - min) + min);

        public static int Range(int min, int max) => (int)(NextUInt() % (max - min) + min);

        public static bool Chance(float chance) => chance == 1.0f || (chance != 0.0f && chance > Value());


        public static void Initialize(uint initialSeed)
        {
            seeds.Clear();

            foreach (StateType scope in Enum.GetValues(typeof(StateType)))
            {
                seeds[scope] = new List<uint>
                {
                    initialSeed,
                    initialSeed * 1812433253 + 1
                };
                seeds[scope].Add(seeds[scope][1] * 1812433253 + 1);
                seeds[scope].Add(seeds[scope][2] * 1812433253 + 1);
            }
        }

        public static bool GetWeightedElement<T>(ICollection<T?>? elements, out T result) where T : class, IWeight
        {
            //TODO: refactor this entire method
            if (elements == null || !elements.Any())
            {
                // NextUInt(); //TODO: check if this can stay removed
                result = default;
                return false;
            }

            var elems = elements.Where(element => !element.Skip);
            var weights = elems as T[] ?? elems.ToArray();
            int num = weights.Aggregate(0, (current, element) => current + element.Weight);

            if (num == 0)
            {
                NextUInt();
                result = default;
                return false;
            }

            int num2 = RangeInclusive(1, num);
            foreach (var element2 in weights)
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

        private static void EnterScope(StateType scope) => stateStack.Push(scope);

        private static void ExitScope()
        {
            if (stateStack.Count > 0)
                stateStack.Pop();
        }

        private static StateType GetCurrentScope() => stateStack.Count > 0 ? stateStack.Peek() : StateType.Default;


        public class Scope : IDisposable
        {
            public Scope(StateType scope) => EnterScope(scope);

            public void Dispose() => ExitScope();
        }
    }
}