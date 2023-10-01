using System;
using System.Collections.Generic;
using System.Linq;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class ZoneData
    {
        public ZoneData() //TODO: check if this is even needed
        {
            EnemyTypeWeight = new[] { 0, 1, 1, 1 };
        }

        public void Initialize() //TODO: check if this is even needed
        {
            while (EnemyTypeWeight.Length < 4)
            {
                EnemyTypeWeight = EnemyTypeWeight.Append(0).ToArray();
            }
        }

        public static ZoneData GetZoneData()
        {
            return JsonDecoder.ZoneData[Save.ZoneIndex].First(zoneData => Save.Check(zoneData.Requirements)) ??
                   throw new InvalidOperationException("data is null");
        }

        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("map")] public string Map { get; set; }
        [JsonProperty("connectivity")] public float Connectivity { get; set; }
        [JsonProperty("zonenumber")] public int ZoneNumber { get; set; }
        [JsonProperty("startfloor")] public int StartFloor { get; set; }
        [JsonProperty("basedifficulty")] public int BaseDifficulty { get; set; }
        [JsonProperty("difficultystep")] public int DifficultyStep { get; set; }
        [JsonProperty("enemytypeweight")] public int[] EnemyTypeWeight { get; set; }
        [JsonProperty("requirements")] public string Requirements { get; set; }

        //rest
        [JsonIgnore] public static CrawlSpace Crawlspace => new()
        {
            Min = 1,
            Max = 1,
            Percent100 = false,
            Items = new List<Item>
            {
                new()
                {
                    Name = "Crawl Space",
                    Requirement = null,
                    Weight = 1,
                    AdjustedWeight = 0
                }
            }
        };

        [JsonProperty("extras")] public List<Extra>? Extras { get; set; }
        [JsonProperty("resources")] public List<Resource>? Resources { get; set; }
        [JsonProperty("setpieces")] public List<SetPiece>? SetPieces { get; set; }


        [JsonProperty("floors")] public List<Floor> Floors { get; set; }
        [JsonProperty("roomnames")] public List<Dictionary<string, Room>> Rooms { get; set; }
        public interface IAbsolutes
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        public abstract class DefaultInformation : IAbsolutes
        {
            [JsonProperty("override")] public List<OverrideDefaultInformation> Override { get; set; } = new();
            [JsonProperty("min")] public int Min { get; set; }
            [JsonProperty("max")] public int Max { get; set; }

            [JsonProperty("100percent")] public bool Percent100 { get; set; }
            [JsonProperty("items")] public List<Item> Items { get; set; } = null!;

            public class OverrideDefaultInformation : IAbsolutes
            {
                [JsonProperty("min")] public int Min { get; set; }
                [JsonProperty("max")] public int Max { get; set; }
                [JsonProperty("enabled")] public bool Enabled { get; set; } = true;
            }
        }

        public class Extra : DefaultInformation
        {
        }

        public class Resource : DefaultInformation
        {
        }

        public class SetPiece : DefaultInformation
        {
        }

        public class CrawlSpace : DefaultInformation
        {
        }
    }


    public class Item : IWeight
    {
        [JsonProperty("name")] public string Name { get; set; } = null!;
        [JsonProperty("weight")] public int Weight { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("icon")] public ItemIcon ItemIcon { get; set; }

        [JsonIgnore] public bool Skip { get; set; }
        [JsonIgnore] public int AdjustedWeight { get; set; }
    }

    public class Override
    {
        [JsonProperty("difficulty")] public float? Difficulty { get; set; }
        [JsonProperty("enemytypeweight")] public int[]? EnemyTypeWeight { get; set; }

        [JsonProperty("connectivity")] public int? Connectivity { get; set; }
    }

    public class Floor
    {
        [JsonProperty("override")] public Override Override { get; set; } = new();
        [JsonProperty("enemies")] private List<string> enemies { get; set; } = new();
        public List<Enemy> Enemies => enemies.Select(Enemy.GetEnemy).ToList();
    }
}