using System;
using System.IO;

namespace Map_Generator.Parsing
{
    public static class PathHandler
    {
        public static void watch(string path, string file, Action call)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*.json";
            watcher.Changed += (sender, args) => call();
            watcher.EnableRaisingEvents = true;
        }

        public static readonly string
            BaseDir = Directory.GetParent(FindDirectory(
                          Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!,
                          "Data", 5))?.FullName ??
                      throw new Exception("Could not find base path");

        private static readonly string _localLowPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

        private static readonly string _undermineDir = _localLowPath + @"\Thorium Entertainment\UnderMine\";


        //savedata
        public static readonly string UndermineSaveDir = Path.Combine(_undermineDir, @"Saves\");

        public static readonly string DataDir = Path.Combine(BaseDir, @"Data\");

        //json
        public static readonly string JsonDir = Path.Combine(DataDir, @"Json\");

        //images
        public static readonly string ImagesDir = Path.Combine(DataDir, @"Images\");
        public static readonly string MapDir = Path.Combine(ImagesDir, @"Maps\");
        public static readonly string EnemyDir = Path.Combine(ImagesDir, @"Enemies\");
        public static readonly string ItemDir = Path.Combine(ImagesDir, @"Items\");
        public static readonly string DoorDir = Path.Combine(ImagesDir, @"Doors\");

        //logs
        public static readonly string LogsDir = Path.Combine(DataDir, @"Logs\");


        //test
        public static readonly string TestProjectDir = Path.Combine(BaseDir, @"Tests\");
        public static readonly string TestDataDir = Path.Combine(TestProjectDir, @"TestData\");
        public static readonly string GlobalTestsDir = Path.Combine(TestDataDir, @"GlobalTests\");

        //rest of tests

        public static string FindDirectory(string baseDirectory, string targetDirectory, int backwards)
        {
            FindDirectory(baseDirectory, targetDirectory, backwards, out var result);
            return result;
        }

        public static bool FindDirectory(string? baseDirectory, string targetDirectory, int backwards,
            out string result)
        {
            //TODO: thinking about it, everytime it goes backwards it checks every directory again probably fix that, currently it's not a problem
            result = "";
            if (baseDirectory == null)
                return false;

            if (!Directory.Exists(baseDirectory))
                throw new DirectoryNotFoundException($"Could not find directory {targetDirectory} in {baseDirectory}");

            string[] directories = Directory.GetDirectories(baseDirectory);
            foreach (string directory in directories)
            {
                if (Path.GetFileName(directory).Equals(targetDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    result = directory;
                    return true;
                }

                if (FindDirectory(directory, targetDirectory, 0, out result))
                    return true;
            }

            if (backwards != 0 && !FindDirectory(Directory.GetParent(baseDirectory)?.FullName, targetDirectory,
                    --backwards, out result)) return false;

            return !string.IsNullOrEmpty(result);
        }

        public static string[] GetFiles(string path, string searchPattern,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }
    }
}