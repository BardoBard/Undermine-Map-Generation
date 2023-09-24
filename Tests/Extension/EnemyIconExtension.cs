using System;
using System.Drawing;
using Map_Generator.Parsing.Json.Enums;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class EnemyIconExtension
    {
        [Test]
        public void GetDoorImage()
        {
            foreach (EnemyIcon enemy in Enum.GetValues(typeof(EnemyIcon)))
                Assert.DoesNotThrow(() => enemy.GetEnemyImage());
        }
    }
}