using System;
using System.Drawing;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class DoorExtension
    {
        [Test]
        public void GetDoorImage()
        {
            foreach (Door door in Enum.GetValues(typeof(Door)))
                Assert.DoesNotThrow(() => door.GetDoorImage());
        }
    }
}