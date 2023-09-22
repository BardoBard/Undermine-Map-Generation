﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Json;
using Map_Generator.Math;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Undermine;

namespace Map_Generator
{
    static class Program
    {
        /// <summary>
        /// list of room with result of algorithm
        /// </summary>
        private static readonly List<RoomType> Rooms = new();

        public static void GetRooms(RoomType[][] batches)
        {
            BardLog.Log("");
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
                room = GetRoom(roomName).DeepClone();
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
                        BardLog.Log("error?");

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
                                room.RoomTypeTag].Rooms.Exists(encounter => encounter?.Tag == room.Tag))
                            throw new InvalidOperationException("room has unable to find " +
                                                                room.Tag); //TODO: remove this for production

                        room.Encounter = encounters.Find(encounter =>
                            encounter?.Tag == room.Tag &&
                            (encounter?.Requirement == null || Save.Check(encounter.Requirement)));
                        //?.DeepClone(); //TODO: clone?!
                    }

                    room.CanReload = room.Encounter is { SubFloor: 0 };

                    if (room.Encounter != null)
                        break;
                    // BardLog.Log("encounter is null"); //TODO: add multiple extra encounters
                }
            }


            return room ?? throw new InvalidOperationException();
        }

        private static void LoadRoomNames(in RoomType[] roomNames, RoomType? previousRoom, bool sequence,
            int sequenceRecursionCount)
        {
            BardLog.Log("new roomnames");
            foreach (RoomType roomName in roomNames)
            {
                BardLog.Log("room: {0}, tag: {1}, chance: {2}", roomName.Name, roomName.RoomTypeTag,
                    roomName.Chance);
                if (!Rand.Chance(roomName.Chance))
                {
                    BardLog.Log("Skipping room [ {0} ]: Chance ( {1} ) failed", roomName.Name,
                        roomName.Chance);
                    BardLog.Log("");
                    return; //TODO: return or continue?
                }

                if (roomName.Requirement != null && !Save.Check(roomName.Requirement))
                {
                    BardLog.Log("skipping: [{0}] due to: {1}, chance: {2}", roomName.RoomTypeTag,
                        roomName.Requirement, roomName.Chance);
                    BardLog.Log("");
                    return; //TODO: break or continue?
                }

                string name2 = roomName.Stages[Rand.Range(0, (uint)roomName.Stages.Count)];

                // for (int i = 0; i < roomName.ExtraEncounters; i++)
                //     Rand.NextUInt(); //TODO: figure out when to use extra sprites?!?!?!?

                if (roomName.HasExtraEncounter)
                    Rand.NextUInt();

                RoomType room =
                    GetEncounter(roomName.Name, MapType.GetMapName(), name2, previousRoom); //scoped randomness

                room.PreviousRoom = previousRoom;

                room.Initialize(Zonedata, MapType.GetMapName(), name2);

                if (room.Encounter?.RoomEnemies != null)
                {
                    foreach (var enemy in room.Encounter?.RoomEnemies)
                        BardLog.Log(enemy.Name);
                }

                if (room.Encounter == null)
                {
                    BardLog.Log("skipping: [" + roomName.Name + "]" + " due to: " + "Encounter is null");
                    return;
                }

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
                        room.Encounter.Sequence.Select(GetRoom).ToArray();

                    BardLog.Log("Reloading..");
                    LoadRoomNames(batch, room, sequence: true, --room.Encounter.SequenceRecursionCount);
                }

                if (previousRoom != null)
                    previousRoom.Branches[roomName.Direction] = room;


                BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);

                if (room.Encounter != null)
                    BardLog.Log("room: {0} encounter name: {1}_{2}_{3} door: {4}, chance: {5}", room.Name, name2,
                        room.RoomTypeTag,
                        room.Encounter.Name,
                        room.Encounter.Door.ToString(),
                        room.Chance);
                previousRoom = room;
                BardLog.Log("");
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MapGenerator());
        }

        public static ZoneData Zonedata = null!;

        public static readonly List<RoomType> PositionedRooms = new(20);

        public static void Start(string saveJsonFile)
        {
            JsonDecoder.ReadJson();
            ClearAll();
            Save.Initialize(saveJsonFile);
            Rand.Initialize((uint)(Save.Seed + Save.floor_number));
            var level = JsonDecoder.Maps.First(map =>
                    map.Name == MapType.GetMapName() && (map.Requirement == null || Save.Check(map.Requirement)))
                .Levels[Save.FloorIndex];


            Zonedata = ZoneData.GetZoneData();

            RoomType[][] batches = level.Rooms.Select(room => room.Select(GetRoom).ToArray())
                .ToArray()
                .Concat(level.RoomsMulti.Select(roomName => new[] { GetRoom(roomName) })).ToArray();

            foreach (var encounter in from encountersValue in JsonDecoder.Encounters.Values
                     from encounters in encountersValue.Values
                     from encounter in encounters
                     select encounter)
                encounter.Value.Initialize();

            BardLog.Log("found zonedata: {0}, with requirement: {1}", Zonedata.Name, Zonedata.Requirements);

            GetRooms(batches);
            BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
            BardLog.Log("starting get room mapping");
            GetRoomMapping();
            BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);

            BardLog.Log("setpieces");
            if (Zonedata.SetPieces != null)
                foreach (var setPiece in Zonedata.SetPieces)
                {
                    PlaceExtras(setPiece, AutoSpawnType.SetPieces);
                }


            BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
            BardLog.Log("");
            BardLog.Log("extras");

            if (Zonedata.Extras != null)
                foreach (var extra in Zonedata.Extras)
                {
                    PlaceExtras(extra, AutoSpawnType.Extras);
                }

            BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
            BardLog.Log("");

            BardLog.Log("resources");
            if (Zonedata.Resources != null)
                foreach (var extra in Zonedata.Resources)
                {
                    PlaceExtras(extra, AutoSpawnType.Extras);
                }

            BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
            BardLog.Log("");

            AddCrawlSpace(ZoneData.Crawlspace);

            if (MapType.GetMap() == MapType.MapName.dungeon && Save.priestessentrance)
                AddCrawlSpace(ZoneData.PriestessCrawlSpace);
        }

        private static void AddCrawlSpace(ZoneData.CrawlSpace extra)
        {
            Spawn(delegate(Item item)
            {
                List<RoomType> list = new List<RoomType>(Rooms);
                list.Shuffle();
                foreach (RoomType? room in list.Where(room =>
                             room.Encounter != null && ((room.Encounter.AutoSpawn & (int)AutoSpawnType.Extras) != 0)))
                {
                    BardLog.Log("crawl space: {0}", room.Encounter?.Name);
                    room.Extras.Add(item);
                    Debug.Assert(room.Encounter != null, "room.Encounter != null");
                    room.Encounter!.HasCrawlSpace = true;
                    return;
                }
            }, extra);
        }

        private static RoomType GetRoom(string roomName) => Zonedata.Rooms[Save.FloorIndex].ContainsKey(roomName)
            ? Zonedata.Rooms[Save.FloorIndex][roomName]
            : JsonDecoder.Rooms[roomName];

        private static void PlaceExtras<T>(T? extras, AutoSpawnType mask) where T : ZoneData.DefaultInformation
        {
            if (extras == null)
                return;

            Spawn(delegate(Item item)
            {
                BardLog.Log("");
                Rooms.Shuffle();
                foreach (var roomType in Rooms)
                {
                    BardLog.Log("name: {0}_{1} autospawn: {2}", roomType.Name, roomType.Encounter.Name,
                        roomType.Encounter.AutoSpawn);
                }

                foreach (RoomType room in Rooms)
                {
                    if ((room.Encounter?.AutoSpawn & (int)mask) != 0)
                    {
                        if (mask == AutoSpawnType.SetPieces)
                        {
                            // BardLog.Log("room: {0}, name: {1}", room.Name, item.Name);
                            room.SetPieces.Add(item);
                        }
                        else
                        {
                            BardLog.Log("room: {0}, name: {1}", room.Encounter.Name, item.Name);
                            room.Extras.Add(item);
                        }

                        break;
                    }
                }
            }, extras);
        }

        private delegate void PreCallback(Item item);

        private static void Spawn<T>(PreCallback preCallback, T extras) where T : ZoneData.DefaultInformation
        {
            ZoneData.DefaultInformation.OverrideDefaultInformation? overrideData =
                extras.Override.Count >= Save.FloorNumber ? extras.Override[Save.FloorIndex] : null;

            if (overrideData is { Enabled: false })
                return;


            int num = Rand.RangeInclusive((overrideData ?? extras as ZoneData.IAbsolutes).Min,
                (overrideData ?? extras as ZoneData.IAbsolutes).Max);
            BardLog.Log("num: {0}", num);
            for (int i = 0; i < num; i++)
            {
                Item? item = GetWeightedSpawnData(extras);
                if (item == null) continue;

                BardLog.Log("name: {0}, weight: {1}", item.Name, item.Weight);

                BardLog.Log("");
                preCallback(item);
            }
        }

        private static Item? GetWeightedSpawnData<T>(T? extras) where T : ZoneData.DefaultInformation
        {
            if (extras?.Items == null)
                return null;

            foreach (var item in extras.Items)
            {
                item.AdjustedWeight =
                    item.Requirement == null || Save.Check(item.Requirement)
                        ? item.Weight
                        : 0;
            }

            int sum = extras.Items.Sum(item => item.AdjustedWeight);
            if (!extras.Percent100 && sum == 0)
            {
                // Rand.NextUInt();
                return null;
            }

            BardLog.Log("percent: {0}", extras.Percent100 ? 100 : sum);

            int randomNumber = Rand.RangeInclusive(1, extras.Percent100 ? 100 : sum);

            foreach (Item? item in extras.Items)
            {
                randomNumber -= item.AdjustedWeight;
                if (randomNumber <= 0)
                {
                    return item;
                }
            }

            return null;
        }

        public enum AutoSpawnType
        {
            Ornaments = 1,
            Props = 2,
            Water = 4,
            Rewards = 8,
            Extras = 0x10,
            SetPieces = 0x20
        }
        // private static T GetWeightedItem<T>(List<T> items)

        private static void ClearAll()
        {
            // foreach (var encounter in Rooms.Select(room => room.Encounter).Where(encounter => encounter != null))
            // {
            //     encounter.Skip = false;
            //     encounter.Seen = false;
            // }

            Rooms.Clear();
            PositionedRooms.Clear();
        }

        private static void GetRoomMapping()
        {
            RoomType startingRoom = Rooms[0] ?? throw new InvalidOperationException("starting room is null");
            foreach (var room in Rooms)
            {
                BardLog.Log("room name: {0}, direction: {1}, door: {2}, ", room.Encounter?.Name, room.Direction,
                    room.Encounter.Door);
            }

            int i = 0;
            while (i < 10)
            {
                foreach (RoomType room in Rooms)
                {
                    room.Neighbors.Clear();
                }

                PositionedRooms.Clear();
                PositionedRooms.Add(startingRoom);
                for (int j = 1; j < Rooms.Count && SetRoomPosition(Rooms[j]); j++)
                {
                    BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
                    PositionedRooms.Add(Rooms[j]);
                }

                foreach (var mPositionedRoom in PositionedRooms)
                {
                    BardLog.Log("room name: {0} ({2},{3}), door: {1}, \nnoexit: {4}, branchweight: {5}",
                        mPositionedRoom.Encounter.Name,
                        mPositionedRoom.Encounter.Door.ToString(),
                        mPositionedRoom.Position.x, mPositionedRoom.Position.y,
                        mPositionedRoom.Encounter.NoExit.ToString(),
                        mPositionedRoom.Weight);
                }

                BardLog.Log(Rand.PeekValue(), BardLog.LogToFileAndConsole);
                if (PositionedRooms.Count == Rooms.Count)
                {
                    break;
                }

                BardLog.Log("Layout failed, trying again!");
                foreach (RoomType room2 in Rooms)
                {
                    // room2.ReloadEncounter();
                    throw new Exception("reload encounter");
                    // room2.Position = Vector2Int.Zero;
                }

                int num = i + 1;
                i = num;
            }

            foreach (RoomType room10 in Rooms)
            {
                if (room10.Encounter == null) continue;

                if (!room10.Secluded && room10.Encounter.Door == Door.Secret)
                {
                    foreach (Direction kCardinalDirection2 in DirectionExtension.CardinalDirections)
                    {
                        RoomType? room4 = GetRoom(GetRoomPosition(room10.Position, kCardinalDirection2));

                        if (room4 == null || !room10.IsValidNeighbor(room4, kCardinalDirection2))
                            continue;

                        if (room4.Encounter.Door is Door.None or Door.Hidden or Door.Crystal || room4.Secluded)
                            continue;

                        room10.Neighbors[kCardinalDirection2] = room4;
                        room4.Neighbors[kCardinalDirection2.Opposite()] = room10;
                    }
                }

                if (room10.Encounter.Door != Door.Normal)
                {
                    continue;
                }

                foreach (Direction kCardinalDirection3 in DirectionExtension.CardinalDirections)
                {
                    RoomType? room5 = GetRoom(GetRoomPosition(room10.Position, kCardinalDirection3));

                    if (room5 == null || room5.Encounter.Door != Door.Normal) continue;

                    if (room10.IsValidNeighbor(room5, kCardinalDirection3))
                    {
                        BardLog.Log("neighbor is valid {0}, {1}", room10.Encounter.Name, room5.Encounter.Name);

                        if (Rand.Chance(Zonedata.Floors[Save.FloorIndex].Override.Connectivity ??
                                        Zonedata.Connectivity))
                        {
                            room10.Neighbors[kCardinalDirection3] = room5;
                            room5.Neighbors[kCardinalDirection3.Opposite()] = room10;
                        }
                    }
                }
            }

            // foreach (Room room in Rooms)
            // {
            //     RequireExt[] componentsInChildren = room.GetComponentsInChildren<RequireExt>(includeInactive: true);
            //     RequireExt[] array = componentsInChildren;
            //     foreach (RequireExt requirement in array)
            //     {
            //         requirement.SpawnTable.Spawn(
            //             delegate(Spawner.SpawnData spawnData, out Vector3 position, out Transform parent)
            //             {
            //                 Room room6 = null;
            //                 if (requirement.Location == RequireExt.SpawnLocation.RandomRoom)
            //                 {
            //                     List<Room> list = new List<Room>(Rooms);
            //                     list.Shuffle();
            //                     foreach (Room item in list)
            //                     {
            //                         if ((item.Encounter.AutoSpawn & Encounter.AutoSpawnType.Extras) != 0 &&
            //                             item != room)
            //                         {
            //                             room6 = item;
            //                             break;
            //                         }
            //                     }
            //                 }
            //                 else if (requirement.Location == RequireExt.SpawnLocation.SameRoom)
            //                 {
            //                     room6 = room;
            //                 }
            //
            //                 Debug.AssertFormat(room6 != null, "No Room found for Requirement [{0}]", requirement);
            //                 if (room6 != null)
            //                 {
            //                     room6.AddExtra(spawnData);
            //                 }
            //
            //                 position = Vector3.zero;
            //                 parent = null;
            //                 return false;
            //             }, delegate(Spawnable spawnable)
            //             {
            //                 if (spawnable is Entity)
            //                 {
            //                     Entity entity = spawnable as Entity;
            //                     requirement.Entity.Link = entity;
            //                     entity.Link = requirement.Entity;
            //                 }
            //             }, null);
            //     }
            // }

            // foreach (RoomType room11 in Rooms)
            // {
            //     room11.LoadDoors();
            // }

            // foreach (RoomType room12 in Rooms)
            // {
            //     yield return room12.Setup();
            // }
            BardLog.Log("");
        }

        private static bool SetRoomPosition(RoomType room)
        {
            List<Direction> list = new List<Direction>(DirectionExtension.CardinalDirections);
            Direction direction = Direction.None;
            if (room.PreviousRoom != null)
            {
                BardLog.Log("previous room");

                room.Position = room.PreviousRoom.Position;
                direction = room.Direction;

                BardLog.Log("room {0} ({1},{2}), encounter: {3}, door {4}, direction: {5}", room.Name,
                    room.Position.x,
                    room.Position.y,
                    room.Encounter.Name,
                    room.Encounter.Door,
                    direction.ToString());

                if (direction != Direction.None)
                {
                    if (CanMove(room, direction, room.Position))
                    {
                        room.Move(direction);
                    }
                    else
                    {
                        direction = Direction.None;
                    }
                }
                else //direction is none
                {
                    list.Shuffle();
                    foreach (var direction1 in list)
                    {
                        BardLog.Log("direction: {0}", direction1.ToString());
                    }

                    foreach (var item in list.Where(item => CanMove(room, item, room.Position)))
                    {
                        direction = item;
                        room.Move(direction);
                        break;
                    }
                }

                BardLog.Log("");
            }
            else //previous room is null
            {
                List<RoomType> list2 = new List<RoomType>(PositionedRooms);

                if (room.Encounter is { Door: Door.None or Door.Hidden })
                {
                    list2.Shuffle();
                    foreach (var room3 in list2)
                    {
                        BardLog.Log("name: {0}, weight: {1}", room3.Encounter.Name, room3.Weight);
                    }

                    foreach (RoomType item2 in list2)
                    {
                        room.Position = item2.Position;
                        list.Shuffle();

                        BardLog.Log("room name: {0} ({2},{3}), door: {1}, ", room.Encounter.Name,
                            room.Encounter.Door, room.Position.x, room.Position.y);
                        foreach (var item3 in list.Where(item3 => CanMove(room, item3, room.Position)))
                        {
                            direction = item3;
                            room.Move(direction);
                            break;
                        }

                        if (direction != 0)
                            break;
                    }
                }
                else //door is not none or hidden
                {
                    BardLog.Log("door type is not none or hidden");
                    foreach (var room3 in list2)
                    {
                        BardLog.Log("name: {0}, weight: {1}", room3.Encounter.Name, room3.Weight);
                    }

                    BardLog.Log("room name: {0} ({2},{3}), door: {1}, ", room.Encounter.Name,
                        room.Encounter.Door, room.Position.x, room.Position.y);

                    list2.RemoveAll(roomType => roomType.Weight == 0);

                    while (direction == Direction.None && list2.Count > 0)
                    {
                        if (!Rand.GetWeightedElement(list2!, out RoomType result, false))
                            continue;

                        room.Position = result.Position;
                        list.Shuffle();
                        foreach (Direction item4 in list.Where(item4 =>
                                     result.IsValidNeighbor(room, item4) && CanMove(room, item4, room.Position)))
                        {
                            direction = item4;
                            room.Move(direction);
                            break;
                        }

                        list2.Remove(result);
                    }
                }
            }

            BardLog.Log("");
            if (direction == Direction.None) return false;
            if (room.Encounter is { Door: Door.None or Door.Hidden }) return true;

            Direction direction2 = direction.Opposite();
            RoomType? room2 = GetRoom(GetRoomPosition(room.Position, direction2));

            if (room2 == null) return true;

            room2.Neighbors[direction] = room;
            room.Neighbors[direction2] = room2;

            return true;
        }

        public static bool CanMove(RoomType room, Direction direction, Vector2Int position)
        {
            BardLog.Log("({0},{1}), direction: {2}", position.x, position.y, direction.ToString());
            if (room.PreviousRoom != null && !room.PreviousRoom.IsValidNeighbor(room, direction))
            {
                BardLog.Log("not a valid neighbor");
                return false;
            }

            position = GetRoomPosition(position, direction);
            RoomType? room2 = GetRoom(position);
            if (room2 != null)
            {
                BardLog.Log("room already exists");
                return false;
            }

            int num = 0;
            foreach (KeyValuePair<Direction, RoomType> branch in room.Branches)
            {
                if (branch.Key != Direction.None)
                {
                    if (CanMove(branch.Value, branch.Key, position))
                    {
                        num++;
                    }

                    continue;
                }

                if (DirectionExtension.CardinalDirections.Any(kCardinalDirection =>
                        CanMove(branch.Value, kCardinalDirection, position)))
                    num++;
            }

            return num == room.Branches.Count;
        }

        private static RoomType? GetRoom(Vector2Int position)
        {
            var x = Rooms.FirstOrDefault(room => room.Position == position);
            return x;
        }

        public static Vector2Int GetRoomPosition(Vector2Int position, Direction direction)
        {
            return position + direction.DirectionToVector();
        }
    }
}