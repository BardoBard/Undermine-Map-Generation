using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;
using Map_Generator.Math;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public sealed class RoomInformationBox : Control
    {
        private readonly Form _form;
        public RoomType? Room { get; set; } = new RoomType();
        private int _textOffset = 20;
        private int _rowSize = 15;
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

            if (Room == null) return;

            Graphics g = e.Graphics;


            Vector2Int position = new Vector2Int(10, 0);

            Draw(g, MapIconExtension.GetMapImageExtraInformation(Room), $"{Room.Name}_{Room.Encounter?.Name}",
                position);

            position.y += _rowSize + GapSize;

            if (Room.Encounter?.RoomEnemies != null)
                foreach (Enemy? enemy in Room.Encounter.RoomEnemies)
                    Draw(g, enemy.EnemyIcon.GetEnemyImage(), enemy.Name, position);
        }

        private void Draw(Graphics g, Image? image, string text, Vector2Int position)
        {
            if (image != null && position.y < this._form.Height)
                g.DrawImage(image, new Rectangle(position.x, position.y, _rowSize, _rowSize));


            g.DrawString(text, this.Font, Brushes.Black, new Point(position.x + _textOffset, position.y));
            position.y += _rowSize + GapSize;
        }
    }
}