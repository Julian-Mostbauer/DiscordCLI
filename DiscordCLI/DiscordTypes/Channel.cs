using System.Text.Json.Serialization;

namespace DiscordCLI.Network.ResponseTypes;

[JsonSerializable(typeof(Channel[]))]
public partial class ChannelJsonContext : JsonSerializerContext
{
}

public record Channel(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("type")] int Type,
    [property: JsonPropertyName("last_message_id")]
    string LastMessageId,
    [property: JsonPropertyName("flags")] int Flags,
    [property: JsonPropertyName("recipients")]
    Recipient[] Recipients
);