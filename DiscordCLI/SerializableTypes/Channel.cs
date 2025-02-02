using System.Text.Json.Serialization;

namespace DiscordCLI.SerializableTypes
{
    public class Channel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("last_message_id")]
        public string LastMessageId { get; set; }

        [JsonPropertyName("flags")]
        public long Flags { get; set; }

        [JsonPropertyName("recipients")]
        public Recipient[] Recipients { get; set; }
    }

    public class Recipient
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("global_name")]
        public string GlobalName { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("avatar_decoration_data")]
        public object AvatarDecorationData { get; set; }

        [JsonPropertyName("discriminator")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public long Discriminator { get; set; }

        [JsonPropertyName("public_flags")]
        public long PublicFlags { get; set; }

        [JsonPropertyName("primary_guild")]
        public object PrimaryGuild { get; set; }

        [JsonPropertyName("clan")]
        public object Clan { get; set; }
    }
}