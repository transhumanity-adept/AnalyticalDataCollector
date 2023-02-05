using System.Text.Json.Serialization;

namespace WebApi.Features.Tasks.Contracts;

public class DataCollectionTaskLogDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("logType")]
    public string LogType { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}