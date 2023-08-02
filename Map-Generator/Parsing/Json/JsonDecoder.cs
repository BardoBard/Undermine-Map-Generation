using System;
using System.Collections.Generic;
using System.IO;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Newtonsoft.Json;

namespace Map_Generator.Json;

public static class JsonDecoder
{
    private static readonly string MapsData = File.ReadAllText(PathHandler.UnderminePath + "maps.json");

    public static readonly List<Maps> Maps =
        JsonConvert.DeserializeObject<List<Maps>>(MapsData) ?? new List<Maps>();

    private static readonly string RoomData = File.ReadAllText(PathHandler.UnderminePath + "rooms.json");

    public static readonly Dictionary<string, RoomType> Rooms =
        JsonConvert.DeserializeObject<Dictionary<string, RoomType>>(RoomData) ?? new Dictionary<string, RoomType>();

    private static readonly string EncounterData = File.ReadAllText(PathHandler.UnderminePath + "encounters.json");

    public static readonly Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>> Encounters =
        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>>(
            EncounterData) ?? new Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>();


    private static readonly string ZoneDataData = File.ReadAllText(PathHandler.UnderminePath + "zonedata.json");

    public static readonly List<List<ZoneData>> ZoneData =
        JsonConvert.DeserializeObject<List<List<ZoneData>>>(ZoneDataData) ?? new List<List<ZoneData>>();

    private static readonly string EnemiesDataData = File.ReadAllText(PathHandler.UnderminePath + "enemies.json");

    public static readonly Dictionary<string, Enemy> Enemies =
        JsonConvert.DeserializeObject<Dictionary<string, Enemy>>(EnemiesDataData) ?? new Dictionary<string, Enemy>();
}