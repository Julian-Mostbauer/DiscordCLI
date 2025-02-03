using System.Text.Json;
using DiscordCLI.SerializableTypes;

namespace DiscordCLI;

public class Client
{
    private static class Constants
    {
        public const string SettingsFileName = "settings.json";
        public const string TokenCacheFileName = "tokenCache.json";
    }

    private readonly Settings _settings;
    private readonly NetworkClient _networkClient;
    private readonly Cache _cache;
    
    public Client(string appPath)
    {
        var settingsPath = Path.Combine(appPath, Constants.SettingsFileName);
        var cachePath = Path.Combine(appPath, Constants.TokenCacheFileName);

        if (!File.Exists(settingsPath)) throw new ArgumentException("Settings file does not exist.");

        _settings = Settings.FromJson(File.ReadAllText(settingsPath));
        _cache = new Cache(_settings.Client.CacheSettings, cachePath);
        _networkClient = new NetworkClient(_settings.UserSettings.Token, _cache);
    }

    public void Run()
    {
        var _ = _networkClient.GetOpenChannels().Result;
    }

    public void Exit()
    {
        Console.WriteLine("Exiting...");
        _cache.Save();
    }
}