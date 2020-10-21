using Newtonsoft.Json;

namespace SmModStudio.Core.Models
{

    public class CcDocumentationModel
    {

        [JsonProperty("version")] public string Version { get; set; }

        [JsonProperty("reference")] public string Reference { get; set; }

        [JsonProperty("namespaces")] public CcNamespaceModel[] Namespaces { get; set; }

    }

}