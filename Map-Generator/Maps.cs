using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using Map_Generator.Parsing.Json.JsonClasses;
using Newtonsoft.Json;

namespace Map_Generator
{
    public class Maps
    {
        public class LevelData
        {
            public List<List<string>> Rooms { get; set; }
            public List<string> RoomsMulti { get; set; }
        }

        public List<LevelData> Levels { get; set; }
    }

    public enum Direction
    {
        None = 0,
        North = 1,
        South = 2,
        East = 4,
        West = 8,
        Up = 16,
        Down = 32,
        NE = 3,
        NW = 9,
        SE = 6,
        SW = 12,
        NS = 5,
        EW = 10,
        WNE = 13,
        NES = 7,
        ESW = 14,
        SWN = 11,
        Cardinal = 15,
        Vertical = 48,
        All = 63
    }

    public class Default
    {
        public float[] Difficulty { get; set; }
        public List<string> Sequence { get; set; } = new();

        public Default()
        {
            Difficulty = new float[] { 0, 0 };
        }
    }

    public class Override
    {
        [JsonProperty("difficulty")] public float? Difficulty { get; set; }
        [JsonProperty("enemytypeweight")] public int[]? EnemyTypeWeight { get; set; }
    }

    public class Floor
    {
        [JsonProperty("override")] public Override? Override { get; set; }
        [JsonProperty("enemies")] public List<Enemy> Enemies { get; set; }
    }

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

    public interface IWeigh
    {
        [JsonProperty("weight")] public int Weight { get; set; }
        [JsonIgnore] public bool Skip { get; set; }
    }
}