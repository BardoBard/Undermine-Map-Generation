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
        public List<Encounter?> Rooms { get; set; } = new();

        public bool HasWeight() //TODO: change this to a variable
        {
            return !Rooms.Exists(encounter => encounter.Weight == 0);
        }

        public void Initialize()
        {
            foreach (var encounter in Rooms)
            {
                if (encounter == null) continue;
                encounter.Door = (encounter.Door == Door.None ? Default.Door : encounter.Door);
            }
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
        [JsonProperty("difficultyweight")] public int DifficultyWeight { get; set; }
        [JsonProperty("branchweight")] public int? Branchweight { get; set; }
        public bool Skip { get; set; }
        [JsonProperty("tag")] public string? Tag;
        [JsonProperty("name")] public string? Name;
        [JsonProperty("weighteddoor")] public List<WeightedDoor?>? WeightedDoors { get; set; }
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("enemies")] private List<string>? enemies { get; set; } = null;

        private List<Enemy>? Enemies
        {
            get => enemies?.Select(Enemy.GetEnemy).ToList();
            set { enemies = value?.Select(enemy => enemy.Name).ToList(); }
        }

        public List<Enemy> RoomEnemies { get; set; } = new();
        [JsonProperty("prohibitedenemies")] public List<string>? ProhibitedEnemies { get; set; }
        [JsonProperty("difficulty")] public float[]? Difficulty { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public Direction Direction { get; set; } = Direction.Undetermined;

        [JsonProperty("noexit")] public Direction NoExit { get; set; } = 0;
        [JsonIgnore] public int SubFloor { get; set; }
        [JsonProperty("recursion")] public int SequenceRecursionCount { get; set; } = -1;
        [JsonProperty("sequence")] public List<string> Sequence { get; set; } = new();
        [JsonIgnore] public bool Seen { get; set; } = false;
        [JsonIgnore] public bool HasCrawlSpace { get; set; } = false;
        [JsonProperty("door")] public Door Door { get; set; } = Door.None;
        [JsonProperty("autospawn")] public int? AutoSpawn { get; set; }

        public bool AllowNeighbor(Encounter neighbor)
        {
            //TODO: just return true? because this just checks if one of the two encounters has Direction.All
            return ((NoExit & Direction.North) == 0 && (neighbor.NoExit & Direction.South) == 0) ||
                   ((NoExit & Direction.South) == 0 && (neighbor.NoExit & Direction.North) == 0) ||
                   ((NoExit & Direction.East) == 0 && (neighbor.NoExit & Direction.West) == 0) ||
                   (NoExit & Direction.West) == 0 && (neighbor.NoExit & Direction.East) == 0;
        }

        public bool AllowNeighbor(Direction direction)
        {
            return (direction & NoExit) == 0;
        }

        /// <summary>
        /// determines the enemies for this encounter
        /// </summary>
        /// <param name="data">zonedata</param>
        public void DetermineEnemies(ZoneData data)
        {
            BardLog.Log("enemySpawnChance: {0}", Difficulty[0]);
            if (!Rand.Chance(this.Difficulty[0])) //TODO: check if we have to check encounter
            {
                BardLog.Log("skipping room {0}", this.Name);
                return;
            }

            int floorNumber = Save.FloorIndex;

            data.Initialize(); //TODO: check, maybe earlier or not needed
            Override? floorOverride = data.Floors[floorNumber].Override;
            float floorDifficulty =
                floorOverride.Difficulty ??
                (data.BaseDifficulty + data.DifficultyStep * floorNumber); //TODO: difficulty int?

            int[] enemyTypeWeight = floorOverride?.EnemyTypeWeight ?? data.EnemyTypeWeight;
            List<Enemy> enemies1 = new List<Enemy>(this.Enemies ?? data.Floors[floorNumber].Enemies);
            this.RoomEnemies =
                new List<Enemy>(); //TODO: probably not override the current enemies instead return a new list

            if (this.ProhibitedEnemies != null)
                enemies1.RemoveAll(enemy => this.ProhibitedEnemies.Contains(enemy.Name));

            if (enemies1.Count <= 0)
            {
                BardLog.Log("no enemies");
                return;
            }

            enemies1.Shuffle();

            int enemyCombo = new[] { 3, 5, 6 }.FirstOrDefault(type => (enemies1[0].Type & type) != 0);

            enemies1.RemoveAll(enemy => (enemy.Type & enemyCombo) == 0);

            int num = System.Math.Min(enemies1.Count, GetEnemyTypeCount(enemyTypeWeight));
            if (num <= 0)
                return;

            enemies1.Shuffle();

            List<Enemy> enemies2 = new List<Enemy>();

            //get enemies and remove enemies that don't belong
            foreach (var enemy in enemies1.Where(enemy => num != 1 || enemy.CanBeSolo))
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
                BardLog.Log("total difficulty: {0}", totalDifficulty);
                foreach (var enemy in enemies2)
                    BardLog.Log("enemy: {0}", enemy.Name);


                int[] array = new int[enemies2.Count];
                for (int i = 0; i < enemies2.Count; i++)
                {
                    Enemy? enemy = enemies2[i];
                    int difficulty = enemy.GetDifficulty();
                    this.RoomEnemies.Add(enemies2[i]);
                    totalDifficulty -= difficulty;
                    array[i]++;
                }

                BardLog.Log("total difficulty after: {0}", totalDifficulty);
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

                    this.RoomEnemies.Add(enemies2[randomNum]);
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
        [JsonProperty("weighteddoor")] public List<Encounter.WeightedDoor?>? WeightedDoors { get; set; }
        [JsonProperty("door")] public Door Door { get; set; }

        [JsonProperty("autospawn")] public int AutoSpawn = 0;

        public Default()
        {
            Difficulty = new float[] { 0, 0 };
        }
    }
}