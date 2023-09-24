using System;
using System.Drawing;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class MapIconExtension
    {
        [Test]
        public void GetDoorImage()
        {
            foreach (MapIcon map in Enum.GetValues(typeof(MapIcon)))
                Assert.DoesNotThrow(() => map.GetMapImage());
        }
    }
}