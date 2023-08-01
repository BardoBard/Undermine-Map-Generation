using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Map_Generator.Parsing;

public class PathHandler
{
    private static readonly string
        BasePath = Directory.GetParent(FindDirectory(Application.StartupPath, "Data", 3))?.FullName ??
                   throw new Exception("Could not find base path");

    private static readonly string LocalLowPath =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

    public static readonly string UnderminePath = LocalLowPath + @"\Thorium Entertainment\UnderMine\";

    public static readonly string DataPath = Path.Combine(BasePath, @"Data\");
    public static readonly string ImagesPath = Path.Combine(DataPath, @"Images\");
    public static readonly string MapPath = Path.Combine(ImagesPath, @"Maps\");
    public static readonly string DoorPath = Path.Combine(ImagesPath, @"Doors\");

    public static string FindDirectory(string baseDirectory, string targetDirectory, int backwards)
    {
        FindDirectory(baseDirectory, targetDirectory, backwards, out var result);
        return result;
    }

    public static bool FindDirectory(string baseDirectory, string targetDirectory, int backwards, out string result)
    {
        result = "";
        if (Directory.Exists(baseDirectory))
        {
            if (backwards == 0 || FindDirectory(Directory.GetParent(baseDirectory).FullName, targetDirectory,
                    --backwards, out result))
            {
                if (!string.IsNullOrEmpty(result))
                    return true;

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
            }

            return false;
        }

        throw new DirectoryNotFoundException($"Could not find directory {targetDirectory} in {baseDirectory}");
    }
}