using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public class GridSquare
    {
        public RoomType Room { get; set; }
        public int x => Room.Position.x;
        public int y => Room.Position.y;
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public GridSquare(RoomType room, int width, int height, Color color)
        {
            Room = room;
            Width = width;
            Height = height;
            Color = color;
        }

        public string GetEnemyInformation()
        {
            List<string> enemiesInfo = (Room.Encounter!.Enemies ?? new List<Enemy>())
                .Select(enemy => $"Enemy: {enemy.Name}")
                .ToList();
            return string.Concat(
                $"{Room.RoomTypeTag}_{Room.Encounter.Name}",
                "\n\n\n",
                string.Join("\n", enemiesInfo));
        }
    }

    public class GridControl : Control
    {
        private const int CellSize = 40;
        private const int GapSize = 10;

        private Vector2Int Origin => GridSquares.Count > 0
            ? new(
                (GridSquares[0].x - GridSquares.Min(s => s.x)) * (CellSize + GapSize),
                (GridSquares[0].y - GridSquares.Min(s => s.y)) * (CellSize + GapSize))
            : new();

        private ToolTip Tooltip = new ToolTip();

        private List<GridSquare> GridSquares { get; set; } = new List<GridSquare>();

        public void InitializeGridSquares(List<RoomType> roomTypes)
        {
            GridSquares = new List<GridSquare>();

            foreach (var room in roomTypes)
                GridSquares.Add(new GridSquare(room, CellSize, CellSize, AssignColor(room)));

            Invalidate();
        }

        private Color AssignColor(RoomType room)
        {
            Color color = Color.Gray;

            if (room.Position == Vector2Int.Zero)
                color = Color.GreenYellow;
            else if (room.Tag == "end")
                color = Color.Red;
            else if (room.Encounter.Door == Door.Secret)
                color = Color.Yellow;
            else if (room.Encounter.Door == Door.Hidden)
                color = Color.Black;
            else if (room.RoomTypeTag is "relic" or "relic_unlocked")
                color = Color.Blue;

            return color;
        }

        public GridControl()
        {
            DoubleBuffered = true;
            Tooltip.ToolTipTitle = "Room Information";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (GridSquares == null || GridSquares.Count == 0)
                return;

            Graphics g = e.Graphics;
            g.TranslateTransform(0, this.Height);
            g.ScaleTransform(1, -1);

            // Calculate the number of rows and columns
            int numRows = GridSquares.Max(square => square.y) - GridSquares.Min(square => square.y) + 1;
            int numCols = GridSquares.Max(square => square.x) - GridSquares.Min(square => square.x) + 1;

            // Calculate the total size of the grid
            int totalWidth = numCols * CellSize + (numCols - 1) * GapSize;
            int totalHeight = numRows * CellSize + (numRows - 1) * GapSize;

            // Calculate the offset to center the grid in the control
            int offsetX = (this.Width - totalWidth) / 2;
            int offsetY = (this.Height - totalHeight) / 2;

            foreach (GridSquare? square in GridSquares)
            {
                int x = offsetX + (square.x - GridSquares.Min(s => s.x)) * (CellSize + GapSize);
                int y = offsetY + (square.y - GridSquares.Min(s => s.y)) * (CellSize + GapSize);

                DrawRoom(g, square, x, y);
            }
        }

        private void DrawRoom(Graphics g, GridSquare square, int x, int y)
        {
            Brush brush = new SolidBrush(square.Color);
            g.FillRectangle(brush, x, y, square.Width, square.Height);

            // Draw exits/doors between neighboring rooms
            foreach (var neighborDirection in square.Room.Neighbors.Keys)
            {
                Vector2Int center = new(x + square.Width / 2, y + square.Height / 2);

                Vector2Int neighborCenter =
                    center + Program.DirectionToVector(neighborDirection) * (square.Width + GapSize);

                // Draw a line from the current room to the neighbor with the direction
                g.DrawLine(Pens.Black, center.x, center.y, neighborCenter.x, neighborCenter.y);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int gridX = (this.Width - e.Location.X - GapSize - Origin.x) / (CellSize + GapSize);
            int gridY = (this.Height - e.Location.Y - GapSize - Origin.y) / (CellSize + GapSize);

            // Find the grid square where the mouse cursor is
            GridSquare? clickedSquare = GridSquares.FirstOrDefault(square =>
                square.x == Origin.x + gridX && square.y == Origin.y + gridY);

            if (clickedSquare is null) return;

            string enemyInformation = clickedSquare.GetEnemyInformation();

            if (string.IsNullOrEmpty(enemyInformation)) return;

            Tooltip.Show(enemyInformation, this, 6000);
        }
    }
}