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
    public sealed class GridControl : Control
    {
        public List<GridSquare> GridSquares { get; private set; } = new List<GridSquare>();
        public const int CellSize = 40;
        public const int GapSize = 10;
        private const int IconSize = 30;
        private readonly Vector2Int Translation = new(1, -1);

        public Vector2Int GridOffset { get; set; } = Vector2Int.Zero;

        private ToolTip Tooltip = new ToolTip();

        public void InitializeGridSquares(List<RoomType> roomTypes)
        {
            GridSquares = roomTypes.Select(room =>
                new GridSquare(
                    room,
                    MapIconExtension.AssignColor(room),
                    GetPositionOnGrid(roomTypes.Select(roomType => roomType.Position).ToList(), room.Position)
                )).ToList();

            Invalidate();
        }

        public GridControl()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (GridSquares.Count == 0)
                return;

            Graphics g = e.Graphics;
            g.TranslateTransform(0, this.Height);
            g.ScaleTransform(Translation.x, Translation.y);

            foreach (GridSquare? square in GridSquares)
            {
                var position = square.GridPosition;

                DrawRoom(g, square, position.x, position.y);
            }
        }

        private void DrawRoom(Graphics g, GridSquare square, int x, int y)
        {
            Brush brush = new SolidBrush(square.Color);
            g.FillRectangle(brush, x, y, CellSize, CellSize);

            // draw connections between neighbors
            foreach (var neighborDirection in square.Room.Neighbors.Keys)
            {
                Vector2Int center = square.Center();
                Vector2Int neighborCenter = square.NeighborCenter(neighborDirection);

                int neighborCenterX = neighborCenter.x;
                int neighborCenterY = neighborCenter.y;

                g.DrawLine(Pens.Black, center.x, center.y, neighborCenterX, neighborCenterY);


                using (Image? doorImage =
                       square.Room.Encounter?.Door.GetDoorImage(square.Room.Neighbors[neighborDirection]))
                {
                    if (doorImage != null)
                    {
                        doorImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                        int iconX = x + neighborDirection.DirectionToVector().x * (CellSize / 2) +
                                    (CellSize - IconSize) / 2;
                        int iconY = y + neighborDirection.DirectionToVector().y * (CellSize / 2) +
                                    (CellSize - IconSize) / 2;

                        g.DrawImage(
                            doorImage,
                            iconX,
                            iconY,
                            IconSize, IconSize
                        );
                    }
                }
            }

            Image? mapImage = MapIconExtension.GetMapImage(square.Room);
            if (mapImage != null)
            {
                var iconPosition = square.Center() - IconSize / 2;

                mapImage.RotateFlip(RotateFlipType.Rotate180FlipX);

                g.DrawImage(mapImage, iconPosition.x, iconPosition.y, IconSize, IconSize);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = this.Width;
            Height = this.Height;

            foreach (var gridSquare in GridSquares)
            {
                gridSquare.GridPosition = GetPositionOnGrid(
                    GridSquares.Select(square => square.Room.Position).ToList(),
                    gridSquare.Room.Position
                );
            }

            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int posY = (int)System.Math.Floor(((this.Height / 2.0f - e.Y + GridOffset.y) / (CellSize + GapSize)));

            GridSquare? clickedSquare = GridSquares.FirstOrDefault(square =>
            {
                var position = square.GridPosition;
                return e.X >= position.x &&
                       e.X <= position.x + CellSize &&
                       posY == square.Room.Position.y;
            });

            if (clickedSquare is null) return;

            MapGenerator.roomInfoBox.Room = clickedSquare.Room;
            MapGenerator.roomInfoBox.Invalidate();
        }

        public Vector2Int GetPositionOnGrid(Vector2Int originalPosition)
        {
            return new Vector2Int(
                this.Width / 2 + (originalPosition.x * (GridControl.CellSize + GridControl.GapSize)),
                this.Height / 2 + (originalPosition.y * (GridControl.CellSize + GridControl.GapSize))
            );
        }

        public Vector2Int GetPositionOnGrid(List<Vector2Int> allPositions, Vector2Int originalPosition)
        {
            if (allPositions.Count == 0)
                return new Vector2Int();

            // calculate min and max of grid, take the middle of that
            int xMin = allPositions.Min(position => position.x);
            int yMin = allPositions.Min(position => position.y);

            int xMax = allPositions.Max(position => position.x);
            int yMax = allPositions.Max(position => position.y);
            
            int xMiddle = (xMin + xMax) / 2;
            int yMiddle = (yMin + yMax) / 2;

            GridOffset = new Vector2Int(
                xMiddle * (GridControl.CellSize + GridControl.GapSize),
                yMiddle * (GridControl.CellSize + GridControl.GapSize)
            );

            return new Vector2Int(
                this.Width / 2 + (originalPosition.x * (GridControl.CellSize + GridControl.GapSize)) - GridOffset.x,
                this.Height / 2 + (originalPosition.y * (GridControl.CellSize + GridControl.GapSize)) - GridOffset.y
            );
        }
    }
}