using System.Text.Json.Serialization;

namespace granito.consumer.domain.Entity;

public class JurosResponse
{
    [JsonPropertyName("data")]
    public DataResult data { get; set; }
}
public class DataResult
{
    [JsonPropertyName("value")]
    public int value { get; set; }
}