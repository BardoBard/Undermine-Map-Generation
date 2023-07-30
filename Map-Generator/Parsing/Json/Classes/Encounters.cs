using System;
using System.Collections.Generic;
using System.Linq;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using Map_Generator.Undermine;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class Encounters : ICloneable
    {
        public object Clone() => (Encounters)this.MemberwiseClone(); //TODO: change this?

        public Default Default { get; set; } = new();
        public List<Encounter?> Rooms { get; set; }

        public bool HasWeight() //TODO: change this to a variable
        {
            return !Rooms.Exists(encounter => encounter.Weight == 0);
        }
    }

    public class Encounter : IWeight
    {
        public class WeightedDoor : IWeight
        {
            [JsonProperty("weight")] public int Weight { get; set; }
            public bool Skip { get; set; }
            [JsonProperty("door")] public Door Door { get; set; }
        }

        [JsonProperty("weight")] public int Weight { get; set; }
        public bool Skip { get; set; }
        [JsonProperty("tag")] public string? Tag;
        [JsonProperty("Name")] public string? Name;
        [JsonProperty("weighteddoor")] public List<WeightedDoor>? WeightedDoors { get; set; }
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("enemies")] public List<Enemy>? Enemies { get; set; }
        [JsonProperty("prohibitedenemies")] public List<string>? ProhibitedEnemies { get; set; }
        [JsonProperty("difficulty")] public float[]? Difficulty { get; set; }

        // [JsonProperty("direction, noexit")] public int NoExit { get; set; }
        [JsonProperty("noexit")] public int NoExit { get; set; } = 0;
        [JsonIgnore] public int SubFloor { get; set; }
        [JsonProperty("recursion")] public int SequenceRecursionCount { get; set; } = -1;
        [JsonProperty("sequence")] public List<string> Sequence { get; set; } = new();
        [JsonIgnore] public bool Seen { get; set; } = false;
        [JsonIgnore] public Door Door { get; set; } = (Door)1;

        public bool AllowNeighbor(Encounter neighbor)
        {
            if (((NoExit & (int)Direction.North) == 0 && (neighbor.NoExit & (int)Direction.South) == 0) ||
                ((NoExit & (int)Direction.South) == 0 && (neighbor.NoExit & (int)Direction.North) == 0) ||
                ((NoExit & (int)Direction.East) == 0 && (neighbor.NoExit & (int)Direction.West) == 0)) return true;

            if ((NoExit & (int)Direction.West) == 0)
                return (neighbor.NoExit & (int)Direction.East) == 0;


            return false;
        }

        /// <summary>
        /// determines the enemies for this encounter
        /// </summary>
        /// <param name="data">zonedata</param>
        public void DetermineEnemies(ZoneData data)
        {
            Console.WriteLine("enemySpawnChance: {0}", this.Difficulty[0]);
            if (!Rand.Chance(this.Difficulty[0])) //TODO: check if we have to check encounter
            {
                Console.WriteLine("skipping room {0}", this.Name);
                return;
            }

            int floorNumber = Save.FloorIndex;

            data.Initialize(); //TODO: check, maybe earlier or not needed
            Override? floorOverride = data.Floors[floorNumber].Override;
            float floorDifficulty =
                floorOverride?.Difficulty ??
                (data.BaseDifficulty + data.DifficultyStep * floorNumber); //TODO: difficulty int?

            int[] enemyTypeWeight = floorOverride?.EnemyTypeWeight ?? data.EnemyTypeWeight;
            List<Enemy> enemies = new List<Enemy>(this.Enemies ?? data.Floors[floorNumber].Enemies);
            this.Enemies =
                new List<Enemy>(); //TODO: probably not override the current enemies instead return a new list

            if (this.ProhibitedEnemies != null)
                enemies.RemoveAll(enemy => this.ProhibitedEnemies.Contains(enemy.Name));

            if (enemies.Count <= 0)
            {
                Console.WriteLine("no enemies");
                return;
            }

            enemies.Shuffle();

            int enemyCombo = new[] { 3, 5, 6 }.FirstOrDefault(type => (enemies[0].Type & type) != 0);

            enemies.RemoveAll(enemy => (enemy.Type & enemyCombo) == 0);

            int num = System.Math.Min(enemies.Count, GetEnemyTypeCount(enemyTypeWeight));
            if (num <= 0)
                return;

            enemies.Shuffle();

            List<Enemy> enemies2 = new List<Enemy>();

            //get enemies and remove enemies that don't belong
            foreach (var enemy in enemies.Where(enemy => num != 1 || enemy.CanBeSolo))
            {
                if ((enemy.Type & enemyCombo) != 0 || enemyCombo == 0)
                {
                    enemies2.Add(enemy);
                    enemyCombo ^= enemy.Type;
                }

                if (enemies2.Count == num)
                    break;
            }

            if (enemies2.Count > 0)
            {
                float totalDifficulty = floorDifficulty + this.Difficulty[1];
                Console.WriteLine("total difficulty: {0}", totalDifficulty);
                int[] array = new int[enemies2.Count];
                for (int i = 0; i < enemies2.Count; i++)
                {
                    Enemy? enemy = enemies2[i];
                    float difficulty = enemy.GetDifficulty();
                    this.Enemies.Add(enemies2[i]);
                    totalDifficulty -= difficulty;
                    array[i]++;
                }
                Console.WriteLine("total difficulty after: {0}", totalDifficulty);
                while (totalDifficulty > 0f && enemies2.Count > 0)
                {
                    int randomNum = Rand.Range(0, enemies2.Count);

                    var enemy = enemies2[randomNum];
                    float enemyDifficulty = enemy.GetDifficulty();
                    if (enemyDifficulty > totalDifficulty || (enemy.Max > 0 && enemy.Max == array[randomNum]))
                    {
                        enemies2.RemoveAt(randomNum);
                        continue;
                    }

                    this.Enemies.Add(enemies2[randomNum]);
                    totalDifficulty -= enemyDifficulty;
                    array[randomNum]++;
                }
            }
            
            enemies2.Clear();
        }

        /// <summary>
        /// returns a random index based on the sum of weights and the individual weights,
        /// the higher the weight the higher the chance of getting that index
        /// </summary>
        /// <param name="enemyTypeWeight">array of weights</param>
        /// <returns>random index</returns>
        public static int GetEnemyTypeCount(int[] enemyTypeWeight) //TODO: this should probably go into a helper class
        {
            int sum = enemyTypeWeight.Sum();

            int num2 = Rand.RangeInclusive(1, sum);

            for (int i = 0; i < enemyTypeWeight.Length; i++)
            {
                num2 -= enemyTypeWeight[i];
                if (num2 <= 0)
                    return i;
            }

            return 0;
        }
    }

    public class Default
    {
        public float[] Difficulty { get; set; }
        public List<string> Sequence { get; set; } = new();
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("weighteddoor")] public List<Encounter.WeightedDoor> WeightedDoors { get; set; }
        [JsonProperty("door")] public Door Door { get; set; }

        public Default()
        {
            Difficulty = new float[] { 0, 0 };
        }
    }
}