using System.Text.Json;
using DiscordCLI.SerializableTypes;

namespace DiscordCLI;

public class Client
{
    private readonly Settings _settings;
    private readonly NetworkClient _networkClient;
    private readonly Cache _cache;

    private readonly string _appPath;

    public Client(string appPath)
    {
        _appPath = appPath;
        var settingsPath = Path.Combine(appPath, "settings.json");
        var cachePath = Path.Combine(appPath, "cache.csv");

        if (!File.Exists(settingsPath)) throw new ArgumentException("Settings file does not exist.");
        if (!File.Exists(cachePath))
        {
            File.WriteAllLines(cachePath, ["VALID:", "INVALID:"]);
        }

        var settingsContent = File.ReadAllText(settingsPath);

        _settings = Settings.FromJson(settingsContent);
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