using System;
using System.Collections.Generic;
using System.IO;

namespace Map_Generator;

public class BardLog
{
    private static readonly string localLowPath =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "LocalLow");

    private static readonly string logFilePath =
        localLowPath + @"\Thorium Entertainment\UnderMine\map.log";       

    private static StreamWriter fs = new StreamWriter(logFilePath, true) { AutoFlush = true };

    static BardLog()
    {
        ClearDebug();
    }
    public static void ClearDebug()
    {
        fs.Close();
        fs.Dispose();
        File.WriteAllText(logFilePath, "");
        fs = new StreamWriter(logFilePath, true) { AutoFlush = true };
    }       
    public static void Log(string str, params object[] args)
    {

        fs.WriteLine(str, args);
    }

    public static void Log<T>(T t) where T : IComparable
    {
        Log(t.ToString());
    }

    public static void Log<T>(IEnumerable<T> t)
    {
        foreach (var num in t)
            Log(num.ToString());
    }
}