using System.Text.Json;

namespace DiscordCLI.SerializableTypes;

public class CacheData : IFromJsonAble<CacheData>
{
    public HashSet<string> ValidTokens { get; set; } = new();
    public HashSet<string> InvalidTokens { get; set; } = new();

    public HashSet<string> Channels { get; set; } = new();

    public static CacheData FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.CacheData)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);

    public static CacheData[] ManyFromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.CacheDataArray)
        ?? throw new InvalidOperationException("Failed to deserialize: \n" + json);

    public void Clear()
    {
        ValidTokens.Clear();
        InvalidTokens.Clear();
        Channels.Clear();
    }
}