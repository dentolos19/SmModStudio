using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
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
            Process.Start(Assembly.GetExecutingAssembly().Location, args);
            Application.Current.Shutdown();
        }

    }

}