using System;
using System.Windows.Forms;

namespace Map_Generator.UserControls.CreateTests
{
    public partial class CreateTests : UserControl
    {
        public CreateTests()
        {
            InitializeComponent();
        }


        private void CreateTestButton_Click(object sender, EventArgs e)
        {
            SaveDecoder.ReadSaveData((int)SaveNumberNumeric.Value);
            RoomsDecoder.LoadRooms((int)SaveNumberNumeric.Value);
            
            SaveDecoder.WriteSaveData();
            RoomsDecoder.WriteRooms();
            Program.Form.MapGeneratorControl.GridControl.InitializeGridSquares(Program.PositionedRooms);
        }
    }
}