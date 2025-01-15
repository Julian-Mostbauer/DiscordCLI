using System.Text.Json.Serialization;

namespace DiscordCLI.Network.ResponseTypes;

public record Recipient(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("global_name")] string GlobalName,
    [property: JsonPropertyName("avatar")] string Avatar,
    [property: JsonPropertyName("avatar_decoration_data")] object AvatarDecorationData,
    [property: JsonPropertyName("discriminator")] string Discriminator,
    [property: JsonPropertyName("public_flags")] int PublicFlags,
    [property: JsonPropertyName("primary_guild")] object PrimaryGuild,
    [property: JsonPropertyName("clan")] object Clan
);