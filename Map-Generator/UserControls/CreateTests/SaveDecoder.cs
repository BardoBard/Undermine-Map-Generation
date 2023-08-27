using System;
using System.Collections.Generic;
using System.IO;
using Map_Generator.Parsing;
using Newtonsoft.Json;

namespace Map_Generator.UserControls.CreateTests
{
    public static class SaveDecoder
    {
        public static SaveDatas SaveData = null!;

        public static void ReadSaveData(int saveNumber)
        {
            string saveDataString = Path.Combine(PathHandler.UndermineSavePath, @$"Save{saveNumber}.json");
            SaveData = JsonConvert.DeserializeObject<SaveDatas>(File.ReadAllText(saveDataString)) ?? throw new Exception("SaveData is null");
        }

        public static void WriteSaveData()
        {
            var outputPath =  Path.Combine(PathHandler.TestsPath, MapType.GetMapName()) + "/" + "test.json";
            var outputString = JsonConvert.SerializeObject(SaveData, Formatting.Indented);
            PathHandler.WriteAllToFile(outputPath, outputString);
        }

        public class SaveDatas
        {
            [JsonProperty("guid")] public  Guid? ZoneGuid;
            [JsonProperty("altarItemID")]  static Guid? AltarItemID;
            [JsonProperty("familiar")] public  Guid? Familiar;
            [JsonProperty("unlocked")] public  List<Guid> Unlocked = new();
            [JsonProperty("discovered")] public  List<Guid> Discovered = new();
            [JsonProperty("upgradeString")] public  string? UpgradeString;
            [JsonProperty("autoSaveData")] public  AutoGameSaveDatas? AutoSaveData;
            [JsonProperty("rogueSaveData")] public  RogueGameSaveDatas? RogueSaveData;
            
            public abstract class GameSaveDatas
            {
                [JsonProperty("seed")] public int Seed;
                [JsonProperty("zone")] public Guid? Zone;
                [JsonProperty("potionDatas")] public List<Datas> PotionDatas = new();
                [JsonProperty("statusEffects")] public List<Datas> StatusEffects = new();
            
                public class Datas
                {
                    [JsonProperty("id")] public Guid? id;
                }
            }
            
            public class RogueGameSaveDatas : GameSaveDatas
            {
            }
            
            public class AutoGameSaveDatas : GameSaveDatas
            {
            }
        }
    }
}