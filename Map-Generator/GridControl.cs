using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Math;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public class GridControl : Control
    {
        private const int IconSize = 30;
        private Grid _grid = new Grid(0, 0, new List<GridSquare>());

        private ToolTip Tooltip = new ToolTip();

        public void InitializeGridSquares(List<RoomType> roomTypes)
        {
            _grid = new Grid(this.Width, this.Height,
                roomTypes.Select(room => new GridSquare(room, MapIconExtension.AssignColor(room))).ToList(),
                new(1, -1));

            Invalidate();
        }

        public GridControl()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_grid.GridSquares.Count == 0)
                return;

            Graphics g = e.Graphics;
            g.TranslateTransform(0, this.Height);
            g.ScaleTransform(_grid.GridTranslation.x, _grid.GridTranslation.y);

            foreach (GridSquare? square in _grid.GridSquares)
            {
                var position = square.GetPositionOnGrid(_grid);

                DrawRoom(g, square, position.x, position.y);
            }
        }

        private void DrawRoom(Graphics g, GridSquare square, int x, int y)
        {
            Brush brush = new SolidBrush(square.Color);
            g.FillRectangle(brush, x, y, Grid.CellSize, Grid.CellSize);

            // draw connections between neighbors
            foreach (var neighborDirection in square.Room.Neighbors.Keys)
            {
                Vector2Int center = square.Center(_grid);
                Vector2Int neighborCenter = square.NeighborCenter(_grid, neighborDirection);

                int neighborCenterX = neighborCenter.x;
                int neighborCenterY = neighborCenter.y;

                g.DrawLine(Pens.Black, center.x, center.y, neighborCenterX, neighborCenterY);


                Image? doorImage = DoorExtension.GetDoorImage(square.Room.Encounter.Door);
                if (doorImage != null)
                {
                    doorImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                    int iconX = x + neighborDirection.DirectionToVector().x * (Grid.CellSize / 2) +
                                (Grid.CellSize - IconSize) / 2;
                    int iconY = y + neighborDirection.DirectionToVector().y * (Grid.CellSize / 2) +
                                (Grid.CellSize - IconSize) / 2;
                    // Vector2Int neighborDoorPosition = square.DoorPosition(_grid, IconSize);
                    g.DrawImage(
                        doorImage,
                        iconX,
                        iconY,
                        IconSize, IconSize
                    );
                }
            }

            Image? mapImage = MapIconExtension.GetMapImage(square.Room.MapIcon);
            if (mapImage != null)
            {
                int iconX = x + (Grid.CellSize - IconSize) / 2;
                int iconY = y + (Grid.CellSize - IconSize) / 2;

                mapImage.RotateFlip(RotateFlipType.Rotate180FlipX);

                g.DrawImage(mapImage, iconX, iconY, IconSize, IconSize);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _grid.Width = this.Width;
            _grid.Height = this.Height;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Find the grid square where the mouse cursor is
            GridSquare? clickedSquare = _grid.GridSquares.FirstOrDefault(square =>
            {
                var position = square.GetPositionOnGrid(_grid);
                return e.X >= position.x && e.X <= position.x + Grid.CellSize &&
                       e.Y <= position.y && e.Y >= position.y - Grid.CellSize;
            });

            if (clickedSquare is null) return;

            string enemyInformation = clickedSquare.GetEnemyInformation();

            if (string.IsNullOrEmpty(enemyInformation)) return;

            Tooltip.ToolTipTitle = $"{clickedSquare.Room.RoomTypeTag}_{clickedSquare.Room.Encounter.Name}";

            Tooltip.Show(enemyInformation, this, 6000);
        }
    }
}