using System.IO;
using System.Windows.Forms;
using Map_Generator.Parsing;

namespace Map_Generator
{
    public partial class MapGenerator : Form
    {
        private GridControl gridControl = new GridControl();
        private bool second = false;

        public MapGenerator()
        {
            InitializeComponent();

            gridControl.Dock = DockStyle.Fill;
            Controls.Add(gridControl);
            Program.Start(Path.Combine(PathHandler.UnderminePath, @"Saves\Save2.json"));
            gridControl.InitializeGridSquares(Program.PositionedRooms);
        }

        private void findMapButton_Click(object sender, System.EventArgs e)
        {
            if (second)
                Application.Restart();

            
            Program.Start(Path.Combine(PathHandler.UnderminePath, @$"Saves\Save{this.saveNumber.Value}.json"));
            gridControl.InitializeGridSquares(Program.PositionedRooms);

            second = true;
        }
    }
}