using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator;

public class GridSquare //maybe make an observer pattern for this
{
    public RoomType Room { get; set; }
    public Vector2Int GridPosition { get; set; }
    public Color Color { get; set; }

    public Vector2Int Center()
    {
        var p = GridPosition;
        var x = p + ((GridControl.CellSize / 2));
        return x;
    }

    public Vector2Int NeighborCenter(Direction direction) =>
        Center() + ((direction.DirectionToVector() * (GridControl.CellSize / 2 + GridControl.GapSize)));

    public Vector2Int DoorPosition(int iconSize)
    {
        var c = Center();
        var x = c + (Room.Direction.DirectionToVector() * (GridControl.CellSize / 2 + iconSize / 2));
        return x;
    }

    public GridSquare(RoomType room, Color color, Vector2Int gridPosition)
    {
        Room = room;
        Color = color;
        GridPosition = gridPosition;
    }

    public string GetEnemyInformation()
    {
        List<string> enemiesInfo = (Room.Encounter!.RoomEnemies ?? new List<Enemy>())
            .Select(enemy => $"Enemy: {enemy.Name}")
            .ToList();
        return string.Concat(
            "\n",
            string.Join("\n", enemiesInfo));
    }
}