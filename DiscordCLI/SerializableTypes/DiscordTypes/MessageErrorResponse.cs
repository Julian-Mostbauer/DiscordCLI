using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes.DiscordTypes;

public class MessageErrorResponse : IFromJsonAble<MessageErrorResponse>
{
    [JsonPropertyName("message")] public string Message { get; set; }
    [JsonPropertyName("retry_after")] public double RetryAfter { get; set; }
    [JsonPropertyName("global")] public bool Global { get; set; }
    [JsonPropertyName("code")] public int Code { get; set; }

    public static MessageErrorResponse FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.MessageErrorResponse)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);

    public static MessageErrorResponse[] ManyFromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.MessageErrorResponseArray)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);
}