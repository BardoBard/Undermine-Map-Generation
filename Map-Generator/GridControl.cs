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
    public sealed class GridControl : Control
    {
        public List<GridSquare> GridSquares { get; private set; } = new();
        public static int CellSize = 40;
        public static int GapSize = 10;
        public int IconSize = 30;
        private const int MaxImagesPerRow = 2;
        private readonly Vector2Int _translation = new(1, -1);

        public Vector2Int GridOffset { get; set; } = Vector2Int.Zero;

        public void InitializeGridSquares(List<Room> roomTypes)
        {
            GridSquares = roomTypes.Select(room =>
                new GridSquare(
                    room,
                    MapIconExtension.AssignColor(room),
                    GetPositionOnGrid(roomTypes.Select(roomType => roomType.Position).ToList(), room.Position)
                )).ToList();

            Invalidate();
        }

        public void Path(List<Room> rooms)
        {
            if (!rooms.Any())
                return;

            foreach (var gridSquare in GridSquares)
            {
                if (!rooms.Contains(gridSquare.Room))
                {
                    gridSquare.Color = MapIconExtension.AssignColor(gridSquare.Room);
                    continue;
                }

                gridSquare.Color = Color.DarkSlateGray;
                
            }

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
            g.ScaleTransform(_translation.x, _translation.y);

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

            List<Image> mapImages = MapIconExtension.GetMapImage(square.Room);
            DrawImages(mapImages, square.Center(), g, MaxImagesPerRow);
        }

        private void DrawImages(List<Image> images, Vector2Int center, Graphics g, int maxImagesPerRow)
        {
            int totalImages = images.Count;
            maxImagesPerRow = totalImages > 1 ? maxImagesPerRow : 1; // Set the maximum number of images per row
            int numRows = (totalImages + maxImagesPerRow - 1) / maxImagesPerRow; // Calculate the number of rows

            int iconSize2 = totalImages > 1 ? CellSize / maxImagesPerRow : IconSize;

            int startX = center.x - (maxImagesPerRow * iconSize2) / 2;
            int startY = center.y - (numRows * iconSize2) / 2;

            int currentRow = 0, currentCol = 0;

            for (int i = 0; i < totalImages; i++)
            {
                var mapImage = images[i];
                var iconPosition = new Point(startX + currentCol * iconSize2, startY + currentRow * iconSize2);

                mapImage.RotateFlip(RotateFlipType.Rotate180FlipX);

                g.DrawImage(mapImage, iconPosition.X, iconPosition.Y, iconSize2, iconSize2);

                if (++currentCol >= maxImagesPerRow)
                {
                    currentCol = 0;
                    currentRow++;
                }
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

            CellSize = (int)System.Math.Floor((Width / 2.0f) / 10.0f);
            GapSize = (int)System.Math.Floor(CellSize / 4.0f);
            IconSize = (int)System.Math.Floor(CellSize / 1.5f);
            Invalidate();
        }

        public GridSquare? ClickedSquare(MouseEventArgs e)
        {
            int posY = (int)System.Math.Floor(((this.Height / 2.0f - e.Y + GridOffset.y) / (CellSize + GapSize)));

            GridSquare? clickedSquare = GridSquares.FirstOrDefault(square =>
            {
                var position = square.GridPosition;
                return e.X >= position.x &&
                       e.X <= position.x + CellSize &&
                       posY == square.Room.Position.y;
            });

            return clickedSquare;
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
                (xMiddle + 1) * (CellSize + GapSize),
                (yMiddle) * (CellSize + GapSize)
            );

            return new Vector2Int(
                this.Width / 2 + (originalPosition.x * (CellSize + GapSize)) - GridOffset.x,
                this.Height / 2 + (originalPosition.y * (CellSize + GapSize)) - GridOffset.y
            );
        }
    }
}