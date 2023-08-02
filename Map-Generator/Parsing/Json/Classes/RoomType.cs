using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Map_Generator.Json;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using Map_Generator.Undermine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Map_Generator.Parsing.Json.Classes
{
    public class ExtraInformation
    {
        [JsonProperty("sprites")] public int Sprites { get; set; } = 0;
    }

    public class RoomType : IWeight
    {
        public RoomType Clone() => (RoomType)MemberwiseClone();

        public RoomType DeepClone()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<RoomType>(serializedObject) ??
                   throw new InvalidOperationException("failed to do a deep copy");
        }

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
        [JsonProperty("tags")] public string? Tag { get; set; }
        [JsonProperty("children")] public bool Children { get; set; } = false;
        [JsonProperty("encounter")] public bool HasExtraEncounter { get; set; } = false;
        [JsonProperty("icon")] public MapIcon MapIcon { get; set; } = MapIcon.None;

        [JsonProperty("extrainformation")]
        public Dictionary<string, ExtraInformation> ExtraInformations { get; set; } = new();

        public int ExtraEncounters =>
            (ExtraInformations.TryGetValue(Save.GetZoneName(this), out ExtraInformation ExtraInformation)
                ? ExtraInformation.Sprites
                : 0) + (HasExtraEncounter ? 1 : 0);

        [JsonProperty("name")] public string Name { get; set; } = null!;
        [JsonProperty("branchweight")] 
        private int _weight = 0;
        public int Weight
        {
            get =>
                (int)(Encounter is { Branchweight: { } }
                    ? Encounter.Branchweight
                    : this._weight);
            set => this._weight = value; //TODO: this isn't nessesary I think
        }

        [JsonProperty("doorcost")] public int? DoorCost { get; set; }
        [JsonProperty("requirements")] public string? Requirement { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        private Direction _direction = Direction.None;

        public Direction Direction =>
            Encounter != null && Encounter.Direction != Direction.Undetermined
                ? Encounter.Direction
                : this._direction;

        [JsonIgnore] public Encounter? Encounter { get; set; }
        [JsonIgnore] public RoomType? PreviousRoom { get; set; }
        [JsonIgnore] public bool CanReload { get; set; }
        [JsonIgnore] public bool Secluded { get; set; }
        [JsonIgnore] public Vector2Int Position { get; set; } = Vector2Int.Zero;

        [JsonIgnore] public Dictionary<Direction, RoomType> Neighbors { get; } = new();
        [JsonIgnore] public Dictionary<Direction, RoomType> Branches { get; set; } = new();

        [JsonProperty("ishidden")] public bool IsHidden { get; set; } = false;

        public void Initialize(string mapNameEncounter, string name2)
        {
            if (this.Encounter == null)
                return;

            if (Rand.GetWeightedElement(
                    this.Encounter.WeightedDoors ?? JsonDecoder.Encounters[mapNameEncounter][name2][this.RoomTypeTag]
                        .Default.WeightedDoors, out var door))
                this.Encounter.Door = door.Door;


            this.Encounter.Difficulty ??=
                JsonDecoder.Encounters[mapNameEncounter][name2][this.RoomTypeTag].Default.Difficulty;

            // loop through data and check if requirement fits
            ZoneData data = ZoneData.GetZoneData();
            Console.WriteLine("found zonedata: {0}, with requirement: {1}", data.Name, data.Requirements);
            this.Encounter.DetermineEnemies(data); //not scoped randomness
            this.Encounter.Seen = true;
        }

        public bool CheckEncounter(Encounter? encounter, RoomType? previousRoom)
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

        public bool IsValidNeighbor(RoomType neighbor, Direction direction)
        {
            if (Encounter == null)
                Console.WriteLine("Unable to validate neighbor for Room [{0}] without an Encounter!", this.Name);
            if (neighbor.Encounter == null)
                Console.WriteLine("Unable to validate neighbor for Room [{0}] without an Encounter!", neighbor.Name);

            if (direction == Enums.Direction.None)
                return Encounter.AllowNeighbor(neighbor.Encounter);


            return Encounter.AllowNeighbor(direction) && neighbor.Encounter.AllowNeighbor(direction.Opposite());
        }

        public void Move(Direction direction)
        {
            Position += direction switch
            {
                Direction.North => Vector2Int.Up,
                Direction.South => Vector2Int.Down,
                Direction.East => Vector2Int.Right,
                Direction.West => Vector2Int.Left,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Invalid direction")
            };
        }

        [JsonIgnore] public bool Skip { get; set; }
    }
}