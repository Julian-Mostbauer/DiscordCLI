namespace DiscordCLI.Manager;

public class ManagerClient
{
    public Settings Settings { get; private set; }
    private static ManagerClient? _instance;

    public static ManagerClient Instance => _instance ??= new();

    private ManagerClient()
    {
        var settings = JsonManager.LoadSettingsSync();
        settings.ValidateSettings();

        Settings = settings;
    }
}