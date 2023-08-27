using System;
using System.Windows.Forms;
using Map_Generator.UserControls;
using Map_Generator.UserControls.CreateTests;

namespace Map_Generator
{
    public partial class MapGeneratorForm : Form
    {
        public readonly MapGeneratorControl MapGeneratorControl = new MapGeneratorControl();
        public MapGeneratorForm()
        {
            InitializeComponent();
            AddNewTab(MapGeneratorControl, "Map Generator");

#if TESTING
            AddNewTab(new CreateTests(), "Create Tests");
#endif
        }

        private void AddNewTab(UserControl userControl, string name)
        {
            var tabPage = new TabPage(name);
            tabPage.Dock = DockStyle.Fill;
            tabPage.Controls.Add(userControl);
            tabControl1.TabPages.Add(tabPage);

            ResizeTab(tabPage, userControl);
        }

        private void MapGeneratorForm_Resize(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
                if (tabPage.Controls[0] is UserControl control)
                    ResizeActiveTab(tabPage, control);
        }

        private void MapGeneratorTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResizeActiveTab(tabControl1.SelectedTab, tabControl1.SelectedTab.Controls[0] as UserControl);
        }

        private void ResizeActiveTab(TabPage tabPage, UserControl? control)
        {
            if (control != null && tabPage.Controls[0] is UserControl)
                ResizeTab(tabPage, control);
        }

        private void ResizeTab(TabPage tabPage, UserControl control)
        {
            control.Size = tabPage.ClientSize;
        }
    }
}