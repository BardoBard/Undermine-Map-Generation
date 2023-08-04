using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Parsing.Json.Enums;

namespace Map_Generator
{
    public partial class MapGenerator : Form
    {
        private static GridControl gridControl = new GridControl();
        public static RoomInformationBox roomInfoBox = null!;

        private bool second = false;

        public MapGenerator()
        {
            InitializeComponent();
            
            roomInfoBox = new RoomInformationBox(this);

            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @"Save2.json"));

            // roomInfoBox.Dock = DockStyle.None; 
            roomInfoBox.Width = 250;
            roomInfoBox.Height = 500;
            roomInfoBox.Location = new Point(0, 50 );
            Controls.Add(roomInfoBox);


            gridControl.Dock = DockStyle.Fill;
            Controls.Add(gridControl);
            gridControl.InitializeGridSquares(Program.PositionedRooms);
        }


        private void findMapButton_Click(object sender, System.EventArgs e)
        {
            if (second)
                Application.Restart();


            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @$"Save{saveNumber.Value}.json"));
            gridControl.InitializeGridSquares(Program.PositionedRooms);

            second = true;
        }
    }
}