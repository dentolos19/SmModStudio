﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
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

        public static string GetSteamLocation()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam");
            if (key == null)
                return null;
            var installPath = (string)key.GetValue("InstallPath");
            return string.IsNullOrEmpty(installPath) ? null : installPath;
        }

        public static string GetSteamAppsLocation(string steamPath)
        {
            var deserialized = VdfConvert.Deserialize(File.ReadAllText(Path.Combine(steamPath, "steamapps", "libraryfolders.vdf")));
            var values = (VObject)deserialized.Value;
            var value = values["1"]?.Value<string>() ?? string.Empty;
            return value;
        }

        public static bool CheckSteamLocation(string steamPath)
        {
            if (!Directory.Exists(Path.Combine(steamPath, "steamapps", "workshop", "content", Constants.GameId.ToString())))
                return false;
            if (!File.Exists(Path.Combine(steamPath, "steamapps", "common", "Scrap Mechanic", "Release", "ScrapMechanic.exe")))
                return false;
            return true;
        }

        public static bool IsPathDirectory(string directoryPath)
        {
            var attributes = File.GetAttributes(directoryPath);
            return attributes.HasFlag(FileAttributes.Directory);
        }

        public static bool IsFileEditable(string filePath)
        {
            if (IsPathDirectory(filePath))
                return false;
            var content = File.ReadAllBytes(filePath);
            for (var index = 1; index < 512 && index < content.Length; index++)
                if (content[index] == 0x00 && content[index - 1] == 0x00)
                    return false;
            return true;
        }

        public static bool IsImagePreviewable(string imagePath)
        {
            try
            {
                if (IsPathDirectory(imagePath))
                    return false;
                var unused = new BitmapImage(new Uri(imagePath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static HierarchyItemBinding[] GenerateHierarchyItems(string directoryPath)
        {
            var items = new List<HierarchyItemBinding>();
            var info = new DirectoryInfo(directoryPath);
            foreach (var directory in info.GetDirectories())
            {
                var item = new HierarchyDirectoryBinding
                {
                    Icon = Constants.FolderIcon,
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
                    Icon = Constants.FileIcon,
                    Name = file.Name, 
                    Path = file.FullName
                };
                items.Add(item);
            }
            return items.ToArray();
        }

    }

}