using System.Text.Json;

namespace DiscordCLI.SerializableTypes;

public static partial class Serialize
{
    public static string ToJson(this Settings self) =>
        JsonSerializer.Serialize(self, JsonContext.Default.Settings);

    public static Settings FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.Settings)!;
}