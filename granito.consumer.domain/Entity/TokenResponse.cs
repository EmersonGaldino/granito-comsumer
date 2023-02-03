using System.Text.Json.Serialization;

namespace granito.consumer.domain.Entity;

public class TokenResponse
{
    [JsonPropertyName("data")]
    public DataResponse data { get; set; }
}
public class DataResponse
{
    [JsonPropertyName("authenticate")]
    public bool authenticate { get; set; }

    [JsonPropertyName("created")]
    public DateTime created { get; set; }

    [JsonPropertyName("expires")]
    public DateTime expires { get; set; }

    [JsonPropertyName("token")]
    public string token { get; set; }
}