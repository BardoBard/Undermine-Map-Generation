﻿using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public sealed class RoomInformationBox : Panel
    {
        private readonly Form _form;
        public Room? Room { get; set; } = null;
        private int _textOffset = 40;
        private int _rowSize = 30;
        private const int GapSize = 5;

        public RoomInformationBox(Form form)
        {
            this._form = form;
            DoubleBuffered = true;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Vector2Int position = new Vector2Int(10, 0);
            if (Room == null)
            {
                Draw(g, null, $"Click on a room!", position);
                return;
            }

            // draw roomname
            Draw(g, MapIconExtension.GetMapImage(Room).FirstOrDefault(), $"{Room.Name}_{Room.Encounter?.Name}",
                position);

            //draw door cost
            if (Room.Encounter is { Door: not Door.None and not Door.Normal })
                Draw(g, Room.Encounter.Door.GetDoorImage(), $"Door: {Room.Encounter.Door.ToString()}",
                    position);

            //spacing between name and enemies
            position.y += _rowSize + GapSize;

            //draw room enemies
            if (Room.Encounter?.RoomEnemies != null)
                foreach (Enemy? enemy in Room.Encounter.RoomEnemies)
                    Draw(g, enemy.EnemyIcon.GetEnemyImage(), enemy.Name, position);

            position.x += 100;
            position.y = _rowSize + (GapSize * 2);

            foreach (Item? item in Room.Extras)
                Draw(g, item.ItemIcon.GetItemImage(), item.Name, position);
        }

        private void Draw(Graphics g, Image? image, string text, Vector2Int position)
        {
            if (image != null && position.y < this._form.Height)
            {
                float aspectRatio = (float)image.Width / image.Height;
                g.DrawImage(image, new Rectangle(position.x, position.y, (int)(aspectRatio * _rowSize), _rowSize));
            }

            g.DrawString(text, this.Font, Brushes.Black, new Point(position.x + _textOffset, position.y));
            position.y += _rowSize + GapSize;
        }
    }
}