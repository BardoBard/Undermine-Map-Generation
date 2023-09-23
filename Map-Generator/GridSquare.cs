using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public class GridSquare //maybe make an observer pattern for this
    {
        public RoomType Room { get; set; }
        public Vector2Int GridPosition { get; set; }
        public Color Color { get; set; }

        public Vector2Int Center() => GridPosition + GridControl.CellSize / 2;

        public Vector2Int NeighborCenter(Direction direction) =>
            Center() + direction.DirectionToVector() * (GridControl.CellSize / 2 + GridControl.GapSize);

        public Vector2Int DoorPosition(int iconSize) => Center() + Room.Direction.DirectionToVector() * (GridControl.CellSize / 2 + iconSize / 2);

        public GridSquare(RoomType room, Color color, Vector2Int gridPosition)
        {
            Room = room;
            Color = color;
            GridPosition = gridPosition;
        }
    }
}