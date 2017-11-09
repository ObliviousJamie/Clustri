using Newtonsoft.Json;

namespace Clustri.Core.Entities
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public double Weight { get; set; }
    }
}
