using System;
using System.Collections.Generic;
using System.Linq;
using Map_Generator.Json;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class ZoneData
    {
        public ZoneData() //TODO: check if this is even needed
        {
            EnemyTypeWeight = new[] { 0, 1, 1, 1 };
        }

        public void Initialize() //TODO: check if this is even needed
        {
            while (EnemyTypeWeight.Length < 4)
            {
                EnemyTypeWeight = EnemyTypeWeight.Append(0).ToArray();
            }
        }

        public static ZoneData GetZoneData()
        {
            return JsonDecoder.ZoneData[Save.ZoneIndex].First(zoneData => Save.Check(zoneData.Requirements)) ??
                   throw new InvalidOperationException("data is null");
        }

        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("map")] public string Map { get; set; }
        [JsonProperty("connectivity")] public float Connectivity { get; set; }
        [JsonProperty("zonenumber")] public int ZoneNumber { get; set; }
        [JsonProperty("startfloor")] public int StartFloor { get; set; }
        [JsonProperty("basedifficulty")] public int BaseDifficulty { get; set; }
        [JsonProperty("difficultystep")] public int DifficultyStep { get; set; }
        [JsonProperty("enemytypeweight")] public int[] EnemyTypeWeight { get; set; }
        [JsonProperty("requirements")] public string Requirements { get; set; }
        [JsonProperty("floors")] public List<Floor> Floors { get; set; }
    }

    public class Override
    {
        [JsonProperty("difficulty")] public float? Difficulty { get; set; }
        [JsonProperty("enemytypeweight")] public int[]? EnemyTypeWeight { get; set; }
    }

    public class Floor
    {
        [JsonProperty("override")] public Override? Override { get; set; }
        [JsonProperty("enemies")] private List<string> enemies { get; set; }
         public List<Enemy> Enemies => enemies.Select(Enemy.GetEnemy).ToList();
    }
}