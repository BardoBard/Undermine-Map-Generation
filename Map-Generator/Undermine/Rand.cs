using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Parsing.Json.Interfaces;

namespace Map_Generator.Undermine
{
    public static class Rand
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

        public const int MinSeed = 0;

        public const int MaxSeed = 99999999;
        private static readonly Dictionary<StateType, List<uint>> _seeds = new();
        private static readonly Stack<StateType> _stateStack = new();


        public static uint NextUInt()
        {
            StateType currentScope = GetCurrentScope();

            uint x = _seeds[currentScope][0];
            x ^= x << 11;
            x ^= x >> 8;

            uint y = _seeds[currentScope][3];
            y ^= y >> 19;
            y = x ^ y;

            _seeds[currentScope].RemoveAt(0);
            _seeds[currentScope].Add(y);

            return y;
        }

        public static uint NextUIntWatch()
        {
            StateType currentScope = GetCurrentScope();

            var Seeds = new Dictionary<StateType, List<uint>>(_seeds);
            uint x = Seeds[currentScope][0];
            x ^= x << 11;
            x ^= x >> 8;

            uint y = Seeds[currentScope][3];
            y ^= y >> 19;
            y = x ^ y;

            Seeds[currentScope].RemoveAt(0);
            Seeds[currentScope].Add(y);

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

        public static bool Chance(float chance) =>
            chance == 1.0f || //chance can be 1.0f due to the nature of the data that I'm working with
            (chance != 0.0f && chance > Value());


        public static void Initialize(uint initialSeed)
        {
            _seeds.Clear();
            _stateStack.Clear();

            foreach (StateType scope in Enum.GetValues(typeof(StateType)))
            {
                _seeds[scope] = new List<uint>
                {
                    initialSeed,
                    initialSeed * 1812433253 + 1
                };
                _seeds[scope].Add(_seeds[scope][1] * 1812433253 + 1);
                _seeds[scope].Add(_seeds[scope][2] * 1812433253 + 1);
            }
        }

        public static bool GetWeightedElement<T>(ICollection<T>? elements, out T result, bool skip = true)
            where T : class, IWeight
        {
            //TODO: refactor this entire method
            if (elements == null || !elements.Any())
            {
                result = default;
                return false;
            }

            var elems = elements.Where(element => element is { Skip: false });
            var weights = elems as T[] ?? elems.ToArray();
            int totalWeight = weights.Aggregate(0, (current, element) => current + element.Weight);
            BardLog.Log("totalweight {0}", totalWeight);

            if (totalWeight == 0)
            {
                NextUInt();
                result = default;
                return false;
            }

            int randomNum = RangeInclusive(1, totalWeight);
            foreach (var element2 in weights)
            {
                randomNum -= element2.Weight;
                if (randomNum <= 0)
                {
                    result = element2;
                    if (skip) element2.Skip = true;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public static int ClampSeed(int seed)
        {
            while (seed > MaxSeed)
            {
                seed /= 10;
            }

            return Unity.Mathf.Clamp(seed, MinSeed, MaxSeed);
        }

        private static void EnterScope(StateType scope) => _stateStack.Push(scope);

        private static void ExitScope()
        {
            if (_stateStack.Count > 0)
                _stateStack.Pop();
        }

        private static StateType GetCurrentScope() => _stateStack.Count > 0 ? _stateStack.Peek() : StateType.Default;


        public class Scope : IDisposable
        {
            public Scope(StateType scope) => EnterScope(scope);

            public void Dispose() => ExitScope();
        }
    }
}