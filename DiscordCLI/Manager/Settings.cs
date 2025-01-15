namespace DiscordCLI.Manager;

public record Settings(UserSettings User, ClientSettings Client);

public record UserSettings(string Token);

public record ClientSettings(ActivitySettings Activity, bool Debug, bool ReadOnly, CacheSettings Cache);

public record CacheSettings(string Location, int Limit, bool Active, bool Channels, bool Users, bool Messages);

public record ActivitySettings(string Name, ActivityType Type);

public enum ActivityType
{
    Playing = 0,
    Streaming = 1,
    Listening = 2,
    Watching = 3,
    Custom = 4,
    Competing = 5
}