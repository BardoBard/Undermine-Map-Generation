using System.Collections.Generic;
using System.IO;
using Map_Generator.Parsing.Json.Classes;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json
{
    public static class JsonDecoder
    {
        private static readonly string MapsData = File.ReadAllText(PathHandler.JsonPath + "maps.json");

        public static List<Maps> Maps =
            JsonConvert.DeserializeObject<List<Maps>>(MapsData) ?? new List<Maps>();

        private static readonly string RoomData = File.ReadAllText(PathHandler.JsonPath + "rooms.json");

        public static  Dictionary<string, RoomType> Rooms =
            JsonConvert.DeserializeObject<Dictionary<string, RoomType>>(RoomData) ?? new Dictionary<string, RoomType>();

        private static readonly string EncounterData = File.ReadAllText(PathHandler.JsonPath + "encounters.json");

        public static  Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>> Encounters =
            JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>>(
                EncounterData) ?? new Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>();


        private static readonly string ZoneDataData = File.ReadAllText(PathHandler.JsonPath + "zonedata.json");

        public static  List<List<ZoneData>> ZoneData =
            JsonConvert.DeserializeObject<List<List<ZoneData>>>(ZoneDataData) ?? new List<List<ZoneData>>();

        private static readonly string EnemiesDataData = File.ReadAllText(PathHandler.JsonPath + "enemies.json");

        public static  Dictionary<string, Enemy> Enemies =
            JsonConvert.DeserializeObject<Dictionary<string, Enemy>>(EnemiesDataData) ?? new Dictionary<string, Enemy>();

        public static void ReadJson()
        {
            Maps.Clear();
            // Rooms.Clear();
            Encounters.Clear();
            Enemies.Clear();
            ZoneData.Clear();
        
        
            Maps =  JsonConvert.DeserializeObject<List<Maps>>(MapsData) ?? new List<Maps>();
            // Rooms = JsonConvert.DeserializeObject<Dictionary<string, RoomType>>(RoomData) ?? new Dictionary<string, RoomType>();
            Encounters = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>>(
                EncounterData) ?? new Dictionary<string, Dictionary<string, Dictionary<string, Encounters>>>();
            Enemies = JsonConvert.DeserializeObject<Dictionary<string, Enemy>>(EnemiesDataData) ?? new Dictionary<string, Enemy>();
            ZoneData = JsonConvert.DeserializeObject<List<List<ZoneData>>>(ZoneDataData) ?? new List<List<ZoneData>>();
        }
    }
}