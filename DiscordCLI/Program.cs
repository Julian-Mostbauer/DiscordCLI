namespace DiscordCLI;

static class Program
{
    public static void Main()
    {
        const string appPath = "/home/julian/RiderProjects/DiscordCLI/DiscordCLI/AppFiles";

        var client = new Client(appPath);
        client.Run();
        
        client.Exit();
    }
}