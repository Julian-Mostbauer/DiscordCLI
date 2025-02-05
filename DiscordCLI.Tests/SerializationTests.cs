using DiscordCLI.SerializableTypes;

namespace DiscordCLI.Tests;

public class SerializationTests
{
    [Fact]
    public void Always_Succeed()
    {
        true.Should().BeTrue();
    }

    [Fact]
    public void Settings_FromJson()
    {
        // Arrange
        var expectedSettings = TestHelper.GenerateRandomSettings();
        var settingsJson = TestHelper.SettingsToJson(expectedSettings);

        // Act
        var actualSettings = Settings.FromJson(settingsJson);

        // Assert
        actualSettings.CompareTo(expectedSettings).Should().Be(0);
    }
}

/*
   {
       Client = DiscordCLI.SerializableTypes.ClientSettings
       {
           ActivitySettings = DiscordCLI.SerializableTypes.ActivitySettings
           {
               Name = "beatae",
               Type = 1
           },
           CacheSettings = DiscordCLI.SerializableTypes.CacheSettings
           {
               Active = True,
               Limit = 9675
           },
           Debug = False,
           ReadOnly = False
       },
       UserSettings = DiscordCLI.SerializableTypes.UserSettings
       {
           Token = "0jvkfuhrc6zxjxhunpkdkvk0naf57e"
       }
   }, but found DiscordCLI.SerializableTypes.Settings
   {
       Client = DiscordCLI.SerializableTypes.ClientSettings
       {
           ActivitySettings = DiscordCLI.SerializableTypes.ActivitySettings
           {
               Name = "beatae",
               Type = 1
           },
           CacheSettings = DiscordCLI.SerializableTypes.CacheSettings
           {
               Active = True,
               Limit = 9675
           },
           Debug = False,
           ReadOnly = False
       },
       UserSettings = DiscordCLI.SerializableTypes.UserSettings
       {
           Token = "0jvkfuhrc6zxjxhunpkdkvk0naf57e"
       }
   }.
*/