using Map_Generator.Undermine;
using NUnit.Framework;

namespace Tests.Extension
{
    [TestFixture]
    public class Extensions
    {
        [Test]
        [TestCase("Hello World!", -0x768E65AB)]
        [TestCase(@"&$%&34348hthrFDGdfg yjT&;p[0[:';i'", -0x250700E7)]
        [TestCase("ThIs Is A tEsT", -0x84B3CD5)]
        [TestCase("abcdefghijklmnopqrstuvwxyz", 0x3D0F219F)]
        [TestCase(@"LKY*&^% &^%&%^#$%@#T SDFSDGHL{P{}:HFDCV", 0x6C95D6AD)]
        public void MyGetHashCode(string str, int expected)
        {
            int actual = str.MyGetHashCode();
            Assert.AreEqual(expected, actual);
        }
    }
}