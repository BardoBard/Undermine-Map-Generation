using System;
using System.Drawing;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class ItemIconExtension
    {
        [Test]
        public void GetDoorImage()
        {
            foreach (ItemIcon item in Enum.GetValues(typeof(ItemIcon)))
                Assert.DoesNotThrow(() => item.GetItemImage());
        }
    }
}