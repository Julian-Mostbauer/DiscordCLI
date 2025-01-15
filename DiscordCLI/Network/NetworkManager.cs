using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Channels;
using DiscordCLI.Manager;
using DiscordCLI.Network.ResponseTypes;
using Channel = DiscordCLI.Network.ResponseTypes.Channel;

namespace DiscordCLI.Network;

public class NetworkManager
{
    private static NetworkManager? _instance;
    public static NetworkManager Instance => _instance ??= new();

    private readonly NetworkClient _sharedClient;

    private NetworkManager()
    {
        var manager = ManagerClient.Instance;
        _sharedClient = new NetworkClient();
    }

    public async Task<Channel[]> GetOpenChannels()
    {
        return await _sharedClient.GetOpenChannels();
    }
}