using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Policy;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Undermine;
using static Map_Generator.Json.JsonDecoder;

namespace Map_Generator
{
    static class Program
    {
        public static List<Room> Rooms = new List<Room>();

        public static int GetFloorPosition(string floor, string name, string name2)
        {
            // Encounter.Keys.ToList().IndexOf(floor);
            int count = 0;
            foreach (var enc in Encounter)
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

        public static void GetSpecificRoom(string floor, string type, string name2, string? tag, out Room room)
        {
            using (Random.CreateScope())
            {
                var res = GetFloorPosition(floor, type, name2);

                for (int i = 0; i < res; i++)
                    Random.NextUInt();

                if (Encounter[floor][name2][type].HasWeight())
                    Random.GetWeightedElement(Encounter[floor][name2][type].Rooms, out room);
                else
                    room = Encounter[floor][name2][type].Rooms
                        .Find(x => x.Tag == tag);
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

        public static void DetermineEnemies(Room room, int floorNumber)
        {
            var data = JsonDecoder.ZoneData[0][0];
            var enemies = room.Enemies ?? data.Floors[floorNumber].Enemies;

            if (room.ProhibitedEnemies != null)
                enemies.RemoveAll(enemy => room.ProhibitedEnemies.Contains(enemy.Name));
            // enemies.RemoveAll(( element) => element.Data.IsDeprecated);
            if (enemies.Count <= 0)
                return;
            

            enemies.Shuffle();
            
            int enemyCombo = 0;
            // foreach (int enemyCombo2 in enemies)
            // {
            //     if (((list[0].Data as EnemyData).Type & enemyCombo2) != 0)
            //     {
            //         enemyCombo = enemyCombo2;
            //         break;
            //     }
            // }
            //I think this is the amount of enemies in the room
            int num = System.Math.Min(enemies.Count, GetEnemyTypeCount(data.EnemyTypeWeight));
            if (num <= 0)
                return;
            
            enemies.Shuffle();
            List<Enemy> list2 = new List<Enemy>();
            
            //get enemies and remove enemies that don't belong
            foreach (var enemy in enemies)
            {
                if ((num != 1 || enemy.CanBeSolo)) // && GameData.Instance.CheckSpawnCondition(item))
                {
                    if ((enemy.Type & enemyCombo) != 0 || enemyCombo == 0)
                    {
                        list2.Add(enemy);
                        enemyCombo ^= enemy.Type;
                    }
                    if (list2.Count == num)
                        break;
                }
            }
        }

        public static void GetRooms(RoomType[][] batches)
        {
            Console.WriteLine("");
            foreach (RoomType[] roomNames in batches)
            {
                foreach (RoomType roomName in roomNames)
                {
                    Random.Count = 0;
                    if (!Random.Chance(roomName.Chance))
                    {
                        Console.WriteLine("Skipping room [" + roomName.Tags + "]: Chance failed");
                        Console.WriteLine("");
                        continue;
                    }

                    string name2 = roomName.Stages[Random.Range(0, (uint)roomName.Stages.Count)];

                    GetSpecificRoom("mine", roomName.RoomTypeTag, name2, roomName.Tags, out var room);


                    // room.Difficulty ??= Encounter["mine"][name2][roomName.RoomTypeTag].Default.Difficulty;
                    room.Difficulty ??= Encounter["mine"][name2][roomName.RoomTypeTag].Default.Difficulty;
                    Room.WeightDoor door;

                    if (room.WeightDoors == null)
                        Random.NextUInt();
                    else
                        Random.GetWeightedElement(room.WeightDoors, out door);

                    if (!Random.Chance(room.Difficulty.First()))
                    {
                        Console.WriteLine("skipping");
                        // continue;
                    }

                    // Random.NextUInt(); //for initialize (in undermine)
                    Console.WriteLine(roomName.RoomTypeTag);
                    Console.WriteLine(Random.Value());

                    // Console.WriteLine(room.Name);
                    Console.WriteLine("");
                    // Rooms.Add(room);
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Random.Initialize(85064318 + 4);

            var level = JsonDecoder.Maps["mine"].Levels[4 - 1];

            var zone = JsonDecoder.ZoneData;

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