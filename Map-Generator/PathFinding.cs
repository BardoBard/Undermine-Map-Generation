using System.Collections.Generic;
using System.Linq;
using Map_Generator.Math;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public static class PathFinding
    {
        public static List<Room> BreadthFirstSearch(this List<Room> rooms)
        {
            var result = new List<Room>(10);

            Room? start = rooms.First(room => room.Position == Vector2Int.Zero);
            Room? end = rooms.First(room => room.Name is "end" or "nextdown");

            //pathfinding algorithm
            var queue = new Queue<Room>();
            queue.Enqueue(start);

            var cameFrom = new Dictionary<Room, Room>
            {
                [start] = null
            };

            while (queue.Count > 0)
            {
                Room current = queue.Dequeue();

                if (current == end)
                    break;

                foreach (var neighbor in current.Neighbors.Where(neighbor =>
                             neighbor.Value != null && !cameFrom.ContainsKey(neighbor.Value)))
                {
                    cameFrom[neighbor.Value] = current;
                    queue.Enqueue(neighbor.Value);
                }
            }

            //if path is found, add the room to the result list
            if (cameFrom.ContainsKey(end))
            {
                Room? current = end;
                while (current != null)
                {
                    result.Add(current);
                    current = cameFrom[current];
                }

                result.Reverse();
            }


            return result;
        }

        /// <summary>
        /// a star algorithm that takes into account the distance between rooms other heuristics
        /// </summary>
        /// <param name="rooms">rooms</param>
        /// <returns>path</returns>
        public static List<Room> AStarSearch(this List<Room> rooms)
        {
            var result = new List<Room>(10);

            Room? start = rooms.First(room => room.Position == Vector2Int.Zero);
            Room? end = rooms.First(room =>
                room.RoomType is RoomType.End or RoomType.NextDown ||
                (Save.FloorNumber == 4 && MapType.GetMap() == MapType.MapName.core));

            //pathfinding algorithm
            var queue = new List<Room>
            {
                start
            };

            var cameFrom = new Dictionary<Room, Room>
            {
                [start] = null
            };

            var gScore = new Dictionary<Room, int>
            {
                [start] = 0
            };

            var fScore = new Dictionary<Room, int>
            {
                [start] = HeuristicCostEstimate(start, end)
            };

            while (queue.Count > 0)
            {
                Room current = queue.OrderBy(room => fScore[room]).First();

                if (current == end)
                    break;

                queue.Remove(current);

                foreach (var neighbor in current.Neighbors.Where(neighbor =>
                             neighbor.Value != null && !cameFrom.ContainsKey(neighbor.Value)))
                {
                    int cost = gScore[current] + HeuristicCostEstimate(current, neighbor.Value);
                    if (!gScore.ContainsKey(neighbor.Value) || cost < gScore[neighbor.Value])
                    {
                        cameFrom[neighbor.Value] = current;
                        gScore[neighbor.Value] = cost;
                        fScore[neighbor.Value] = gScore[neighbor.Value] + HeuristicCostEstimate(neighbor.Value, end);
                        if (!queue.Contains(neighbor.Value))
                            queue.Add(neighbor.Value);
                    }
                }
            }

            //if path is found, add the room to the result list
            if (cameFrom.ContainsKey(end))
            {
                Room? current = end;
                while (current != null)
                {
                    result.Add(current);
                    current = cameFrom[current];
                }

                result.Reverse();
            }

            return result;
        }

        private static int HeuristicCostEstimate(Room start, Room end)
        {
            int weight = start.Position.DistanceTo(end.Position); //distance between the rooms
            weight -= end.RoomType == RoomType.Treasure ? 5 : 0;
            weight += end.RoomType == RoomType.Secret ? 1 : 0;

            if (end.Encounter != null)
            {
                weight += end.Encounter.RoomEnemies.Sum(enemy => enemy.Difficulty); //weight of the enemies in the room
                weight += end.Encounter
                    .DifficultyWeight; //weight of the room itself, for example a maze room is more difficult than a normal room
            }

            return weight;
        }
    }
}