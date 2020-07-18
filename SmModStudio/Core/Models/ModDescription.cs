using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmModStudio.Core.Models
{

    public class ModDescription
    {

        private string _path;

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("localId")] public string Id { get; set; }

        [JsonPropertyName("name")] public string Title { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("version")] public int Version { get; set; }

        public void Save()
        {
            var data = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_path, data);
        }

        public static ModDescription Load(string path)
        {
            var obj = JsonSerializer.Deserialize<ModDescription>(File.ReadAllText(path));
            obj._path = path;
            return obj;
        }

    }

}