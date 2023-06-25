using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Policy;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.JsonClasses;
using Map_Generator.Undermine;
using static Map_Generator.Json.JsonDecoder;

namespace Map_Generator
{
    static class Program
    {
        /// <summary>
        /// list of room with result of algorithm
        /// </summary>
        public static List<RoomType?> Rooms = new();

        /// <summary>
        /// gets the encounter for the room
        /// </summary>
        /// <param name="zone">zone name -> e.g mine</param>
        /// <param name="roomSize">room size -> e.g small/large</param>
        /// <param name="mainRoom">main room refernce, this object will get the encounter</param>
        public static void GetEncounter(string zone, string roomSize, RoomType? mainRoom)
        {
            //using scope to make sure the random is not affected by other randoms
            using (Random.CreateScope())
            {
                //filter encounters and put it into a list
                var encounters = JsonDecoder.Encounter[zone][roomSize][mainRoom?.RoomTypeTag].Rooms
                    .Where(mainRoom.CheckEncounter).ToList();
                if (encounters.Count == 0)
                    Console.WriteLine("error?");

                //if the encounter has a weight, get a random encounter based on the weight
                if (JsonDecoder.Encounter[zone][roomSize][mainRoom.RoomTypeTag].HasWeight())
                {
                    if (Random.GetWeightedElement(encounters, out var mainRoomEncounter))
                        mainRoom.Encounter = mainRoomEncounter;
                }
                //else search for the encounter with the same tag as the room
                else
                    mainRoom.Encounter = encounters.Find(x =>
                        x?.Tag == mainRoom.Tags && (x?.Requirement == null || Save.Check(x?.Requirement!)));

                mainRoom.CanReload = mainRoom.Encounter is { SubFloor: 0 };

                if (mainRoom.Encounter == null)
                    Console.WriteLine("encounter is null");
            }
        }

        /// <summary>
        /// returns a random index based on the sum of weights and the individual weights,
        /// the higher the weight the higher the chance of getting that index
        /// </summary>
        /// <param name="enemyTypeWeight">array of weights</param>
        /// <returns>random index</returns>
        public static int GetEnemyTypeCount(int[] enemyTypeWeight)
        {
            int sum = enemyTypeWeight.Sum();

            int num2 = Random.RangeInclusive(1, sum);

            for (int i = 0; i < enemyTypeWeight.Length; i++)
            {
                num2 -= enemyTypeWeight[i];
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

        /// <summary>
        /// determines the enemies for the encounter
        /// </summary>
        /// <param name="encounter">encounter</param>
        /// <param name="data">zonedata</param>
        public static void DetermineEnemies(Encounter? encounter, ZoneData data)
        {
            if (!Random.Chance(encounter.Difficulty[0])) //TODO: check if we have to check encounter
            {
                Console.WriteLine("skipping room");
                return;
            }

            int floorNumber = Save.FloorNumber - 1;

            data.Initialize(); //TODO: check, maybe earlier or not needed
            Override? floorOverride = data.Floors[floorNumber].Override;
            float floorDifficulty =
                floorOverride?.Difficulty ??
                (data.BaseDifficulty + data.DifficultyStep * floorNumber); //TODO: difficulty int?
            
            int[] enemyTypeWeight = floorOverride?.EnemyTypeWeight ?? data.EnemyTypeWeight;
            List<Enemy> enemies = new List<Enemy>(encounter.Enemies ?? data.Floors[floorNumber].Enemies);
            encounter.Enemies ??= new List<Enemy>();

            if (encounter.ProhibitedEnemies != null)
                enemies.RemoveAll(enemy => encounter.ProhibitedEnemies.Contains(enemy.Name));
            
            if (enemies.Count <= 0)
            {
                Console.WriteLine("no enemies");
                return;
            }

            enemies.Shuffle();

            int enemyCombo = new[] { 3, 5, 6 }.FirstOrDefault(type => (enemies[0].Type & type) != 0);

            //I think this is the amount of enemies in the room
            enemies.RemoveAll(enemy => (enemy.Type & enemyCombo) == 0);

            int num = System.Math.Min(enemies.Count, GetEnemyTypeCount(enemyTypeWeight));
            if (num <= 0)
                return;

            enemies.Shuffle();

            List<Enemy> list2 = new List<Enemy>();
            
            //get enemies and remove enemies that don't belong
            foreach (var enemy in enemies.Where(enemy => num != 1 || enemy.CanBeSolo))
            {
                if ((enemy.Type & enemyCombo) != 0 || enemyCombo == 0)
                {
                    list2.Add(enemy);
                    enemyCombo ^= enemy.Type;
                }

                if (list2.Count == num)
                    break;
            }

            if (list2.Count > 0)
            {
                float num2 = floorDifficulty + encounter.Difficulty[1]; //TODO: null?
                int[] array = new int[list2.Count];
                for (int i = 0; i < list2.Count; i++)
                {
                    Enemy? enemy = list2[i];
                    float difficulty = enemy.GetDifficulty(); //TODO: make gamemode generic
                    encounter.Enemies.Add(list2[i]);
                    num2 -= difficulty;
                    array[i]++;
                }

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

        // public static string MapName { get; set; } = "mineearly";
        public static string MapName { get; set; } = "mineearly";

        // public static string MapNameEncounter { get; set; } = "mine";
        public static string MapNameEncounter { get; set; } = "mine";

        public static void GetRooms(RoomType[][] batches)
        {
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

                string name2 = roomName.Stages[Random.Range(0, (uint)roomName.Stages.Count)];

                Console.WriteLine(Random.Value());

                RoomType room = roomName.Clone();
                room.PreviousRoom = previousRoom;
                
                if (room.Encounter == null)
                    GetEncounter(MapNameEncounter, name2, room); //scoped randomness
                
                if (room.Encounter != null)
                    Initialize(room, name2);

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
                        room.Encounter.Sequence.Select(room1 => JsonDecoder.Rooms[room1]).ToArray();

                    LoadRoomNames(batch, room, sequence: true, --room.Encounter.SequenceRecursionCount);
                }

                if (previousRoom != null)
                {
                    // previousRoom.Branches[roomName.Direction] = room;
                }

                previousRoom = room;
                Console.WriteLine(Random.Value());
                Console.WriteLine("");
                // Rooms.Add(room);
            }
        }

        public static void Initialize(RoomType? room, string name2)
        {
            if (Random.GetWeightedElement(room?.Encounter?.WeightDoors, out var door))
                room.Encounter.Door = door.Door;


            room.Encounter.Difficulty ??=
                JsonDecoder.Encounter[MapNameEncounter][name2][room.RoomTypeTag].Default.Difficulty;

            ZoneData data = new ZoneData();

            //loop through data and check if requirement fits
            // foreach (ZoneData? zoneData in
            //          JsonDecoder.ZoneData[1]
            //              .Where(zoneData =>
            //                  Save.Check(zoneData.Requirements))) //TODO: change zonedata[0] to generic value
            // {
            //     data = zoneData;
            //     Console.WriteLine("found zonedata: {0}", data.Name);
            //     break;
            // }
            data = JsonDecoder.ZoneData[0][1];
            DetermineEnemies(room.Encounter, data); //not scoped randomness
            room.Encounter.Seen = true;
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Save.Initialize("Save2.json");
            Random.Initialize((uint)(Save.Seed + Save.floor_number));

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