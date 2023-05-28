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
        // [JsonProperty("children")] public bool Children { get; set; } = false;
        // [JsonProperty("extra")] public bool Extra { get; set; } = false;
        [JsonProperty("branchweight")] public int BranchWeight { get; set; } = 0;
        [JsonProperty("doorcost")] public string? DoorCost { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("direction")] public int? Direction { get; set; }
        [JsonIgnore] public Encounter Encounter { get; set; }
        [JsonIgnore] public RoomType PreviousRoom { get; set; }
        [JsonIgnore] public bool CanReload { get; set; }
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
        [JsonProperty("max")] public int Max { get; set; }
        [JsonProperty("type")] public int Type { get; set; }

        public int GetDifficulty(int gameMode)
        {
            return gameMode == 0 ? Difficulty : RougeDifficulty;
        }
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

    public class Encounter : IWeigh
    {
        public class WeightDoor : IWeigh
        {
            [JsonProperty("weight")] public int Weight { get; set; }
            public bool Skip { get; set; }
            [JsonProperty("door")] public int Door { get; set; }
        }

        [JsonProperty("weight")] public int Weight { get; set; }
        public bool Skip { get; set; }
        [JsonProperty("tag")] public string? Tag;
        [JsonProperty("Name")] public string? Name;
        [JsonProperty("weighteddoor")] public List<WeightDoor>? WeightDoors { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("enemies")] public List<Enemy>? Enemies { get; set; }
        [JsonProperty("prohibitedenemies")] public List<string>? ProhibitedEnemies { get; set; }
        [JsonProperty("difficulty")] public float[]? Difficulty { get; set; }

        [JsonProperty("direction, noexit")] public Direction NoExit { get; set; }

        // [JsonProperty("noexit")] public Direction NoExit { get; set; }
        [JsonIgnore] public bool Seen { get; set; }
        [JsonIgnore] public int Door { get; set; }

        public bool AllowNeighbor(Encounter neighbor)
        {
            if (((NoExit & Direction.North) == 0 && (neighbor.NoExit & Direction.South) == 0) ||
                ((NoExit & Direction.South) == 0 && (neighbor.NoExit & Direction.North) == 0) ||
                ((NoExit & Direction.East) == 0 && (neighbor.NoExit & Direction.West) == 0)) return true;

            if ((NoExit & Direction.West) == 0)
                return (neighbor.NoExit & Direction.East) == 0;


            return false;
        }

        public Encounter()
        {
            // Enemies = new List<Enemy>();
            Difficulty = null;
            Seen = false;
        }
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
        public List<Encounter> Rooms { get; set; }

        public bool HasWeight()
        {
            return !Rooms.Exists(x => x.Weight == 0);
        }
    }

    public class Override
    {
        public float Difficulty { get; set; }
    }

    public class Floor
    {
        public Override Override { get; set; }
        public List<Enemy> Enemies { get; set; }
    }

    public class ZoneData
    {
        public ZoneData()
        {
            EnemyTypeWeight = new[] { 0, 1, 1, 1 };
        }

        public void Initialize()
        {
            while (EnemyTypeWeight.Length < 4)
            {
                EnemyTypeWeight = EnemyTypeWeight.Append(1).ToArray();
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