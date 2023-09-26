using System;
using System.IO;
using System.Text;
using Map_Generator;
using Map_Generator.Parsing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Program
    {
#if DEBUG
        [Test]
        public void TestAll()
        {
            string globalTestsDir = PathHandler.GlobalTestsDir;
            const string pattern = "*.log";
            string[] files = PathHandler.GetFiles(globalTestsDir, pattern, SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string logDir = PathHandler.LogsDir;
                string logFilePath = Path.Combine(logDir, "map.log");

                string fileName = Path.GetFileName(file);
                string dirName = Path.GetDirectoryName(file) ?? throw new InvalidOperationException();
                string testName = fileName.Substring(0, fileName.IndexOf('.'));

                string jsonTestPath = Path.Combine(dirName, testName + ".json");

                //make sure the json file exists
                Assert.IsTrue(File.Exists(jsonTestPath), testName);

                //turn off console output
                BardLog.IsLoggingToConsole = false;
                TestStart(Path.Combine(dirName, testName + ".json"));
                BardLog.IsLoggingToConsole = true;

                using (StreamReader reader =
                       new StreamReader(File.Open(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
                           Encoding.UTF8))
                {
                    //make sure the log file exists
                    Assert.IsTrue(File.Exists(logFilePath), testName);
                    Assert.AreEqual(new FileInfo(file).Length, new FileInfo(logFilePath).Length, testName);

                    foreach (string line in File.ReadLines(file))
                    {
                        string? log = reader.ReadLine();

                        string logMessage = log ?? throw new InvalidOperationException();
                        string expectedOutput = line ?? throw new InvalidOperationException();

                        //make sure the log file matches the expected output
                        Assert.AreEqual(expectedOutput, logMessage, testName);
                    }
                }
            }
        }

        public void TestStart(string testName)
        {
            Map_Generator.Program.Start(testName);
        }
#endif
    }
}