using System.Collections.Generic;
using Map_Generator.Parsing;

namespace Map_Generator;

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

    private static MapName _mapName = MapName.mine;
    public static MapName GetMap() => _mapName;
    public static string GetMapName() => _mapName.ToString();
    
    public static MapName GetNextMap() => (MapName)Save.ZoneIndex + 1;
    public static string GetNextMapName() => ((MapName)Save.ZoneIndex + 1).ToString();
    public static void NextMap() => _mapName = (MapName)Save.ZoneIndex + 1;
}