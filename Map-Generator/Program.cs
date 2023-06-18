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
        public static List<Encounter> Rooms = new List<Encounter>();

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
                var res = GetFloorPosition(floor, mainRoom.RoomTypeTag, name2);

                Console.WriteLine("res: " + res);
                // for (int i = 0; i < res; i++)
                //     Random.NextUInt();
                var encounters = JsonDecoder.Encounter[floor][name2][mainRoom.RoomTypeTag].Rooms;

                if (JsonDecoder.Encounter[floor][name2][mainRoom.RoomTypeTag].HasWeight())
                {
                    var mainRoomEncounter = mainRoom.Encounter;
                    encounters.Where(encounter1 =>
                    {
                        return (mainRoomEncounter != null && CheckEncounter(encounter1, mainRoom));
                    });
                    if (encounters.Count == 0)
                    {
                        Console.WriteLine("error?");
                    }

                    Random.GetWeightedElement(encounters, out mainRoomEncounter);
                    mainRoom.Encounter = mainRoomEncounter;
                }
                else
                    mainRoom.Encounter = encounters.Find(x => x.Tag == mainRoom.Tags);
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
                    float difficulty = enemy.GetDifficulty(0); //TODO: make gamemode generic
                    encounter.Enemies.Add(list2[i]);
                    num2 -= difficulty;
                    array[i]++;
                }

                while (num2 > 0f && list2.Count > 0)
                {
                    int num3 = Random.Range(0, list2.Count);

                    var enemy = list2[num3];
                    float difficulty2 = enemy.GetDifficulty(0); //TODO: make gamemode generic
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
                    return false;
                }

                // if (room.Stage != encounter.Stage) //TODO: two rooms should be ignored
                // {
                // return false;
                // }
                if (!Save.Check(encounter.Requirement))
                {
                    return false;
                }

                if (encounter.Seen) //&& !encounter.Repeatable <- TODO: repeatable only for a few rooms, which in a normal run can be ignored
                {
                    return false;
                }

                return true;
            }
        }

        public static string MapName { get; set; } = "mineearly";
        public static string MapNameEncounter { get; set; } = "mine";

        public static void GetRooms(RoomType[][] batches)
        {
            Console.WriteLine("");
            foreach (RoomType[] roomNames in batches)
            {
                foreach (RoomType roomName in roomNames)
                {
                    if (!Random.Chance(roomName.Chance))
                    {
                        Console.WriteLine("Skipping room [" + roomName.Stages.First() + "]: Chance failed");
                        Console.WriteLine("");
                        continue;
                    }

                    if (roomName.Requirement != null && Save.Check(roomName.Requirement))
                    {
                        Console.WriteLine("skipping: " + roomName.Requirement);
                        Console.WriteLine("");
                        continue;
                    }

                    // Console.WriteLine(Random.Value());
                    // Random.Count = 0;
                    string name2 = roomName.Stages[Random.Range(0, (uint)roomName.Stages.Count)];
                    // Console.WriteLine(Random.Value());
                    // Console.WriteLine("begin: " + Random.Count);
                    GetEncounter(MapNameEncounter, name2, roomName); //scoped randomness

                    // roomName.CanReload = roomName.Encounter != null && Encounter.SubFloor == 0;;

                    // Room.WeightDoor door;

                    // if (room.WeightDoors == null)
                    //     Random.NextUInt();
                    // else
                    //     Random.GetWeightedElement(room.WeightDoors, out door);

                    // if (roomName.Children)
                    // Random.NextUInt();
                    // else
                    // if (!roomName.Children)
                    // {
                    Initialize(roomName, name2);
                    // Console.WriteLine("end: " + Random.Count);
                    // }

                    // if (roomName.Children)
                    // Console.WriteLine(Random.Value());

                    Console.WriteLine(roomName.Encounter.Name);
                    if (roomName.Encounter?.Enemies != null)
                        foreach (var enemy in roomName.Encounter?.Enemies)
                        {
                            Console.WriteLine(enemy.Name);
                        }

                    // Console.WriteLine(room.Name);
                    Console.WriteLine("");
                    // Rooms.Add(room);
                }
            }
        }

        public static void Initialize(RoomType room, string name2)
        {
            if (room.Encounter.WeightDoors == null)
            {
                // if (!room.Extra)
                Random.NextUInt();
            }
            else
            {
                Random.GetWeightedElement(room.Encounter.WeightDoors, out var door);
                room.Encounter.Door = door.Door;
            }

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