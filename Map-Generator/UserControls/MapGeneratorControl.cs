using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Map_Generator.Parsing;
using Map_Generator.Undermine;

namespace Map_Generator.UserControls
{
    public partial class MapGeneratorControl : UserControl
    {
        public readonly GridControl GridControl = new GridControl();
        private readonly RoomInformationBox _roomInfoBox = null!;
        private bool _secondClick = false;

        public MapGeneratorControl()
        {
            InitializeComponent();
            WhipSeed.Text = $@"Whip Seed: {Whip.CurrentWhipSeed}";

            _roomInfoBox = new RoomInformationBox();

            // Program.Start(Path.Combine(PathHandler.UndermineSavePath, @"Save0.json"));

            // roomInfoBox.Dock = DockStyle.None; 
            _roomInfoBox.Width = 250;
            _roomInfoBox.Height = 500;
            _roomInfoBox.Location = new Point(0, 50);
            Controls.Add(_roomInfoBox);

            GridControl.MouseClick += GridControl_MouseClick;
            GridControl.Dock = DockStyle.Fill;
            Controls.Add(GridControl);
            GridControl.InitializeGridSquares(Program.PositionedRooms);
        }

        private void GridControl_MouseClick(object sender, MouseEventArgs e)
        {
            var square = GridControl.ClickedSquare(e);
            _roomInfoBox.Room = square?.Room;
            _roomInfoBox.Invalidate();
        }


        private void findMapButton_Click(object sender, System.EventArgs e)
        {
            if (_secondClick)
                Application.Restart();
            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @$"Save{saveNumber.Value}.json"));
            GridControl.InitializeGridSquares(Program.PositionedRooms);
            _secondClick = true;
        }

        private void IssueButton_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/BardoBard/Undermine-Map-Generation/issues");
        }
    }
}