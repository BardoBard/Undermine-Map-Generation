using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Classes;
using Map_Generator.Undermine;

namespace Map_Generator
{
    public partial class MapGenerator : Form
    {
        private FileSystemWatcher watcher = new FileSystemWatcher();
        private readonly GridControl _gridControl = new();
        private readonly RoomInformationBox _roomInfoBox = null!;

        public MapGenerator()
        {
            InitializeComponent();
            WhipSeed.Text = $@"Whip Seed: {Whip.CurrentWhipSeed}";

            _roomInfoBox = new RoomInformationBox(this);

            // Program.Start(Path.Combine(PathHandler.UndermineSavePath, @"Save0.json"));

            // roomInfoBox.Dock = DockStyle.None; 
            _roomInfoBox.Width = 250;
            _roomInfoBox.Height = 500;
            _roomInfoBox.Location = new Point(0, 50);
            Controls.Add(_roomInfoBox);

            _gridControl.MouseClick += GridControl_MouseClick;
            _gridControl.Dock = DockStyle.Fill;
            Controls.Add(_gridControl);
            _gridControl.InitializeGridSquares(Program.PositionedRooms);

            watcher.Path = PathHandler.UndermineSaveDir;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = @$"Save{SaveNumber.Value}.json";
            watcher.Changed += (sender, args) => { ShowMap(); };
            watcher.EnableRaisingEvents = true;
            Program.Initialize(Path.Combine(PathHandler.UndermineSaveDir, @$"Save{SaveNumber.Value}.json"));
        }

        private void GridControl_MouseClick(object sender, MouseEventArgs e)
        {
            var square = _gridControl.ClickedSquare(e);
            _roomInfoBox.Room = square?.Room;
            _roomInfoBox.Invalidate();
        }

        private void SaveNumber_ValueChanged(object sender, System.EventArgs e)
        {
            watcher.Filter = @$"Save{SaveNumber.Value}.json";
        }

        private void FindFastMapButton_Click(object sender, System.EventArgs e)
        {
            Program.Initialize(Path.Combine(PathHandler.UndermineSaveDir, @$"Save{SaveNumber.Value}.json"));
            var count = int.MaxValue;
            var result = Program.PositionedRooms;
            var originalSeed = Save.Seed;
            var newSeed = Save.Seed;
            var max = Save.Seed + 2000;
            for (; Save.Seed < max; ++Save.Seed)
            {
                Program.Start();
                var count2 = Program.PositionedRooms.AStarSearch(Heuristic()).Count;
                if (count2 > count) continue;

                count = count2;
                result = new List<Room>(Program.PositionedRooms);
                newSeed = Save.Seed;
                if (count == 2) break;
            }

            Save.Seed = newSeed;
            Program.PositionedRooms = result;
            Program.Start();
            _gridControl.InitializeGridSquares(Program.PositionedRooms);
            _gridControl.Path(Program.PositionedRooms.AStarSearch(Heuristic()));
            this.FloorNameLabel.Text = @$"{MapType.GetMapName()}-{Save.FloorNumber} Seed: {Save.Seed}";
            Save.Seed = originalSeed;
        }

        private void FindMapButton_Click(object sender, System.EventArgs e)
        {
            ShowMap();
        }

        private void ShowMap()
        {
            Program.Initialize(Path.Combine(PathHandler.UndermineSaveDir, @$"Save{SaveNumber.Value}.json"));
            Program.Start();
            _gridControl.InitializeGridSquares(Program.PositionedRooms);
            _gridControl.Path(Program.PositionedRooms.AStarSearch(Heuristic()));
        }

        private PathFinding.Heuristic Heuristic()
        {
            if (SimpleAStarRadio.Checked)
                return PathFinding.SimpleHeuristics;
            if (AdvancedAStarRadio.Checked)
                return PathFinding.AdvancedHeuristics;

            return PathFinding.SimpleHeuristics;
        }

        private void AStarRadio_Click(object sender, EventArgs e)
        {
            if (!Program.PositionedRooms.Any()) return;
            _gridControl.Path(Program.PositionedRooms.AStarSearch(Heuristic()));
        }

        private void IssueButton_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/BardoBard/Undermine-Map-Generation/issues");
        }

        private void CreateTestButton_Click(object sender, System.EventArgs e)
        {
            Test();

            var zoneDataName = Program.Zonedata.Name.Substring(0, Program.Zonedata.Name.Length - 1);
            int zoneDataNumber =
                int.Parse(Program.Zonedata.Name.Substring(Program.Zonedata.Name.Length - 1, 1));
            var floorNumber = Save.FloorNumber;
            var dir = Path.Combine(PathHandler.GlobalTestsDir, zoneDataName, zoneDataNumber.ToString());

            var fullName = $"{zoneDataName}{zoneDataNumber}-{floorNumber}-{Save.Seed}";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(Path.Combine(dir, fullName + ".json"))) return;


            File.Copy(Path.Combine(PathHandler.UndermineSaveDir, @$"Save{SaveNumber.Value}.json"),
                Path.Combine(dir, fullName + ".json"));
            File.Copy(Path.Combine(PathHandler.LogsDir, "map.log"), Path.Combine(dir, fullName + ".log"));
            File.Create(Path.Combine(dir, fullName + ".info")).Close();

            System.Threading.Thread.Sleep(1000);
            Process.Start(Path.Combine(dir, fullName + ".info"));
        }

        private void Test()
        {
            string extraDir = Path.Combine(PathHandler.UndermineSaveDir, "../");
            string logDir = PathHandler.LogsDir;
            string logFilePath = Path.Combine(logDir, "map.log");
            string extraLogFilePath = Path.Combine(extraDir, "extra.log");


            using (StreamReader reader2 =
                   new StreamReader(File.Open(extraLogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                       Encoding.UTF8))
            {
                using (StreamReader reader =
                       new StreamReader(File.Open(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                           Encoding.UTF8))
                {
                    if (!File.Exists(logFilePath)) throw new InvalidOperationException();
                    if (!File.Exists(extraLogFilePath)) throw new InvalidOperationException();
                    var logFile = File.ReadAllLines(logFilePath);
                    var extraLogFile = File.ReadAllLines(extraLogFilePath);

                    if (logFile.Length != extraLogFile.Length || logFile.Length == 0 || extraLogFile.Length == 0)
                        throw new InvalidOperationException();

                    foreach (string line in File.ReadLines(logFilePath))
                    {
                        string? log = reader.ReadLine();
                        string? extra = reader2.ReadLine();

                        var logMessage = float.Parse(log ?? throw new InvalidOperationException());
                        var expectedOutput = float.Parse(extra ?? throw new InvalidOperationException());

                        if (System.Math.Abs(logMessage - expectedOutput) > 0.0001)
                        {
                            Console.WriteLine("extra: " + extra);
                            Console.WriteLine("map: " + log);
                            Console.WriteLine("log file: " + logFilePath);
                            Console.WriteLine("extra log file: " + extraLogFilePath);
                            throw new InvalidOperationException();
                        }
                    }
                }
            }
        }
    }
}