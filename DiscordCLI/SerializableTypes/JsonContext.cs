using System.Text.Json.Serialization;
using DiscordCLI.SerializableTypes.DiscordTypes;

namespace DiscordCLI.SerializableTypes
{
    [JsonSerializable(typeof(Settings[]))]
    [JsonSerializable(typeof(Settings))]
    [JsonSerializable(typeof(ClientSettings))]
    [JsonSerializable(typeof(Activity))]
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
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class JsonContext : JsonSerializerContext
    {
    }
}