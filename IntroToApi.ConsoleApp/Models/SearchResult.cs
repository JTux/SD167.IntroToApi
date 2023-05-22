using System.Text.Json.Serialization;

// Generic class that requires a reference type as the given T
public class SearchResult<T> where T : class
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("results")]
    public List<T> Results { get; set; } = new List<T>();
}
