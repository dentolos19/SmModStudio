using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace SmModStudio.Core
{

    public static class Utilities
    {

        public static void SetAppTheme(string accent, bool setDark)
        {
            var dictionary = new ResourceDictionary
            {
                Source = setDark
                    ? new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.{accent}.xaml")
                    : new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.{accent}.xaml")
            };
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        public static string RetrieveResourceData(string resourceName)
        {
            var manifest = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (manifest == null)
                return null;
            var reader = new StreamReader(manifest);
            var result = reader.ReadToEnd();
            reader.Close();
            manifest.Close();
            return result;
        }

        public static IHighlightingDefinition ConvertXmlToSyntax(string data)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var reader = new XmlTextReader(stream);
            var result = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            reader.Close();
            stream.Close();
            return result;
        }

        public static void RestartApp(string args = null)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            if (location.ToLower().EndsWith(".dll"))
                location = Path.Combine(Path.GetDirectoryName(location)!, Path.GetFileNameWithoutExtension(location) + ".exe");
            Process.Start(location, args);
            Application.Current.Shutdown();
        }

        public static string GetGameUserPath()
        {
            var usersPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Axolot Games", "Scrap Mechanic", "User");
            if (!Directory.Exists(usersPath))
                return null;
            var usersPaths = Directory.GetDirectories(usersPath);
            if (!(usersPath.Length > 0))
                return null;
            return usersPaths[0];
        }

        public static bool IsFileEditable(string path)
        {
            var content = File.ReadAllBytes(path);
            for (var index = 1; index < 512 && index < content.Length; index++)
                if (content[index] == 0x00 && content[index - 1] == 0x00)
                    return false;
            return true;
        }

        public static bool IsFileAnImage(string path)
        {
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

        public static bool IsFileAn3DObject(string path)
        {
            var extension = Path.GetExtension(path).ToLower();
            if (extension.Equals(".3ds")) return true;
            if (extension.Equals(".lwo")) return true;
            if (extension.Equals(".off")) return true;
            if (extension.Equals(".obj")) return true;
            if (extension.Equals(".stl")) return true;
            return false;
        }

        public static void CopyDirectory(string inputPath, string outputPath)
        {
            var dir = new DirectoryInfo(inputPath);
            var dirs = dir.GetDirectories();
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tempPath = Path.Combine(outputPath, file.Name);
                file.CopyTo(tempPath);
            }
            foreach (var subdir in dirs)
            {
                var tempPath = Path.Combine(outputPath, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }

    }

}