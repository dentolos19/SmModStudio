using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Core
{

    public static class Utilities
    {

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        public static void RestartApp(string args = null)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            if (location.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
                location = Path.Combine(Path.GetDirectoryName(location)!, Path.GetFileNameWithoutExtension(location) + ".exe");
            Process.Start(location, args ?? string.Empty);
            Application.Current.Shutdown();
        }

        public static bool IsPathDirectory(string path)
        {
            var attributes = File.GetAttributes(path);
            return attributes.HasFlag(FileAttributes.Directory);
        }

        public static bool IsFileEditable(string path)
        {
            if (IsPathDirectory(path))
                return false;
            var content = File.ReadAllBytes(path);
            for (var index = 1; index < 512 && index < content.Length; index++)
                if (content[index] == 0x00 && content[index - 1] == 0x00)
                    return false;
            return true;
        }

        public static bool IsImagePreviewable(string path)
        {
            try
            {
                if (IsPathDirectory(path))
                    return false;
                var unused = new BitmapImage(new Uri(path));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static HierarchyItemBinding[] GenerateHierarchyItems(string path)
        {
            var items = new List<HierarchyItemBinding>();
            var info = new DirectoryInfo(path);
            foreach (var directory in info.GetDirectories())
            {
                var item = new HierarchyDirectoryBinding
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/Folder.png")),
                    Name = directory.Name,
                    Path = directory.FullName,
                    Items = GenerateHierarchyItems(directory.FullName)
                };
                items.Add(item);
            }
            foreach(var file in info.GetFiles())
            {
                var item = new HierarchyFileBinding
                {
                    Icon = new BitmapImage(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/File.png")),
                    Name = file.Name, 
                    Path = file.FullName
                };
                items.Add(item);
            }
            return items.ToArray();
        }

    }

}