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
        bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n < 4) return true;
            if (n % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0) return false;
            }

            return true;
        }

        var channels = _networkManager.GetOpenChannels().Result;
        var erikChannel = channels.First(c => c.Recipients.First().Username == "integr_");

        for (int i = 2; i < 10000; i++)
        {
            Console.WriteLine($"Checking {i}");
            if (!IsPrime(i)) continue;

            var success = await _networkManager.SendMessage(erikChannel.Id, $"Prime number found: {i}");

            if (!success.IsSuccessStatusCode)
            {
                var res = success.Content.ReadAsStringAsync().Result;
                Console.WriteLine(res);

                var error = MessageErrorResponse.FromJson(res);
                await Task.Delay(3 * (int)(1000 * error.RetryAfter));
            }
        }
    }

    public void Exit()
    {
        Console.WriteLine("Exiting...");
        _cacheManager.Save();
    }
}