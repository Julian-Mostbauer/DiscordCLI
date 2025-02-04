using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes.DiscordTypes;

public class Message
{
    [JsonPropertyName("content")] public string Content { get; set; }   
}