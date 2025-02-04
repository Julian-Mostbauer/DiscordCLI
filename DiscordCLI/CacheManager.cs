using System.Text.Json;

namespace DiscordCLI.SerializableTypes;

public enum CacheStatus
{
    Valid,
    Invalid,
    Unknown
}

public class CacheManager
{
    private CacheSettings Settings { get; set; }
    private string Location { get; set; }

    private CacheData? Data { get; set; }

    public bool IsActive => Settings.Active;

    public CacheManager(CacheSettings settings, string location)
    {
        Settings = settings;
        Location = location;

        if (!File.Exists(Location))
        {
            File.Create(Location).Close();
        }

        Restore();
    }


    private void Restore()
    {
        var content = File.ReadAllText(Location);

        if (content.Length != 0)
        {
            var loadedData = CacheData.FromJson(content);
            Data = loadedData;
        }
        else
        {
            Data = new CacheData();
        }
    }

    public void AddToken(string token, bool valid)
    {
        if (valid)
        {
            Data!.ValidTokens.Add(token);
        }
        else
        {
            Data!.InvalidTokens.Add(token);
        }
    }

    public void ClearCache()
    {
        Data!.Clear();
    }

    public void Save()
    {
        var content = JsonSerializer.Serialize(Data!, JsonContext.Default.CacheData);
        File.WriteAllText(Location, content);
    }

    public CacheStatus GetTokenStatus(string authorizationParameter)
    {
        if (Data!.ValidTokens.Contains(authorizationParameter)) return CacheStatus.Valid;
        if (Data!.InvalidTokens.Contains(authorizationParameter)) return CacheStatus.Invalid;
        return CacheStatus.Unknown;
    }
}