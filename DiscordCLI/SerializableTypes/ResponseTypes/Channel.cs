using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes.ResponseTypes;

public class Channel : IFromJsonAble<Channel>
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("type")] public long Type { get; set; }

    [JsonPropertyName("last_message_id")] public string LastMessageId { get; set; }

    [JsonPropertyName("flags")] public long Flags { get; set; }

    [JsonPropertyName("recipients")] public Recipient[] Recipients { get; set; }

    public static Channel FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.Channel)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);

    public static Channel[] ManyFromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.ChannelArray)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);
}