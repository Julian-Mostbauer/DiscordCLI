namespace DiscordCLI;

static class Program
{
    public static void Main()
    {
        var client = Client.Instance;
        client.Run();
    }
}