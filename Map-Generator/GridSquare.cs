using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator;

public class GridSquare
{
    public RoomType Room { get; set; }
    public int x => Room.Position.x;
    public int y => Room.Position.y;
    public Color Color { get; set; }

    public Vector2Int GetPositionOnGrid(Grid grid, Vector2Int? originalPosition = null)
    {
        originalPosition ??= Room.Position;
        return new Vector2Int(
            grid.Width / 2 + (originalPosition.x * (Grid.CellSize + Grid.GapSize)),
            grid.Height / 2 + (originalPosition.y * (Grid.CellSize + Grid.GapSize))
        );
    }


    public Vector2Int Center(Grid grid)
    {
        var p = GetPositionOnGrid(grid, this.Room.Position);
        var x = p + ((Grid.CellSize / 2));
        return x;
    }

    public Vector2Int NeighborCenter(Grid grid, Direction direction) =>
        Center(grid) + ((direction.DirectionToVector() * (Grid.CellSize / 2 + Grid.GapSize)));

    public Vector2Int DoorPosition(Grid grid, int iconSize)
    {
        var c = Center(grid);
        var x = c + (Room.Direction.DirectionToVector() * (Grid.CellSize / 2 + iconSize / 2));
        return x;
    }

    public GridSquare(RoomType room, Color color)
    {
        Room = room;
        Color = color;
    }

    public string GetEnemyInformation()
    {
        List<string> enemiesInfo = (Room.Encounter!.Enemies ?? new List<Enemy>())
            .Select(enemy => $"Enemy: {enemy.Name}")
            .ToList();
        return string.Concat(
            "\n",
            string.Join("\n", enemiesInfo));
    }
}