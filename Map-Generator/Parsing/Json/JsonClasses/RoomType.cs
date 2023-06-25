using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.JsonClasses
{
    public class RoomType
    {
        public RoomType Clone() => (RoomType)this.MemberwiseClone();

        [JsonProperty("stage")] public List<string> Stages { get; set; }
        [JsonProperty("roomtype")] public string RoomTypeTag { get; set; }
        [JsonProperty("chance")] public float Chance { get; set; } = 1;
        [JsonProperty("tags")] public string? Tags { get; set; }
        [JsonProperty("children")] public bool Children { get; set; } = false;
        [JsonProperty("extra")] public bool Extra { get; set; } = false;
        [JsonProperty("branchweight")] public int BranchWeight { get; set; } = 0;
        [JsonProperty("doorcost")] public int? DoorCost { get; set; }
        [JsonProperty("requirement")] public string? Requirement { get; set; }
        [JsonProperty("direction")] public int? Direction { get; set; }
        [JsonIgnore] public Encounter? Encounter { get; set; }
        [JsonIgnore] public RoomType? PreviousRoom { get; set; }
        [JsonIgnore] public bool CanReload { get; set; }
        [JsonIgnore] public bool Secluded { get; set; }

        public bool CheckEncounter(Encounter? encounter)
        {
            if (encounter == null)
            {
                Console.WriteLine("null...");
                return false;
            }

            // if (encounter.IsDeprecated)
            // {
            //     return false;
            // }
            if (this?.PreviousRoom?.Encounter != null)
            {
                if (!this.PreviousRoom.Encounter.AllowNeighbor(encounter))
                {
                    Console.WriteLine("not allowed encounter: {0}, noexit: {1}", encounter.Name, encounter.NoExit);
                    Console.WriteLine("not allowed neighbor: {0}, noexit: {1}", this.PreviousRoom.Encounter.Name,
                        this.PreviousRoom.Encounter.NoExit);
                    Console.WriteLine("");
                    return false;
                }
            }

            // if (room.Stage != encounter.Stage) //TODO: two rooms should be ignored
            // {
            // return false;
            // }
            if (encounter.Requirement != null && !Save.Check(encounter.Requirement))
            {
                Console.WriteLine("requirement failed");
                return false;
            }

            if (encounter.Seen) //&& !encounter.Repeatable <- TODO: repeatable only for a few rooms, which in a normal run can be ignored
            {
                Console.WriteLine("seen: {0}", encounter.Name);
                return false;
            }

            return true;
        }
    }
}