using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DiscordCLI.SerializableTypes;
using DiscordCLI.SerializableTypes.DiscordTypes;


namespace DiscordCLI;

public class NetworkManager
{
    public NetworkManager(CacheManager cacheManager, string token)
    {
        _cacheManager = cacheManager;
        _token = token;
        _networkClient = new NetworkClient(token);

        if (!ValidateToken()) throw new ArgumentException("Invalid token.");
    }

    private readonly NetworkClient _networkClient;
    private readonly CacheManager _cacheManager;
    private readonly string _token;

    public async Task<bool> SendMessage(string channelId, string msg)
    {
        return await _networkClient.SendMessage(channelId, msg);
    }

    public async Task<HashSet<Channel>> GetOpenChannels()
    {
        if (!_cacheManager.IsActive) return await _networkClient.GetOpenChannels();
        if (_cacheManager.HasStoredChannels) return _cacheManager.GetCachedChannels();

        var channels = await _networkClient.GetOpenChannels();
        _cacheManager.AddChannels(channels);

        return channels;
    }

    private bool ValidateToken()
    {
        if (!_cacheManager.IsActive) return _networkClient.ValidateToken();

        switch (_cacheManager.GetTokenStatus(_token))
        {
            case CacheStatus.Valid:
                return true;
            case CacheStatus.Invalid:
                return false;
            case CacheStatus.Unknown:
                var isValid = _networkClient.ValidateToken();
                _cacheManager.AddToken(_token, isValid);
                return isValid;
            default:
                throw new InvalidOperationException("Unknown cache status.");
        }
    }

    private class NetworkClient
    {
        private readonly HttpClient _sharedClient;
        private readonly string _token;

        public NetworkClient(string userToken)
        {
            _token = userToken;
            _sharedClient = new()
            {
                BaseAddress = new Uri("https://discord.com/api/v9/"),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(userToken)
                }
            };
        }

        public bool ValidateToken()
        {
            return _sharedClient.GetAsync("users/@me").Result.IsSuccessStatusCode;
        }

        public async Task<HashSet<Channel>> GetOpenChannels()
        {
            var response = await _sharedClient.GetAsync("users/@me/channels");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return Channel.ManyFromJson(jsonResponse).ToHashSet();
        }

        public async Task<bool> SendMessage(string channelId, string msg)
        {
            var json = JsonSerializer.Serialize(new Message { Content = msg }, JsonContext.Default.Message);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _sharedClient.PostAsync($"channels/{channelId}/messages", content);

            if (response.IsSuccessStatusCode) return true;

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Error: " + responseContent);
            return false;
        }
    }
}