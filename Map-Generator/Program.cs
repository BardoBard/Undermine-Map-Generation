using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Policy;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Parsing;
using Map_Generator.Undermine;
using static Map_Generator.Json.JsonDecoder;

namespace Map_Generator
{
    static class Program
    {
        public static List<RoomType> Rooms = new();

        public static int GetFloorPosition(string floor, string name, string name2)
        {
            // Encounter.Keys.ToList().IndexOf(floor);
            int count = 0;
            foreach (var enc in JsonDecoder.Encounter)
            {
                foreach (var stages in enc.Value.Where(stages => stages.Value.Any(roomType => roomType.Key == name)))
                {
                    if (enc.Key == floor && stages.Key == name2)
                        return count;
                    ++count;
                }
            }

            return count;
        }

        public static void GetEncounter(string floor, string name2, RoomType mainRoom)
        {
            using (Random.CreateScope())
            {
                // var res = GetFloorPosition(floor, mainRoom.RoomTypeTag, name2);

                // Console.WriteLine("res: " + res);
                // for (int i = 0; i < res; i++)
                //     Random.NextUInt();

                var encounters = JsonDecoder.Encounter[floor][name2][mainRoom.RoomTypeTag].Rooms;
                encounters.RemoveAll(encounter1 => !CheckEncounter(encounter1, mainRoom));
                if (encounters.Count == 0)
                {
                    Console.WriteLine("error?");
                }

                if (JsonDecoder.Encounter[floor][name2][mainRoom.RoomTypeTag].HasWeight())
                {
                    var mainRoomEncounter = mainRoom.Encounter;

                    if (Random.GetWeightedElement(encounters, out mainRoomEncounter))
                        mainRoom.Encounter = mainRoomEncounter;
                }
                else
                    mainRoom.Encounter = encounters.Find(x => x.Tag == mainRoom.Tags && Save.Check(x.Requirement));

                mainRoom.CanReload = mainRoom.Encounter is { SubFloor: 0 };

                if (mainRoom.Encounter == null)
                    Console.WriteLine("encounter is null");
            }
        }

        public static int GetEnemyTypeCount(int[] zoneData)
        {
            int num = zoneData.Sum();

            int num2 = Random.RangeInclusive(1, num);
            for (int i = 0; i < zoneData.Length; i++)
            {
                num2 -= zoneData[i];
                if (num2 <= 0)
                    return i;
            }

            return 0;
        }

        enum EnemyCombo
        {
            TieUp = 1,
            Disturb = 2,
            Urgent = 4
        }

        public static void DetermineEnemies(Encounter encounter, int floorNumber)
        {
            if (!Random.Chance(encounter.Difficulty[0]))
            {
                Console.WriteLine("skipping room");
                return;
            }

            var data = JsonDecoder.ZoneData[0][0];
            data.Initialize();
            List<Enemy> enemies = new List<Enemy>(encounter.Enemies ?? data.Floors[floorNumber].Enemies);
            encounter.Enemies ??= new List<Enemy>();

            if (encounter.ProhibitedEnemies != null)
                enemies.RemoveAll(enemy => encounter.ProhibitedEnemies.Contains(enemy.Name));
            // enemies.RemoveAll(( element) => element.Data.IsDeprecated);
            if (enemies.Count <= 0)
            {
                Console.WriteLine("no enemies");
                return;
            }

            enemies.Shuffle();

            int enemyCombo = new[] { 3, 5, 6 }.FirstOrDefault(type => (enemies[0].Type & type) != 0);

            //I think this is the amount of enemies in the room
            enemies.RemoveAll(enemy => (enemy.Type & enemyCombo) == 0);

            int num = System.Math.Min(enemies.Count, GetEnemyTypeCount(data.EnemyTypeWeight));
            if (num <= 0)
                return;

            enemies.Shuffle();
            List<Enemy> list2 = new List<Enemy>();

            //get enemies and remove enemies that don't belong
            foreach (var enemy in enemies.Where(
                         enemy => (num != 1 ||
                                   enemy.CanBeSolo) /* && GameData.Instance.CheckSpawnCondition(item)     */))
            {
                if ((enemy.Type & enemyCombo) != 0 || enemyCombo == 0)
                {
                    list2.Add(enemy);
                    enemyCombo ^= enemy.Type;
                }

                if (list2.Count == num)
                {
                    break;
                }
            }

            if (list2.Count > 0)
            {
                float num2 =
                    (data.Floors[floorNumber].Override != null ? data.Floors[floorNumber].Override.Difficulty : 0) +
                    encounter.Difficulty[1];
                int[] array = new int[list2.Count];
                for (int i = 0; i < list2.Count; i++)
                {
                    Enemy? enemy = list2[i];
                    float difficulty = enemy.GetDifficulty(); //TODO: make gamemode generic
                    encounter.Enemies.Add(list2[i]);
                    num2 -= difficulty;
                    array[i]++;
                }

                Console.WriteLine("list2 count: " + list2.Count);
                Console.WriteLine("num2: " + num2);

                while (num2 > 0f && list2.Count > 0)
                {
                    int num3 = Random.Range(0, list2.Count);

                    var enemy = list2[num3];
                    float difficulty2 = enemy.GetDifficulty(); //TODO: make gamemode generic
                    if (difficulty2 > num2 || (enemy.Max > 0 && enemy.Max == array[num3]))
                    {
                        list2.RemoveAt(num3);
                        continue;
                    }

                    encounter.Enemies.Add(list2[num3]);
                    num2 -= difficulty2;
                    array[num3]++;
                }
            }

            list2.Clear();
        }

        public static bool CheckEncounter(Encounter encounter, RoomType room)
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
            if (room.PreviousRoom != null && !room.PreviousRoom.Encounter.AllowNeighbor(encounter))
            {
                Console.WriteLine("not allowed neighbor");
                return false;
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
                Console.WriteLine("seen");
                return false;
            }

            return true;
        }

        public static string MapName { get; set; } = "mineearly";
        public static string MapNameEncounter { get; set; } = "mine";

        public static void GetRooms(RoomType[][] batches)
        {
            Console.WriteLine("");
            foreach (RoomType[] roomNames in batches)
            {
                LoadRoomNames(roomNames, null, false, -1);
            }
        }

        private static void LoadRoomNames(RoomType[] roomNames, RoomType previousRoom, bool sequence,
            int sequenceRecursionCount)
        {
            foreach (RoomType roomName in roomNames)
            {
                if (!Random.Chance(roomName.Chance))
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

                Console.WriteLine(Random.Value());
                // Random.Count = 0;
                string name2 = roomName.Stages[Random.Range(0, (uint)roomName.Stages.Count)];
                // Console.WriteLine(Random.Value());
                // Console.WriteLine("begin: " + Random.Count);
                var room = roomName.Clone();
                if (room.Encounter == null)
                    GetEncounter(MapNameEncounter, name2, room); //scoped randomness

                // if (roomName.Children)
                // Random.NextUInt();
                // else
                // if (!roomName.Children)
                // {
                if (room.Encounter != null)
                    Initialize(room, name2);
                // Console.WriteLine("end: " + Random.Count);
                // }

                Console.WriteLine(Random.Value());

                if (room.Encounter?.Enemies != null)
                {
                    foreach (var enemy in room.Encounter?.Enemies)
                        Console.WriteLine(enemy.Name);
                }

                if (room.Encounter != null)
                {
                    Console.WriteLine(room.Encounter.Name);
                }


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
                        room.Encounter.Sequence.Select(room => JsonDecoder.Rooms[room]).ToArray();

                    LoadRoomNames(batch, room, sequence: true, --room.Encounter.SequenceRecursionCount);
                }

                if (previousRoom != null)
                {
                    // previousRoom.Branches[roomName.Direction] = room;
                }

                previousRoom = room;
                Console.WriteLine("");
                // Rooms.Add(room);
            }
        }

        public static void Initialize(RoomType room, string name2)
        {
            if (Random.GetWeightedElement(room.Encounter.WeightDoors, out var door))
                room.Encounter.Door = door.Door;


            room.Encounter.Difficulty ??=
                JsonDecoder.Encounter[MapNameEncounter][name2][room.RoomTypeTag].Default.Difficulty;
            DetermineEnemies(room.Encounter, Save.floor_number - 1); //not scoped randomness
            room.Encounter.Seen = true;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Save.Initialize("Save2.json");
            Random.Initialize((uint)(85064318 + Save.floor_number));

            var level = JsonDecoder.Maps[MapName].Levels[Save.floor_number - 1];

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