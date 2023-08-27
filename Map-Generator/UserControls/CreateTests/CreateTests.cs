using System;
using System.IO;
using System.Windows.Forms;
using Map_Generator.Parsing;

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
            Program.Start(Path.Combine(PathHandler.UndermineSavePath, @$"Save{(int)SaveNumberNumeric.Value}.json"));
            Program.Form.MapGeneratorControl.GridControl.InitializeGridSquares(Program.PositionedRooms);
        }
    }
}