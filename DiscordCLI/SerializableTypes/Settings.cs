using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes;

public class Settings : IFromJsonAble<Settings>, IComparable
{
    [JsonPropertyName("User")] public UserSettings UserSettings { get; set; }

    [JsonPropertyName("Client")] public ClientSettings Client { get; set; }

    public static Settings FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.Settings)
        ?? throw new InvalidOperationException("Failed to deserialize");

    public static Settings[] ManyFromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.SettingsArray)
        ?? throw new InvalidOperationException("Failed to deserialize");

    public int CompareTo(object? obj)
    {
        if (obj is not Settings other) return -1;
        return UserSettings.Token == other.UserSettings.Token
            && Client.ReadOnly == other.Client.ReadOnly
            && Client.Debug == other.Client.Debug
            && Client.ActivitySettings.Name == other.Client.ActivitySettings.Name
            && Client.ActivitySettings.Type == other.Client.ActivitySettings.Type
            && Client.CacheSettings.Limit == other.Client.CacheSettings.Limit
            && Client.CacheSettings.Active == other.Client.CacheSettings.Active
            ? 0
            : -1;
    }
}

public class ClientSettings
{
    [JsonPropertyName("ReadOnly")] public bool ReadOnly { get; set; }

    [JsonPropertyName("Debug")] public bool Debug { get; set; }

    [JsonPropertyName("Activity")] public ActivitySettings ActivitySettings { get; set; }

    [JsonPropertyName("Cache")] public CacheSettings CacheSettings { get; set; }
}

public class ActivitySettings
{
    [JsonPropertyName("Type")] public int Type { get; set; }

    [JsonPropertyName("Name")] public string Name { get; set; }
}

public class CacheSettings
{
    [JsonPropertyName("Limit")] public int Limit { get; set; }

    [JsonPropertyName("Active")] public bool Active { get; set; }
}

public class UserSettings
{
    [JsonPropertyName("Token")] public string Token { get; set; }
}