using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Map_Generator
{
    public class RoomType
    {
        [JsonProperty("stage")] public List<string> Stages { get; set; }
        [JsonProperty("roomtype")] public string RoomTypeTag { get; set; }
        [JsonProperty("chance")] public float Chance { get; set; } = 1;
        [JsonProperty("tags")] public string? Tags { get; set; }
        [JsonProperty("branchweight")] public int BranchWeight { get; set; } = 0;
        [JsonProperty("doorcost")] public string? DoorCost { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("direction")] public int? Direction { get; set; }
    }

    // [JsonArray]
    public class Batch
    {
        public Dictionary<string, List<string>> level_types { get; set; }
    }

    public class Maps
    {
        public class LevelData
        {
            public List<List<string>> Rooms { get; set; }
            public List<string> RoomsMulti { get; set; }
        }

        public List<LevelData> Levels { get; set; }
    }

    public class Enemy
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("difficulty")] public int Difficulty { get; set; }
        [JsonProperty("rougedifficulty")] public int RougeDifficulty { get; set; }
        [JsonProperty("canbesolo")] public bool CanBeSolo { get; set; }
        [JsonProperty("type")] public int Type { get; set; }
    }

    public class Room : IWeigh
    {
        public class WeightDoor : IWeigh
        {
            [JsonProperty("weight")] public int Weight { get; set; }
            [JsonProperty("door")] public int Door { get; set; }
        }

        [JsonProperty("weight")] public int Weight { get; set; }
        [JsonProperty("tag")] public string? Tag;
        [JsonProperty("Name")] public string? Name;
        [JsonProperty("weighteddoor")] public List<WeightDoor>? WeightDoors { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("enemies")] public List<Enemy>? Enemies { get; set; }
        [JsonProperty("prohibitedenemies")] public List<string>? ProhibitedEnemies { get; set; }
        [JsonProperty("difficulty")] public float[]? Difficulty { get; set; }
    }

    public class Default
    {
        public float[] Difficulty { get; set; }

        public Default()
        {
            Difficulty = new float[] { 0, 0 };
        }
    }

    public class Encounters
    {
        public Encounters()
        {
            Default = new Default();
        }

        public Default Default { get; set; }
        public List<Room> Rooms { get; set; }

        public bool HasWeight()
        {
            return !Rooms.Exists(x => x.Weight == 0);
        }
    }

    public class Floor
    {
        public List<Enemy> Enemies { get; set; }
    }

    public class ZoneData
    {
        public ZoneData()
        {
            EnemyTypeWeight = new[] { 0, 0, 0, 0 };
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
    }
}