using System;
using Newtonsoft.Json;

namespace SmModStudio.Core.Models
{

    public class InventoryDescriptionModel
    {

        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

    }

}