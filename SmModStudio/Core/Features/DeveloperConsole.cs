using System;
using System.Diagnostics;
using System.IO;

namespace SmModStudio.Core.Features
{

    public class DeveloperConsole
    {
        
        public bool IsActivated { get; private set; }
        public bool AlsoUseDebug { get; set; }
        
        public static DeveloperConsole Instance { get; } = new DeveloperConsole();

        public void Activate()
        {
            if (IsActivated)
                return;
            Native.AllocConsole();
            Console.Title = "Studio Developer Console";
            var time = DateTime.Now;
            var format = $"[SMMS // Developer Console] Started logging on {time.Year}-{time.Month}-{time.Day}; {time.Hour}:{time.Minute}:{time.Second}.";
            Console.WriteLine(format);
            if (AlsoUseDebug)
                Debug.WriteLine(format);
            IsActivated = true;
        }

        public void Deactivate()
        {
            if (!IsActivated)
                return;
            Console.Clear();
            Native.FreeConsole();
            IsActivated = false;
        }

        public void Log(string message)
        {
            if (!IsActivated)
                return;
            var time = DateTime.Now;
            var format = $"[SMMS // {time.Hour}:{time.Minute}:{time.Second}] {message}";
            Console.WriteLine(format);
            if (AlsoUseDebug)
                Debug.WriteLine(format);
        }

    }

}