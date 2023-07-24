using System.Collections.Generic;

namespace Map_Generator.Parsing.Json.Classes
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
}