using System;
using System.IO;
using Newtonsoft.Json;

namespace SmModStudio.Core.Models
{

    public class ModDescriptionModel
    {

        [JsonProperty("localId")]
        public Guid Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("fileId")]
        public uint WorkshopId { get; private set; }

        public static ModDescriptionModel Load(string path)
        {
            var content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ModDescriptionModel>(content);
        }

    }

}