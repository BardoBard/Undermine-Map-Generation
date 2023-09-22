using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Encounter
    {
        [Test]
        public void AllowNeighbor()
        {   
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.None
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.North
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor2()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.West
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.None
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor3()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.South
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.None
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor4()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.South
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.West
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor5()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.East
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.South
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor6()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.East
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.West
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor7()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.East
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.North
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor8()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.North
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.South
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor9()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.North
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.West
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor10()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.None
            };

            Map_Generator.Parsing.Json.Classes.Encounter neighbor = new()
            {
                NoExit = Direction.None
            };

            Assert.IsTrue(encounter.AllowNeighbor(neighbor));
        }

        [Test]
        public void AllowNeighbor11()
        {
            Map_Generator.Parsing.Json.Classes.Encounter encounter = new()
            {
                NoExit = Direction.WNE
            };

            Direction direction = Direction.NS;

            Assert.IsFalse(encounter.AllowNeighbor(direction));
        }
    }
}