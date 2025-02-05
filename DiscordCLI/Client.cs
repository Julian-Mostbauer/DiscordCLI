using System.Text.Json;
using DiscordCLI.SerializableTypes;
using DiscordCLI.SerializableTypes.DiscordTypes;

namespace DiscordCLI;

public class Client
{
    private static class Constants
    {
        public const string SettingsFileName = "settings.json";
        public const string CacheFileName = "cacheData.json";
    }

    private readonly Settings _settings;
    private readonly NetworkManager _networkManager;
    private readonly CacheManager _cacheManager;

    public Client(string appPath)
    {
        var settingsPath = Path.Combine(appPath, Constants.SettingsFileName);
        var cachePath = Path.Combine(appPath, Constants.CacheFileName);

        if (!File.Exists(settingsPath)) throw new ArgumentException("Settings file does not exist.");

        _settings = Settings.FromJson(File.ReadAllText(settingsPath));
        _cacheManager = new CacheManager(_settings.Client.CacheSettings, cachePath);
        _networkManager = new NetworkManager(_cacheManager, _settings.UserSettings.Token);
    }

    public async Task Run()
    {
        var channels = _networkManager.GetOpenChannels().Result;
        var exampleChannel = channels.First(c => c.Recipients.First().Username == "kxrim_ae");

        var res = _networkManager.SendMessage(exampleChannel.Id, "Hello, World!").Result;
        if (!res.IsSuccessStatusCode)
        {
            Console.WriteLine(res.Content.ReadAsStringAsync().Result);
        }
    }

    public void Exit()
    {
        Console.WriteLine("Exiting...");
        _cacheManager.Save();
    }
}