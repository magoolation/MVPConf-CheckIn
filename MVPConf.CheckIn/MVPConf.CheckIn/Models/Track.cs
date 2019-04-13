using Newtonsoft.Json;

namespace MVPConf.CheckIn.Models
{
    public class Track
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Name { get; set;}
    }
}