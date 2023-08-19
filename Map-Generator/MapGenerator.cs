using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Undermine;

namespace Map_Generator
{
    public partial class MapGenerator : Form
    {
        private readonly GridControl _gridControl = new GridControl();
        private readonly RoomInformationBox _roomInfoBox = null!;

        public MapGenerator()
        {
            InitializeComponent();
            WhipSeed.Text = $@"Whip Seed: {Whip.CurrentWhipSeed}";

            _roomInfoBox = new RoomInformationBox(this);

            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @"Save0.json"));

            // roomInfoBox.Dock = DockStyle.None; 
            _roomInfoBox.Width = 250;
            _roomInfoBox.Height = 500;
            _roomInfoBox.Location = new Point(0, 50);
            Controls.Add(_roomInfoBox);

            _gridControl.MouseClick += GridControl_MouseClick;
            _gridControl.Dock = DockStyle.Fill;
            Controls.Add(_gridControl);
            _gridControl.InitializeGridSquares(Program.PositionedRooms);
        }

        private void GridControl_MouseClick(object sender, MouseEventArgs e)
        {
            var square = _gridControl.ClickedSquare(e);
            _roomInfoBox.Room = square?.Room;
            _roomInfoBox.Invalidate();
        }


        private void findMapButton_Click(object sender, System.EventArgs e)
        {
            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @$"Save{saveNumber.Value}.json"));
            _gridControl.InitializeGridSquares(Program.PositionedRooms);
        }
    }
}