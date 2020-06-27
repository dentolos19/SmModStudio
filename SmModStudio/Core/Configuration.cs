using System;
using System.IO;
using System.Xml.Serialization;

namespace SmModStudio.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SmModStudio.cfg");
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Configuration));

        public void Save()
        {
            var stream = new FileStream(Source, FileMode.Create);
            Serializer.Serialize(stream, this);
            stream.Close();
        }

        public static Configuration Load()
        {
            if (!File.Exists(Source))
                return new Configuration();
            var stream = new FileStream(Source, FileMode.Open);
            var result = (Configuration) Serializer.Deserialize(stream);
            stream.Close();
            return result;
        }

    }

}