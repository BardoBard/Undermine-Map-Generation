using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// gets the encounter for the room
        /// </summary>
        /// <param name="zone">zone name -> e.g mine</param>
        /// <param name="roomSize">room size -> e.g small/large</param>
        public void GetEncounter(string zone, string roomSize)
        {
            //using scope to make sure the random is not affected by other randoms
            using (new Rand.Scope(Rand.StateType.Default))
            {
                //filter encounters and put it into a list
                var encounters = JsonDecoder.Encounter[zone][roomSize][this.RoomTypeTag].Rooms
                    .Where(this.CheckEncounter).ToList();
                if (encounters.Count == 0)
                    Console.WriteLine("error?");

                //if the encounter has a weight, get a random encounter based on the weight
                if (JsonDecoder.Encounter[zone][roomSize][this.RoomTypeTag].HasWeight())
                {
                    if (Rand.GetWeightedElement(encounters, out var mainRoomEncounter))
                        this.Encounter = mainRoomEncounter;
                }
                //else search for the encounter with the same tag as the room
                else
                {
                    this.Encounter = encounters.Find(encounter =>
                        encounter?.Tag == this.Tags &&
                        (encounter?.Requirement == null || Save.Check(encounter.Requirement)));
                }

                this.CanReload = this.Encounter is { SubFloor: 0 };

                if (this.Encounter == null)
                    Console.WriteLine("encounter is null");
            }
        }

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

            Console.WriteLine("found zonedata: {0}", data.Name);
            // data = JsonDecoder.ZoneData[0][1];
            this.Encounter.DetermineEnemies(data); //not scoped randomness
            this.Encounter.Seen = true;
        }

        public bool CheckEncounter(Encounter? encounter)
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
            if (this.PreviousRoom?.Encounter != null)
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