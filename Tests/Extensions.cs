using Map_Generator.Undermine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Extensions
    {
        [Test]
        public void MyGetHashCode()
        {
            const string str = "Hello World!";
            const int expected = -0x768E65AB;

            int actual = str.MyGetHashCode();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MyGetHashCode2()
        {
            const string str = @"&$%&34348hthrFDGdfg yjT&;p[0[:';i'";
            const int expected = -0x250700E7;

            int actual = str.MyGetHashCode();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MyGetHashCode3()
        {
            const string str = "ThIs Is A tEsT";
            const int expected = -0x84B3CD5;

            int actual = str.MyGetHashCode();
            Assert.AreEqual(expected, actual);
        }
    }
}