using DiscordCLI.Network;

namespace DiscordCLI;

public class Client(Settings settings)
{
    private readonly Settings _settings = settings;

    public static Client FromSavedSettings(string settingsPath)
    {
        if (!File.Exists(settingsPath)) throw new ArgumentException("Settings file does not exist.");
        var content = File.ReadAllText(settingsPath);
        try
        {
            var settings = Settings.FromJson(content);
            return new Client(settings);
        }
        catch (Exception e)
        {
            throw new ArgumentException("Settings file is not valid JSON.", e);
        }
    }

    public void Run()
    {
        Console.WriteLine(settings.ToJson());
    }
}