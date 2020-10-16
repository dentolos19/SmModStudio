using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmModStudio.Core.Enums;

namespace SmModStudio.Core.Models
{

    public class ModDescriptionModel
    {

        [JsonProperty("localId")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModType Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fileId")]
        public uint WorkshopId { get; set; }

        public void Save(string descriptionPath)
        {
            var contents = JsonConvert.SerializeObject(this);
            File.WriteAllText(descriptionPath, contents);
        }

        public static ModDescriptionModel Load(string descriptionPath)
        {
            var contents = File.ReadAllText(descriptionPath);
            return JsonConvert.DeserializeObject<ModDescriptionModel>(contents);
        }

    }

}