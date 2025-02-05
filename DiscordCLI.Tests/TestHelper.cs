using System.Diagnostics.CodeAnalysis;
using DiscordCLI.SerializableTypes;

namespace DiscordCLI.Tests;

internal static class TestHelper
{
    public static Settings GenerateRandomSettings()
    {
        var faker = new Faker();
        var token = faker.Random.AlphaNumeric(30);
        var readOnly = faker.Random.Bool();
        var debug = faker.Random.Bool();
        var activityName = faker.Lorem.Word();
        var activityType = faker.Random.Int(0, 5);
        var cacheLimit = faker.Random.Int(1, 10000);
        var cacheActive = faker.Random.Bool();

        return new Settings
        {
            UserSettings = new UserSettings
            {
                Token = token
            },
            Client = new ClientSettings
            {
                ReadOnly = readOnly,
                Debug = debug,
                ActivitySettings = new ActivitySettings
                {
                    Name = activityName,
                    Type = activityType
                },
                CacheSettings = new CacheSettings
                {
                    Limit = cacheLimit,
                    Active = cacheActive
                }
            }
        };
    }

    public static string SettingsToJson(Settings settings)
    {
        return GenerateSettingsJson(settings.UserSettings.Token, settings.Client.ReadOnly, settings.Client.Debug,
            settings.Client.ActivitySettings.Name, settings.Client.ActivitySettings.Type,
            settings.Client.CacheSettings.Limit, settings.Client.CacheSettings.Active);
    }

    private static string GenerateSettingsJson(string token, LowerCaseWrapper<bool> readOnly,
        LowerCaseWrapper<bool> debug, string activityName,
        int activityType, int cacheLimit, LowerCaseWrapper<bool> cacheActive)
    {
        return $$"""
                         {
                             "User": {
                                 "Token": "{{token}}"
                             },
                             "Client": {
                                 "ReadOnly": {{readOnly}},
                                 "Debug": {{debug}},
                                 "Activity": {
                                     "Type": {{activityType}},
                                     "Name": "{{activityName}}"
                                 },
                                 "Cache": {
                                     "Limit": {{cacheLimit}},
                                     "Active": {{cacheActive}}
                                 }
                             }
                         }
                 """;
    }
}

internal readonly record struct LowerCaseWrapper<T>
{
    [NotNull] private T Value { get; init; }

    public override string ToString()
    {
        return Value!.ToString()!.ToLower();
    }

    public static implicit operator T(LowerCaseWrapper<T> wrapper) => wrapper.Value;
    public static implicit operator LowerCaseWrapper<T>(T value) => new() { Value = value };
}