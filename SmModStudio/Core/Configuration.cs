using System;
using System.IO;
using System.Xml.Serialization;
using SmModStudio.Core.Enums;

namespace SmModStudio.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "SmModStudio.cfg");
        private static readonly XmlSerializer Serializer = new(typeof(Configuration));

        public AppLanguageOptions AppLanguage { get; set; } = AppLanguageOptions.English;
        public AppThemeOptions AppTheme { get; set; } = AppThemeOptions.Light;

        public string GameDataPath { get; set; }
        public string WorkshopPath { get; set; }
        public string UserDataPath { get; set; }

        public bool AutoSaveClosingFile { get; set; } = true;

        public void Save()
        {
            using var stream = new FileStream(Source, FileMode.Create);
            Serializer.Serialize(stream, this);
        }

        public void Reset()
        {
            if (File.Exists(Source))
                File.Delete(Source);
        }

        public static Configuration Load()
        {
            if (!File.Exists(Source))
                return new Configuration();
            using var stream = new FileStream(Source, FileMode.Open);
            return (Configuration)Serializer.Deserialize(stream);
        }

    }

}