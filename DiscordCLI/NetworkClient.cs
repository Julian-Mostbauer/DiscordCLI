using System.Net.Http.Headers;
using System.Text.Json;
using DiscordCLI.SerializableTypes;
using DiscordCLI.SerializableTypes.ResponseTypes;


namespace DiscordCLI;

public class NetworkClient
{
    private readonly HttpClient _sharedClient;
    private readonly Cache _cache;
    private readonly string _token;

    public NetworkClient(string userToken, Cache cache)
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

        _cache = cache;

        if (!ValidateTokenTryCache())
        {
            throw new ArgumentException("Invalid token.");
        }
    }

    private bool ValidateTokenTryCache()
    {
        if (!_cache.IsActive) return ValidateToken();

        switch (_cache.GetTokenStatus(_token))
        {
            case CacheStatus.Valid:
                return true;
            case CacheStatus.Invalid:
                return false;
            case CacheStatus.Unknown:
                var isValid = ValidateToken();
                _cache.AddToken(_token, isValid);
                return isValid;
            default:
                throw new InvalidOperationException("Unknown cache status.");
        }
    }

    private bool ValidateToken()
    {
        return _sharedClient.GetAsync("users/@me").Result.IsSuccessStatusCode;
    }


    public async Task<Channel[]> GetOpenChannels()
    {
        var response = await _sharedClient.GetAsync("users/@me/channels");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
        return Channel.ManyFromJson(jsonResponse);
    }
}