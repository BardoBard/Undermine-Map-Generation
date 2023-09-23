using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Map_Generator.Parsing;

namespace Map_Generator
{
    public static class BardLog
    {
        private static readonly string _logFileDir = PathHandler.DataDir + @"Logs\";
        private static readonly string _logFilePath = _logFileDir + "map.log";
        private static StreamWriter _fs;


        static BardLog()
        {
            if (!Directory.Exists(_logFileDir))
                Directory.CreateDirectory(_logFileDir);

            if (!Directory.Exists(_logFilePath))
                File.Create(_logFilePath).Close();
            
            _fs = new StreamWriter(_logFilePath, true) { AutoFlush = true };
            LogToFile = _fs.WriteLine;
            LogToFileAndConsole = s =>
            {
                _fs.WriteLine(s);
                Console.WriteLine(s);
            };
            
            ClearDebug();
        }

        public static Action<string> LogToFile;
        public static readonly Action<string> LogToFileAndConsole;

        [Conditional("DEBUG")]
        public static void ClearDebug()
        {
            _fs.Close();
            _fs.Dispose();
            File.WriteAllText(_logFilePath, "");
            _fs = new StreamWriter(_logFilePath, true) { AutoFlush = true };
        }

        [Conditional("DEBUG")]
        public static void LogStackTrace()
        {
            var frames = new StackTrace().GetFrames();
            for (var index = 1; index < frames.Length; index++)
            {
                var frame = frames[index];
                var method = frame.GetMethod();

                //full path
                var fullPath = method?.DeclaringType != null
                    ? $"{method.DeclaringType.FullName}.{method.Name}"
                    : method?.Name;

                Log(fullPath);
            }

            Log("");
        }

        [Conditional("DEBUG")]
        public static void Log(string? str, Action<string>? outputMethod = null, params object?[] args)
        {
            outputMethod ??= Console.WriteLine;
            string message = string.Format(str ?? string.Empty, args);
            outputMethod.Invoke(message);
        }

        [Conditional("DEBUG")]
        public static void Log(string str, params object?[] args) => Log(str, null, args);

        [Conditional("DEBUG")]
        public static void Log(string str) => Log(str, new object?[] { });

        [Conditional("DEBUG")]
        public static void Log<T>(T t, Action<string>? outputMethod = null) where T : IComparable =>
            Log(t.ToString(), outputMethod);

        [Conditional("DEBUG")]
        public static void Log<T>(IEnumerable<T> t, Action<string>? outputMethod = null) where T : IComparable
        {
            foreach (var num in t)
                Log(num.ToString(), outputMethod);
        }
    }
}