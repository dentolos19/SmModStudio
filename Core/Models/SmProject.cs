using System.IO;
using System.Xml.Serialization;

namespace SmModStudio.Core.Models
{
    
    public class SmProject
    {
        
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(SmProject));

        public string Title { get; set; }
        public string Description { get; set; }
        
        public void Save(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Create);
            Serializer.Serialize(stream, this);
            stream.Close();
        }

        public static SmProject Load(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Open);
            var result = (SmProject) Serializer.Deserialize(stream);
            stream.Close();
            return result;
        }
        
    }
    
}