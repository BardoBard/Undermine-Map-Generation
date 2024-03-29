﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Map_Generator.Parsing;

namespace Map_Generator
{
    public static class BardLog
    {
        private static readonly string _logFileDir = PathHandler.LogsDir;
        private static readonly string _logFilePath = _logFileDir + "map.log";
        private static StreamWriter _fs;


        static BardLog()
        {
            IsLoggingToConsole = true;
            Open();
        }

        [Conditional("DEBUG")]
        public static void Open()
        {
            if (_fs != null)
                return;

            if (!Directory.Exists(_logFileDir))
                Directory.CreateDirectory(_logFileDir);

            if (!File.Exists(_logFilePath))
                File.Create(_logFilePath).Close();

            _fs = new StreamWriter(File.Open(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite),
                Encoding.UTF8) { AutoFlush = true };
            LogToFile = s =>
            {
                _fs.WriteLine(s);
            };
            LogToConsole = s =>
            {
                if (IsLoggingToConsole)
                    Console.WriteLine(s);
            };
            LogToFileAndConsole = s =>
            {
                _fs.WriteLine(s);
                LogToConsole(s);
            };

            ClearDebug();
        }

        [Conditional("DEBUG")]
        public static void Close()
        {
            _fs.Close();
            _fs.Dispose();
            _fs = null;
        }

        public static bool IsLoggingToConsole { get; set; }
        public static Action<string> LogToConsole;
        public static Action<string> LogToFile;
        public static Action<string> LogToFileAndConsole;

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
            outputMethod ??= LogToConsole;
            string message = args.Length == 0 ? str ?? string.Empty : string.Format(str ?? string.Empty, args);
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