using System;
using System.Collections.Generic;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
using Map_Generator.Undermine;
using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Classes
{
    public class ExtraInformation
    {
        [JsonProperty("sprites")] public int Sprites { get; set; } = 0;
    }

    public class Room : IWeight
    {
        public override string ToString()
        {
            return $"Name: {Name}, Type: {RoomType}, Position: {Position}, Chance: {Chance}, Weight: {Weight}," +
                   $"Direction: {Direction}, Children: {Children}, Branches: {Branches.Count}, Neighbors: {Neighbors.Count}, " +
                   $"Encounter: {Encounter?.Name}, PreviousRoom: {PreviousRoom?.Name}, CanReload: {CanReload}, " +
                   $"Secluded: {Secluded}, ExtraEncounters: {ExtraEncounters}, IsHidden: {IsHidden}";
        }

        public Room Clone() => (Room)MemberwiseClone();

        public Room DeepClone()
        {
            string serializedObject = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Room>(serializedObject) ??
                   throw new InvalidOperationException("failed to do a deep copy");
        }

        [JsonProperty("stage")] public List<string> Stages { get; set; } = null!;
        [JsonProperty("roomtypetag")] public string RoomTypeTag { get; set; } = null!;
        [JsonProperty("roomtype")] public RoomType RoomType { get; set; } = RoomType.None;
       
        [JsonProperty("crawlspace")] private bool crawlspace = false;
        [JsonIgnore] public ZoneData.CrawlSpace? Crawlspace => crawlspace ? ZoneData.Crawlspace : null;

        private float chance = 1f;

        [JsonProperty("chance")]
        public float Chance
        {
            get => chance + StepChance * Save.floor_number + ZoneStepChance * Save.ZoneIndex;
            set => chance = value;
        }

        [JsonProperty("stepchance")] public float StepChance { get; set; } = 0;
        [JsonProperty("zonestepchance")] public float ZoneStepChance { get; set; } = 0;
        [JsonProperty("tags")] public string? Tag { get; set; }
        [JsonProperty("children")] public bool Children { get; set; } = false;
        [JsonProperty("encounter")] public bool HasExtraEncounter { get; set; } = false;
        [JsonProperty("icon")] private List<MapIcon>? _mapIcons { get; set; }

        [JsonIgnore] public List<MapIcon> MapIcons => _mapIcons ?? new List<MapIcon> { MapIcon.None }; //TODO: change this

        [JsonProperty("extrainformation")]
        public Dictionary<string, ExtraInformation> ExtraInformations { get; set; } = new();

        public int ExtraEncounters =>
            (ExtraInformations.TryGetValue(Save.GetZoneName(this), out ExtraInformation extraInformation)
                ? extraInformation.Sprites
                : 0) + (HasExtraEncounter ? 1 : 0);

        [JsonProperty("name")] public string Name { get; set; } = null!;
        [JsonProperty("branchweight")] private int _weight = 0;

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
                : _direction;

        [JsonIgnore] public Encounter? Encounter { get; set; }
        [JsonIgnore] public Room? PreviousRoom { get; set; }
        [JsonIgnore] public bool CanReload { get; set; }
        [JsonIgnore] public bool Secluded { get; set; }
        [JsonIgnore] public Vector2Int Position { get; set; } = Vector2Int.Zero;

        [JsonIgnore] public Dictionary<Direction, Room> Neighbors { get; set; } = new();
        [JsonIgnore] public Dictionary<Direction, Room> Branches { get; set; } = new();
        [JsonIgnore] public List<Item> SetPieces { get; set; } = new();
        [JsonIgnore] public List<Item> Extras { get; set; } = new();

        [JsonProperty("ishidden")] public bool IsHidden { get; set; } = false;

        public void Initialize(ZoneData data)
        {
            if (this.Encounter == null)
                return;
            
            if (Rand.GetWeightedElement(this.Encounter.WeightedDoors, out var door))
                this.Encounter.Door = door.Door;

            this.Encounter.DetermineEnemies(data); //not scoped randomness
            this.Encounter.Seen = true;
        }

        public bool CheckEncounter(Encounter? encounter, Room? previousRoom)
        {
            if (encounter == null)
            {
                BardLog.Log("null during check...");
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
                    BardLog.Log("not allowed encounter: {0}, noexit: {1}", encounter.Name, encounter.NoExit);
                    BardLog.Log("not allowed neighbor: {0}, noexit: {1}", previousRoom.Encounter.Name,
                        previousRoom.Encounter.NoExit);
                    BardLog.Log("");
                    return false;
                }
            }

            // if (room.Stage != encounter.Stage) //TODO: two rooms should be ignored
            // {
            // return false;
            // }
            if (encounter.Requirement != null && !Save.Check(encounter.Requirement))
            {
                BardLog.Log("requirement failed for: {0}, req: {1} ", encounter.Name, encounter.Requirement);
                return false;
            }

            if (encounter.Seen) //&& !encounter.Repeatable <- TODO: repeatable only for a few rooms, which in a normal run can be ignored
            {
                BardLog.Log("seen: {0}", encounter.Name);
                return false;
            }

            return true;
        }

        public bool IsValidNeighbor(Room neighbor, Direction direction)
        {
            if (Encounter == null)
                throw new Exception($"Unable to validate neighbor for Room [{Name}] without an Encounter!");
            if (neighbor.Encounter == null)
                throw new Exception($"Unable to validate neighbor for Room [{neighbor.Name}] without an Encounter!");

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

        public Encounter? ReloadEncounter(ZoneData zoneData)
        {
            if (!CanReload)
            {
                BardLog.Log("can't reload: {0}, subfloor: {1}", Name, Encounter?.SubFloor);
                return this.Encounter;
            }
            
            int? subFloor = 0;
            if (Encounter != null)
            {
                subFloor = Encounter.SubFloor;
            }

            Room room = Program.GetEncounter(this, this.PreviousRoom);
                
            //set this object to room
                
            if (room.Encounter != null)
            {
                room.Encounter.SubFloor = subFloor;
                room.Initialize(zoneData);
            }

            return room.Encounter;

        }
    }
}