using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public partial class Encounter
    {
        [Test]
        public void AllowNeighbor01()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.North
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.SE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SW));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor02()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.East
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NS));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor03()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.South
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor04()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.West
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NE));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor05()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.NS
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor06()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.NW
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor07()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.NE
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor08()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.SE
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor09()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.SW
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor10()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.EW
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor11()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.WNE
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor12()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.NES
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor13()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.ESW
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }

        [Test]
        public void AllowNeighbor14()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.SWN
            };

            //cardinal directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.South));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NS));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NE));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.SE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SW));

            Assert.IsFalse(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsFalse(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.NES));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsFalse(encounter.AllowNeighbor(Direction.SWN));
        }[Test]
        public void AllowNeighbor15()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.None
            };

            //cardinal directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.North));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.East));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.South));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.West));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.None));

            //two directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NS));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NE));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.SE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SW));

            Assert.IsTrue(encounter.AllowNeighbor(Direction.EW));

            //three directions
            Assert.IsTrue(encounter.AllowNeighbor(Direction.WNE));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.NES));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.ESW));
            Assert.IsTrue(encounter.AllowNeighbor(Direction.SWN));
        }
    }
}