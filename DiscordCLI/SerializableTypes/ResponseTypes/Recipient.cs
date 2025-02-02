using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes.ResponseTypes;

public class Recipient : IFromJsonAble<Recipient>
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("username")] public string Username { get; set; }

    [JsonPropertyName("global_name")] public string GlobalName { get; set; }

    [JsonPropertyName("avatar")] public string Avatar { get; set; }

    [JsonPropertyName("avatar_decoration_data")]
    public object AvatarDecorationData { get; set; }

    [JsonPropertyName("discriminator")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public long Discriminator { get; set; }

    [JsonPropertyName("public_flags")] public long PublicFlags { get; set; }

    [JsonPropertyName("primary_guild")] public object PrimaryGuild { get; set; }

    [JsonPropertyName("clan")] public object Clan { get; set; }

    public static Recipient FromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.Recipient)
        ?? throw new InvalidOperationException("Failed to deserialize");

    public static Recipient[] ManyFromJson(string json) =>
        JsonSerializer.Deserialize(json, JsonContext.Default.RecipientArray)
        ?? throw new InvalidOperationException("Failed to deserialize");
}