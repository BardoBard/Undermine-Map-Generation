using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class Maps
    {
        public class LevelData
        {
            public List<List<string>> Rooms { get; set; } = new();
            public List<string> RoomsMulti { get; set; } = new();
        }

        [JsonProperty("levels")] public List<LevelData> Levels { get; set; } = new();
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("name")] public string? Name { get; set; }
    }
}