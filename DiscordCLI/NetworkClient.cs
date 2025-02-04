using System.Net.Http.Headers;
using System.Text.Json;
using DiscordCLI.SerializableTypes;
using DiscordCLI.SerializableTypes.ResponseTypes;


namespace DiscordCLI;

public class NetworkManager(CacheManager cacheManager, string token)
{
    private readonly NetworkClient _networkClient = new NetworkClient(token);

    public async Task<HashSet<Channel>> GetOpenChannels()
    {
        if (!cacheManager.IsActive) return await _networkClient.GetOpenChannels();
        if (cacheManager.HasStoredChannels) return cacheManager.GetCachedChannels();

        var channels = await _networkClient.GetOpenChannels();
        cacheManager.AddChannels(channels);

        return channels;
    }

    private bool ValidateToken()
    {
        if (!cacheManager.IsActive) return _networkClient.ValidateToken();

        switch (cacheManager.GetTokenStatus(token))
        {
            case CacheStatus.Valid:
                return true;
            case CacheStatus.Invalid:
                return false;
            case CacheStatus.Unknown:
                var isValid = _networkClient.ValidateToken();
                cacheManager.AddToken(token, isValid);
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

            if (!ValidateToken())
            {
                throw new ArgumentException("Invalid token.");
            }
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
    }
}