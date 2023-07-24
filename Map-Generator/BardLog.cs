using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public static void LogStackTrace(bool overrideIsLogging = false)
    {
        Log("");
        var frames = new StackTrace().GetFrames();
        for (var index = 1; index < frames.Length; index++)
        {
            var frame = frames[index];
            var method = frame.GetMethod();

            //full path
            var fullPath = method?.DeclaringType != null
                ? $"{method.DeclaringType.FullName}.{method.Name}"
                : method?.Name;

            Console.WriteLine(fullPath);
        }

        Console.WriteLine("");
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