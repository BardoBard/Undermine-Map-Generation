using System;
using System.Collections.Generic;
using Map_Generator.Math;

namespace Map_Generator.Parsing.Json.Enums
{
    [Flags]
    public enum Direction
    {
        None = 0,
        North = 1,
        South = 2,
        East = 4,
        West = 8,
        Up = 16,
        Down = 32,
        NE = 3,
        NW = 9,
        SE = 6,
        SW = 12,
        NS = 5,
        EW = 10,
        WNE = 13,
        NES = 7,
        ESW = 14,
        SWN = 11,
        Cardinal = 15,
        Vertical = 48,
        All = 63
    }

    public static class DirectionExtension
    {
        public static readonly List<Direction> CardinalDirections = new List<Direction>
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West
        };

        public static Direction Opposite(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.South,
                Direction.South => Direction.North,
                Direction.East => Direction.West,
                Direction.West => Direction.East,
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                _ => Direction.All,
            };
        }

        public static Direction Rotate90CCW(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => Direction.All,
            };
        }

        public static Direction Rotate90CW(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => Direction.All,
            };
        }
        public static Vector2Int DirectionToVector(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Vector2Int.Up,
                Direction.South => Vector2Int.Down,
                Direction.East => Vector2Int.Right,
                Direction.West => Vector2Int.Left,
                _ => Vector2Int.Zero,
            };
        }
    }
}