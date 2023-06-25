using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.JsonClasses
{
    public class Encounters : ICloneable
    {
        public object Clone() => (Encounters)this.MemberwiseClone(); //TODO: change this?

        public Default Default { get; set; } = new();
        public List<Encounter?> Rooms { get; set; }

        public bool HasWeight()
        {
            return !Rooms.Exists(x => x.Weight == 0);
        }
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
        [JsonProperty("weighteddoor")] public List<WeightDoor> WeightDoors { get; set; }
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
        [JsonIgnore] public int Door { get; set; } = 1;

        public bool AllowNeighbor(Encounter? neighbor)
        {
            //TODO: null?
            if (((NoExit & (int)Direction.North) == 0 && (neighbor.NoExit & (int)Direction.South) == 0) ||
                ((NoExit & (int)Direction.South) == 0 && (neighbor.NoExit & (int)Direction.North) == 0) ||
                ((NoExit & (int)Direction.East) == 0 && (neighbor.NoExit & (int)Direction.West) == 0)) return true;
            if ((NoExit & (int)Direction.West) == 0)
            {
                return (neighbor.NoExit & (int)Direction.East) == 0;
            }

            return false;
        }
    }
}