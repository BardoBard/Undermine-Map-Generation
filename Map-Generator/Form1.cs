using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Map_Generator.Parsing.Json.Classes;

namespace Map_Generator
{
    public partial class Form1 : Form
    {
        private GridControl gridControl;

        public Form1()
        {
            InitializeComponent();
            InitializeData();

            // Create and add the GridControl to the Form
            gridControl = new GridControl();
            gridControl.Dock = DockStyle.Fill;
            Controls.Add(gridControl);

            // Set the PositionedRooms data to the GridControl
            gridControl.InitializeGridSquares(Program.PositionedRooms);
        }

        private void InitializeData()
        {
            Program.Start();
        }
    }
}