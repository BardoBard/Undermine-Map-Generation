using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Undermine;

namespace Map_Generator
{
    static class Program
    {
        /// <summary>
        /// list of room with result of algorithm
        /// </summary>
        private static readonly List<RoomType?> Rooms = new();

        // public static string MapName { get; set; } = "mineearly";
        public static string MapName { get; set; } = "dungeon";

        // public static string MapNameEncounter { get; set; } = "mine";
        public static string MapNameEncounter { get; set; } = "dungeon";

        public static void GetRooms(RoomType[][] batches)
        {
            // for (int i = 0; i < 40; i++)
            //     Console.WriteLine(Random.Value());

            Console.WriteLine("");
            foreach (RoomType[] roomNames in batches)
            {
                LoadRoomNames(roomNames, null, false, -1);
                // Random.PreviousUInt();
            }
        }

        private static void LoadRoomNames(RoomType[] roomNames, RoomType? previousRoom, bool sequence,
            int sequenceRecursionCount)
        {
            foreach (RoomType roomName in roomNames)
            {
                if (!Rand.Chance(roomName.Chance))
                {
                    Console.WriteLine("Skipping room [" + roomName.Stages.First() + "]: Chance failed");
                    Console.WriteLine("");
                    return;
                }

                if (roomName.Requirement != null && Save.Check(roomName.Requirement))
                {
                    Console.WriteLine("skipping: " + roomName.Requirement);
                    Console.WriteLine("");
                    return;
                }

                string name2 = roomName.Stages[Rand.Range(0, (uint)roomName.Stages.Count)];

                Console.WriteLine(Rand.Value());

                RoomType room = roomName.Clone();
                room.PreviousRoom = previousRoom;
                Console.WriteLine(Rand.Value());

                if (room.Encounter == null) 
                    room.GetEncounter(MapNameEncounter, name2); //scoped randomness

                Console.WriteLine("initializing");
                if (room.Encounter != null) 
                    room.Initialize(MapNameEncounter, name2);

                if (room.Encounter?.Enemies != null)
                {
                    foreach (var enemy in room.Encounter?.Enemies)
                        Console.WriteLine(enemy.Name);
                }

                if (room.Encounter != null)
                    Console.WriteLine(room.Encounter.Name);
                Console.WriteLine(Rand.Value());


                if (room.Encounter == null)
                {
                    Console.WriteLine("skipping: " + roomName.Stages.First() + " no encounter");
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
                    JsonDecoder.Encounter[MapNameEncounter][name2][room.RoomTypeTag].Default.Sequence);

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

                previousRoom = room;
                Console.WriteLine(Rand.Value());
                Console.WriteLine("");
                // Rooms.Add(room);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Rand.Initialize(123456);
            using (new Rand.Scope(Rand.StateType.Layout))
            {
                for (int i = 0; i < 5; i++)
                    Console.WriteLine(Rand.Value());

                Console.WriteLine("");
                using (new Rand.Scope(Rand.StateType.Default))
                {
                    Console.WriteLine(Rand.Value());
                    Console.WriteLine(Rand.Value());
                    Console.WriteLine(Rand.Value());
                }
                Console.WriteLine("");
                for (int i = 0; i < 5; i++)
                    Console.WriteLine(Rand.Value());
                    
                Console.WriteLine("");
                using (new Rand.Scope(Rand.StateType.Default))
                {
                    Console.WriteLine(Rand.Value());
                    Console.WriteLine(Rand.Value());
                    Console.WriteLine(Rand.Value());
                }
            }

            Save.Initialize("Save2.json");
            Rand.Initialize((uint)(Save.Seed + Save.floor_number));

            var level = JsonDecoder.Maps[MapName].Levels[Save.FloorNumber - 1];

            RoomType[][] batches = level.Rooms.Select(room => room.Select(x => JsonDecoder.Rooms[x]).ToArray())
                .ToArray()
                .Concat(level.RoomsMulti.Select(x => new[] { JsonDecoder.Rooms[x] })).ToArray();

            GetRooms(batches);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}