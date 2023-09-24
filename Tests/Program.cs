using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json.Enums;
using Map_Generator.Parsing.Json.Interfaces;
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
            const string pattern = "Test*.log";
            string[] files = PathHandler.GetFiles(globalTestsDir, pattern, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string logDir = PathHandler.LogsDir;
                string logFilePath = Path.Combine(logDir, "map.log");

                string fileName = Path.GetFileName(file);
                string testName = fileName.Substring(0, fileName.IndexOf('.'));

                string jsonTestPath = Path.Combine(globalTestsDir, testName + ".json");

                //make sure the json file exists
                Assert.IsTrue(File.Exists(jsonTestPath), testName);

                //start program
                TestStart(Path.Combine(globalTestsDir, testName + ".json"));

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

                        string logMessage = log?.Substring(0, 7) ?? throw new InvalidOperationException();
                        string expectedOutput = line.Substring(0, 7);

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