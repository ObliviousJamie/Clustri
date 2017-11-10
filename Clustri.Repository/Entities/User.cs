using Newtonsoft.Json;

namespace Clustri.Repository.Entities
{
    public class User
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public double Weight { get; set; }
    }
}
