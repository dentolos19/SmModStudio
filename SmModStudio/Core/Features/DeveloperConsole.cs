using System;
using System.Diagnostics;
using System.IO;

namespace SmModStudio.Core.Features
{

    public class DeveloperConsole
    {

        private string _writerOutput;
        private StreamWriter _writer;
        
        public bool IsActivated { get; private set; }
        public bool AlsoUseDebug { get; set; }
        
        private static Random Generator = new Random();
        public static DeveloperConsole Instance { get; } = new DeveloperConsole();

        private string AddZeroIfNeeded(int value)
        {
            if (value.ToString().Length < 2)
                return "0" + value;
            return value.ToString();
        }
        
        public void Activate()
        {
            if (IsActivated)
                return;
            Native.AllocConsole();
            Console.Title = "Studio Developer Console";
            _writerOutput = Path.Combine(Path.GetTempPath(), "smms-" + Generator.Next(int.MaxValue) + ".log");
            _writer = new StreamWriter(_writerOutput);
            var time = DateTime.Now;
            var format = $"[SMMS // Developer Console] Activated on {time.Year}-{AddZeroIfNeeded(time.Month)}-{AddZeroIfNeeded(time.Day)}; {AddZeroIfNeeded(time.Hour)}:{AddZeroIfNeeded(time.Minute)}:{AddZeroIfNeeded(time.Second)}.";
            Console.WriteLine(format);
            if (AlsoUseDebug)
                Debug.WriteLine(format);
            _writer.WriteLine();
            IsActivated = true;
        }

        public void Deactivate()
        {
            if (!IsActivated)
                return;
            _writer.Close();
            File.Delete(_writerOutput);
            Console.Clear();
            Native.FreeConsole();
            IsActivated = false;
        }

        public void Log(string message)
        {
            if (!IsActivated)
                return;
            var time = DateTime.Now;
            var format = $"[SMMS // {AddZeroIfNeeded(time.Hour)}:{AddZeroIfNeeded(time.Minute)}:{AddZeroIfNeeded(time.Second)}] {message}";
            Console.WriteLine(format);
            if (AlsoUseDebug)
                Debug.WriteLine(format);
        }

        public void Save(string path)
        {
            _writer.Flush();
            File.Copy(_writerOutput, path);
        }

    }

}