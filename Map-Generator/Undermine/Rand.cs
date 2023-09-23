using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static readonly Dictionary<StateType, List<uint>> seeds = new();
        private static readonly Stack<StateType> stateStack = new();

        public static Dictionary<StateType, ReadOnlyCollection<uint>> Seeds =>
            seeds.ToDictionary(kv => kv.Key, kv => kv.Value.AsReadOnly());

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

        public static uint PeekNextUInt()
        {
            StateType currentScope = GetCurrentScope();

            var Seeds = new Dictionary<StateType, List<uint>>(seeds);
            uint x = Seeds[currentScope][0];
            x ^= x << 11;
            x ^= x >> 8;

            uint y = Seeds[currentScope][3];
            y ^= y >> 19;
            y = x ^ y;

            return y;
        }

        public static float RangeFloat(uint min = 0, uint max = 1)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "min must be less than or equal to max");

            float value = (float)(1 - NextUInt() << 9) / 0xFFFFFFFF;
            return (max - min) * value + min;
        }

        public static float Value() => 1 - RangeFloat();
        public static float PeekValue() => 1 - (float)(1 - PeekNextUInt() << 9) / 0xFFFFFFFF;


        public static int RangeInclusive(uint min, uint max) => Range(min, max + 1);

        public static int RangeInclusive(int min, int max) => Range(min, max + 1);

        public static int Range(uint min, uint max)
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException(nameof(min), "min must be less than to max");

            return (int)(NextUInt() % (max - min) + min);
        }

        public static int Range(int min, int max)
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException(nameof(min), "min must be less than to max");

            return (int)(NextUInt() % (max - min) + min);
        }

        public static bool Chance(float chance)
        {
            if (chance is < 0f or > 1f)
                throw new ArgumentOutOfRangeException(nameof(chance), "chance must be between 0f and 1f");
            
            return chance == 1.0f || //chance can be 1.0f due to the nature of the data that I'm working with
                   (chance != 0.0f && chance > Value());
        }


        public static void Initialize(uint initialSeed)
        {
            seeds.Clear();
            stateStack.Clear();

            initialSeed = (uint)ClampSeed((int)initialSeed);

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

        public static bool GetWeightedElement<T>(ICollection<T?>? elements, out T result, bool skip = true)
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

        private static void EnterScope(StateType scope) => stateStack.Push(scope);

        private static void ExitScope()
        {
            if (stateStack.Count > 0)
                stateStack.Pop();
        }

        public static StateType GetCurrentScope() => stateStack.Count > 0 ? stateStack.Peek() : StateType.Default;


        public class Scope : IDisposable
        {
            public Scope(StateType scope) => EnterScope(scope);

            public void Dispose() => ExitScope();
        }
    }
}