using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Undermine;
using Newtonsoft.Json;

namespace Map_Generator
{
    static class Program
    {
        /// <summary>
        /// list of room with result of algorithm
        /// </summary>
        private static readonly List<RoomType?> Rooms = new();

        public static void GetRooms(RoomType[][] batches)
        {
            Console.WriteLine("");
            foreach (RoomType[] roomNames in batches)
            {
                LoadRoomNames(roomNames, null, false, -1);
            }
        }

        /// <summary>
        /// gets the encounter for the room
        /// </summary>
        /// <param name="roomNames"></param>
        /// <param name="zone">zone name -> e.g mine</param>
        /// <param name="roomSize">room size -> e.g small/large</param>
        /// <param name="previousRoom"></param>
        public static RoomType GetEncounter(string roomNames, string zone, string roomSize, RoomType? previousRoom)
        {
            RoomType room = null!;
            foreach (var roomName in roomNames.Split(','))
            {
                room = JsonDecoder.Rooms[roomName];
                zone = Save.GetZoneName(room);
                //using scope to make sure the random is not affected by other randoms
                using (new Rand.Scope(Rand.StateType.Default))
                {
                    var defaultRequirement =
                        JsonDecoder.Encounters[zone][roomSize][room.RoomTypeTag].Default.Requirement;
                    if (defaultRequirement != null && !Save.Check(defaultRequirement))
                        continue;

                    //filter encounters and put it into a list
                    List<Encounter?> encounters =
                        JsonDecoder.Encounters[zone][room.Stages.Count > 1 ? roomSize : room.Stages.First()][
                                room.RoomTypeTag].Rooms
                            .Where(enc => room.CheckEncounter(enc, previousRoom)).ToList();
                    if (encounters.Count == 0)
                        Console.WriteLine("error?");

                    //if the encounter has a weight, get a random encounter based on the weight
                    if (JsonDecoder.Encounters[zone][roomSize][room.RoomTypeTag].HasWeight())
                    {
                        if (Rand.GetWeightedElement(encounters, out Encounter mainRoomEncounter))
                            room.Encounter = mainRoomEncounter;
                    }
                    //else search for the encounter with the same tag as the room
                    else
                    {
                        if (!JsonDecoder.Encounters[zone][room.Stages.Count > 1 ? roomSize : room.Stages.First()][
                                room.RoomTypeTag].Rooms.Exists(encounter => encounter?.Tag == room.Tags))
                            throw new InvalidOperationException("room has unable to find " + room.Tags); //TODO: remove this for production
                        
                        room.Encounter = encounters.Find(encounter =>
                            encounter?.Tag == room.Tags &&
                            (encounter?.Requirement == null || Save.Check(encounter.Requirement)));
                    }

                    room.CanReload = room.Encounter is { SubFloor: 0 };

                    if (room.Encounter != null)
                        break;
                    // Console.WriteLine("encounter is null"); //TODO: add multiple extra encounters
                }
            }


            return room ?? throw new InvalidOperationException();
        }

        private static void LoadRoomNames(in RoomType[] roomNames, RoomType? previousRoom, bool sequence,
            int sequenceRecursionCount)
        {
            Console.WriteLine("new roomnames");
            foreach (RoomType roomName in roomNames)
            {
                Console.WriteLine("room: {0}, tag: {1}, chance: {2}", roomName.Name, roomName.RoomTypeTag,
                    roomName.Chance);
                if (!Rand.Chance(roomName.Chance))
                {
                    Console.WriteLine("Skipping room [ {0} ]: Chance ( {1} ) failed", roomName.Name,
                        roomName.Chance);
                    Console.WriteLine("");
                    return; //TODO: return or continue?
                }

                if (roomName.Requirement != null && !Save.Check(roomName.Requirement))
                {
                    Console.WriteLine("skipping: [{0}] due to: {1}, chance: {2}", roomName.RoomTypeTag,
                        roomName.Requirement, roomName.Chance);
                    Console.WriteLine("");
                    return; //TODO: break or continue?
                }

                string name2 = roomName.Stages[Rand.Range(0, (uint)roomName.Stages.Count)];

                // for (int i = 0; i < roomName.ExtraEncounters; i++)
                //     Rand.NextUInt(); //TODO: figure out when to use extra sprites?!?!?!?
                
                if (roomName.HasExtraEncounter)
                    Rand.NextUInt();
                
                RoomType room =
                    GetEncounter(roomName.Name, MapType.GetMapName(), name2, previousRoom); //scoped randomness

                if (room.Encounter != null)
                    room.Initialize(MapType.GetMapName(), name2);

                if (room.Encounter?.Enemies != null)
                {
                    foreach (var enemy in room.Encounter?.Enemies)
                        Console.WriteLine(enemy.Name);
                }

                if (room.Encounter == null)
                {
                    Console.WriteLine("skipping: [" + roomName.Name + "]" + " due to: " + "Encounter is null");
                    return;
                }

                //TODO: doorcost
                Rooms.Add(room);
                if (sequence)
                {
                    room.Secluded = true;
                    room.Encounter.SubFloor = previousRoom.Encounter.SubFloor;
                }

                room.Encounter.Sequence.AddRange(
                    JsonDecoder.Encounters[MapType.GetMapName()][name2][room.RoomTypeTag].Default.Sequence);

                if (sequenceRecursionCount != 0 && room.Encounter.Sequence.Count > 0)
                {
                    RoomType[] batch =
                        room.Encounter.Sequence.Select(room_name => JsonDecoder.Rooms[room_name]).ToArray();
                    Console.WriteLine("Reloading..");
                    LoadRoomNames(batch, room, sequence: true, --room.Encounter.SequenceRecursionCount);
                }

                if (previousRoom != null)
                {
                    // previousRoom.Branches[roomName.Direction] = room;
                }

                Console.WriteLine(Rand.Value());

                if (room.Encounter != null)
                    Console.WriteLine("room: {0} encounter name: {1}_{2}_{3} door: {4}, chance: {5}", room.Name, name2,
                        room.RoomTypeTag,
                        room.Encounter.Name,
                        room.Encounter.Door.ToString(),
                        room.Chance);
                previousRoom = room;
                Console.WriteLine("");
                Rooms.Add(room);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Save.Initialize("Save2.json");
            Rand.Initialize((uint)(Save.Seed + Save.floor_number));

            var level = JsonDecoder.Maps.First(map =>
                    map.Name == MapType.GetMapName() && (map.Requirement == null || Save.Check(map.Requirement)))
                .Levels[Save.FloorIndex];

            RoomType[][] batches = level.Rooms.Select(room => room.Select(x => JsonDecoder.Rooms[x]).ToArray())
                .ToArray()
                .Concat(level.RoomsMulti.Select(x => new[] { JsonDecoder.Rooms[x] })).ToArray();

            GetRooms(batches);
            Console.WriteLine(Rand.Value());
            var x = Save.hoodie_met_dungeon;
            var y = Save.floor_number;
            var z = MapType.GetMapName();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}