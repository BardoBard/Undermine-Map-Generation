using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Map_Generator.Json;
using Map_Generator.Undermine;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class RoomType
    {
        public RoomType Clone() => (RoomType)this.MemberwiseClone();

        [JsonProperty("stage")] public List<string> Stages { get; set; } = null!;
        [JsonProperty("roomtype")] public string RoomTypeTag { get; set; } = null!;
        private float chance = 1f;

        [JsonProperty("chance")]
        public float Chance
        {
            get { return chance + StepChance * Save.floor_number + ZoneStepChance * Save.ZoneIndex; }
            set { chance = value; }
        }

        [JsonProperty("stepchance")] public float StepChance { get; set; } = 0;
        [JsonProperty("zonestepchance")] public float ZoneStepChance { get; set; } = 0;
        [JsonProperty("tags")] public string? Tags { get; set; }
        [JsonProperty("children")] public bool Children { get; set; } = false;
        [JsonProperty("encounter")] public bool HasExtraEncounter { get; set; } = false;
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("branchweight")] public int BranchWeight { get; set; } = 0;
        [JsonProperty("doorcost")] public int? DoorCost { get; set; }
        [JsonProperty("requirements")] public string? Requirement { get; set; }
        [JsonProperty("direction")] public int? Direction { get; set; }
        [JsonIgnore] public Encounter? Encounter { get; set; }
        [JsonIgnore] public RoomType? PreviousRoom { get; set; }
        [JsonIgnore] public bool CanReload { get; set; }
        [JsonIgnore] public bool Secluded { get; set; }

        public void Initialize(string mapNameEncounter, string name2)
        {
            // if (this.Encounter?.WeightDoors != null)
            if (Rand.GetWeightedElement(this.Encounter.WeightDoors!, out var door))
                this.Encounter.Door = door.Door;


            this.Encounter.Difficulty ??=
                JsonDecoder.Encounter[mapNameEncounter][name2][this.RoomTypeTag].Default.Difficulty;

            // loop through data and check if requirement fits
            ZoneData data = JsonDecoder.ZoneData[Save.ZoneIndex]
                                .First(zoneData => Save.Check(zoneData.Requirements)) ??
                            throw new InvalidOperationException(
                                "data is null"); //TODO: change zonedata[0] to generic value
            Console.WriteLine("found zonedata: {0}, with requirement: {1}", data.Name, data.Requirements);
            this.Encounter.DetermineEnemies(data); //not scoped randomness
            this.Encounter.Seen = true;
        }

        public bool CheckEncounter(Encounter? encounter, RoomType previousRoom)
        {
            if (encounter == null)
            {
                Console.WriteLine("null during check...");
                return false;
            }

            // if (encounter.IsDeprecated)
            // {
            //     return false;
            // }
            if (previousRoom?.Encounter != null)
            {
                if (!previousRoom.Encounter.AllowNeighbor(encounter))
                {
                    Console.WriteLine("not allowed encounter: {0}, noexit: {1}", encounter.Name, encounter.NoExit);
                    Console.WriteLine("not allowed neighbor: {0}, noexit: {1}", previousRoom.Encounter.Name,
                        previousRoom.Encounter.NoExit);
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
                Console.WriteLine("requirement failed for: {0}, req: {1} ", encounter.Name, encounter.Requirement);
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