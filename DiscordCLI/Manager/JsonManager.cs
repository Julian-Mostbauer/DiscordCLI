using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using DiscordCLI.Network;

namespace DiscordCLI.Manager;

[JsonSerializable(typeof(Settings))]
public partial class SettingsJsonContext : JsonSerializerContext
{
}

public static class JsonManager
{
    private const string SettingsPath = "/home/julian/RiderProjects/DiscordCLI/DiscordCLI/ClientSettings.json";

    public static async Task<Settings?> LoadSettingsAsync()
    {
        await using FileStream openStream = File.OpenRead(SettingsPath);
        return await JsonSerializer.DeserializeAsync(openStream, SettingsJsonContext.Default.Settings);
    }

    public static Settings LoadSettingsSync()
    {
        using FileStream openStream = File.OpenRead(SettingsPath);
        return JsonSerializer.Deserialize(openStream, SettingsJsonContext.Default.Settings)
               ?? throw new DataException("Could not load settings");
    }

    public static void ValidateSettings(this Settings settings)
    {
        if (!IsValidToken(settings.User.Token).Result)
        {
            throw new DataException("Invalid token");
        }

        if (settings.Client.Cache.Active)
        {
            if (!Directory.Exists(settings.Client.Cache.Location))
            {
                throw new DataException("Cache location does not exist");
                // Directory.CreateDirectory(settings.Client.Cache.Location);
            }
        }
    }

    private static async Task<bool> IsValidToken(string token)
    {
        var client = new HttpClient
        {
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue(token)
            }
        };
        var response = await client.GetAsync("https://discord.com/api/v9/users/@me");
        return response.IsSuccessStatusCode;
    }
}