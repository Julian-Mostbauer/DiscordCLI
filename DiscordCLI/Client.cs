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

    public void Run()
    {
        var channels = _networkManager.GetOpenChannels().Result;
        var erikChannel = channels.First(c => c.Recipients.First().Username == "integr_");

        string msg;
        do
        {
            Console.Write("Enter message: ");
            msg = Console.ReadLine()!;
            var res = _networkManager.SendMessage(erikChannel.Id, msg).Result;
            Console.WriteLine(res ? "Message sent." : "Failed to send message.");
        } while (msg != "exit");
    }

    public void Exit()
    {
        Console.WriteLine("Exiting...");
        _cacheManager.Save();
    }
}