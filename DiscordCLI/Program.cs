namespace DiscordCLI;

static class Program
{
    public static void Main()
    {
        var client = Client.FromSavedSettings("/home/julian/RiderProjects/DiscordCLI/DiscordCLI/settings.json");
        client.Run();
    }
}