using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Logging
{
    public class Logger
    {
        private string filename;

        public Logger()
        {
            // Get the directory for storing logs
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Saber Tool Plus", "Logs");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Set the filename with the current timestamp
            filename = Path.Combine(logDirectory, filename ?? $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
        }

        public void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]");
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            string callingMethod = method.DeclaringType.Name + "." + method.Name;
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine($"{timestamp} [{callingMethod}] {message}");
            }
        }
    }
}

