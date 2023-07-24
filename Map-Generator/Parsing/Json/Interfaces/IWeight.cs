using Newtonsoft.Json;

namespace Map_Generator.Parsing.Json.Interfaces
{
    public interface IWeight
    {
        [JsonProperty("weight")] public int Weight { get; set; }
        [JsonIgnore] public bool Skip { get; set; }
    }
}