using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes
{
    public partial class Settings
    {
        [JsonPropertyName("User")]
        public UserSettings UserSettings { get; set; }

        [JsonPropertyName("Client")]
        public ClientSettings Client { get; set; }

        public static Settings FromJson(string json) =>
            JsonSerializer.Deserialize(json, JsonContext.Default.Settings)!;
    }

    public partial class ClientSettings
    {
        [JsonPropertyName("ReadOnly")]
        public bool ReadOnly { get; set; }

        [JsonPropertyName("Debug")]
        public bool Debug { get; set; }

        [JsonPropertyName("Activity")]
        public Activity Activity { get; set; }

        [JsonPropertyName("Cache")]
        public CacheSettings CacheSettings { get; set; }
    }

    public partial class Activity
    {
        [JsonPropertyName("Type")]
        public long Type { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }
    }

    public partial class CacheSettings
    {
        [JsonPropertyName("Limit")]
        public long Limit { get; set; }

        [JsonPropertyName("Active")]
        public bool Active { get; set; }
    }

    public partial class UserSettings
    {
        [JsonPropertyName("Token")]
        public string Token { get; set; }
    }
}