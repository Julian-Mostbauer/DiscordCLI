using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes
{
    [JsonSerializable(typeof(Settings))]
    [JsonSerializable(typeof(ClientSettings))]
    [JsonSerializable(typeof(Activity))]
    [JsonSerializable(typeof(CacheSettings))]
    [JsonSerializable(typeof(UserSettings))]
    [JsonSerializable(typeof(Channel))]
    [JsonSerializable(typeof(Recipient))]
    [JsonSourceGenerationOptions(WriteIndented = true)]
    internal partial class JsonContext : JsonSerializerContext { }
}