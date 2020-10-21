using Newtonsoft.Json;

namespace SmModStudio.Core.Models
{

    public class CcNamespaceModel
    {
        
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("members")] public CcMemberModel[] Members { get; set; }

    }

}