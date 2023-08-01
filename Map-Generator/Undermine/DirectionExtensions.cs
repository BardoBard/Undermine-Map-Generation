using System.Collections.Generic;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator.Undermine;

public static class DirectionExtensions
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
}