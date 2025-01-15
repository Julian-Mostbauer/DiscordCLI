using System.Threading.Channels;
using DiscordCLI.Network;
using DiscordCLI.Manager;
using Channel = DiscordCLI.Network.ResponseTypes.Channel;

namespace DiscordCLI;

public class Client
{
    private readonly NetworkManager _networkManager;
    private readonly ManagerClient _clientManager;

    private static Client? _instance;
    public static Client Instance => _instance ??= new();

    private Client()
    {
        _clientManager = ManagerClient.Instance;
        _networkManager = NetworkManager.Instance;
    }
    
    public void Run()
    {
        if (_clientManager.Settings.Client.Debug)
        {
            Console.WriteLine("Debug Mode active");
        }

        var channels = _networkManager.GetOpenChannels().Result;
        foreach (var channel in channels)
        {
            Console.WriteLine("--------------------");
            foreach (var recipient in channel.Recipients)
            {
                Console.WriteLine(recipient);
            }
        }
    }
}