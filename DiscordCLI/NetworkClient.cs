using System.Net.Http.Headers;
using System.Text.Json;
using DiscordCLI.Network.ResponseTypes;

namespace DiscordCLI;

public class NetworkClient(string userToken)
{
    private readonly HttpClient _sharedClient = new()
    {
        BaseAddress = new Uri("https://discord.com/api/v9/"),
        DefaultRequestHeaders =
        {
            Authorization = new AuthenticationHeaderValue(userToken)
        }
    };

    public async Task<Channel[]> GetOpenChannels()
    {
        var response = await _sharedClient.GetAsync("users/@me/channels");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize(jsonResponse, ChannelJsonContext.Default.ChannelArray)
               ?? throw new InvalidOperationException("Failed to deserialize channels");
    }
}