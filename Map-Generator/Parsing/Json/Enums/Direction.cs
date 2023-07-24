namespace Map_Generator.Parsing.Json.Enums
{
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
}