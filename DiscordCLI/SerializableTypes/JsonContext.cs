using System.Text.Json.Serialization;
using DiscordCLI.SerializableTypes.DiscordTypes;

namespace DiscordCLI.SerializableTypes
{
    [JsonSerializable(typeof(Settings[]))]
    [JsonSerializable(typeof(Settings))]
    [JsonSerializable(typeof(ClientSettings))]
    [JsonSerializable(typeof(ActivitySettings))]
    [JsonSerializable(typeof(CacheSettings))]
    [JsonSerializable(typeof(UserSettings))]
    [JsonSerializable(typeof(Channel))]
    [JsonSerializable(typeof(Channel[]))]
    [JsonSerializable(typeof(HashSet<Channel>))]
    [JsonSerializable(typeof(Recipient))]
    [JsonSerializable(typeof(Recipient[]))]
    [JsonSerializable(typeof(CacheData))]
    [JsonSerializable(typeof(CacheData[]))]
    [JsonSerializable(typeof(Message))]
    [JsonSerializable(typeof(MessageErrorResponse))]
    [JsonSerializable(typeof(MessageErrorResponse[]))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class JsonContext : JsonSerializerContext
    {
    }
}