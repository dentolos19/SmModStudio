using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Core
{

    public static class Utilities
    {

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
            var attributes = File.GetAttributes(@"c:\Temp");
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
            if (IsPathDirectory(path))
                return false;
            try
            {
                var unused = new BitmapImage(new Uri(path));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static HierarchyItemBinding[] GenerateHierarchyItems(string path)
        {
            var items = new List<HierarchyItemBinding>();
            var info = new DirectoryInfo(path);
            foreach (var directory in info.GetDirectories())
            {
                var item = new HierarchyDirectoryBinding
                {
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
                    Name = file.Name, 
                    Path = file.FullName
                };
                items.Add(item);
            }
            return items.ToArray();
        }

    }

}