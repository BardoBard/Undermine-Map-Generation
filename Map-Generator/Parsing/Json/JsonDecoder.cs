using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Map_Generator.Json;

public static class JsonDecoder
{
    private static readonly string LocalLowPath =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

    public static readonly string UnderminePath = LocalLowPath + @"\Thorium Entertainment\UnderMine\";

    private static readonly string MapsData = File.ReadAllText(UnderminePath + "maps.json");

    public static readonly Dictionary<string, Maps> Maps =
        JsonConvert.DeserializeObject<Dictionary<string, Maps>>(MapsData) ?? new Dictionary<string, Maps>();

    private static readonly string RoomData = File.ReadAllText(UnderminePath + "rooms.json");

    public static readonly Dictionary<string, RoomType> Rooms =
        JsonConvert.DeserializeObject<Dictionary<string, RoomType>>(RoomData) ?? new Dictionary<string, RoomType>();

    private static readonly string EncounterData = File.ReadAllText(UnderminePath + "encounters.json");

    public static readonly Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>> Encounter =
        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>>(
            EncounterData) ?? new Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>();


    public static readonly string ZoneDataData = File.ReadAllText(UnderminePath + "zonedata.json");

    public static readonly List<List<ZoneData>> ZoneData = JsonConvert.DeserializeObject<List<List<ZoneData>>>(ZoneDataData) ?? new List<List<ZoneData>>();
}