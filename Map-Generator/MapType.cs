
using Map_Generator.Parsing;

namespace Map_Generator
{
    public static class MapType
    {
        public enum MapName
        {
            none = 0,
            mine = 1,
            dungeon = 2,
            hall = 3,
            cavern = 4,
            core = 5,
        }

        private static MapName _mapName => (MapName)System.Math.Min(Save.ZoneIndex + 1, (int)MapName.core);
        public static MapName GetMap() => _mapName;
        public static string GetMapName() => _mapName.ToString();
    
        public static MapName GetNextMap() => (MapName)System.Math.Min(Save.ZoneIndex + 1, (int)MapName.core);
        public static string GetNextMapName() => ((MapName)System.Math.Min(Save.ZoneIndex + 1, (int)MapName.core)).ToString();
    }
}