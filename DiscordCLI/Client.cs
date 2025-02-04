using System.Text.Json;
using DiscordCLI.SerializableTypes;

namespace DiscordCLI;

public class Client
{
    private static class Constants
    {
        public const string SettingsFileName = "settings.json";
        public const string CacheFileName = "cacheData.json";
    }

    private readonly Settings _settings;
    private readonly NetworkClient _networkClient;
    private readonly CacheManager _cacheManager;
    
    public Client(string appPath)
    {
        var settingsPath = Path.Combine(appPath, Constants.SettingsFileName);
        var cachePath = Path.Combine(appPath, Constants.CacheFileName);

        if (!File.Exists(settingsPath)) throw new ArgumentException("Settings file does not exist.");

        _settings = Settings.FromJson(File.ReadAllText(settingsPath));
        _cacheManager = new CacheManager(_settings.Client.CacheSettings, cachePath);
        _networkClient = new NetworkClient(_settings.UserSettings.Token, _cacheManager);
    }

    public void Run()
    {
        var _ = _networkClient.GetOpenChannels().Result;
    }

    public void Exit()
    {
        Console.WriteLine("Exiting...");
        _cacheManager.Save();
    }
}