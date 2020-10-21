using Newtonsoft.Json;
using SmModStudio.Core.Enums;

namespace SmModStudio.Core.Models
{

    public class CcMemberModel
    {

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("type")] public ApiMemberType Type { get; set; }

    }

}