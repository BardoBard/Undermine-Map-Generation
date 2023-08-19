using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class Maps
    {
        public class LevelData
        {
            public List<List<string>> Rooms { get; set; }
            public List<string> RoomsMulti { get; set; }
        }

        [JsonProperty("levels")] public List<LevelData> Levels { get; set; }
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("name")] public string? Name { get; set; }
    }
}