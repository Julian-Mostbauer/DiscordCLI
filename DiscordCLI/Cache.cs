namespace DiscordCLI;

public enum CacheStatus
{
    Valid,
    Invalid,
    Unknown
}

public class Cache
{
    private CacheSettings Settings { get; set; }
    private HashSet<string> ValidTokens { get; set; } = new();
    private HashSet<string> InvalidTokens { get; set; } = new();
    private string Location { get; set; }

    private const char Separator = ';';

    public bool IsActive => Settings.Active;

    public Cache(CacheSettings settings, string location)
    {
        Settings = settings;
        Location = location;

        Validate();
        Restore();
    }

    private void Validate()
    {
        if (!File.Exists(Location)) throw new ArgumentException("Cache file does not exist.");
        if (Settings.Limit < 0) throw new ArgumentException("Cache limit must be non-negative.");
    }

    private void Restore()
    {
        var content = File.ReadAllLines(Location);
        foreach (var line in content)
        {
            if (line.StartsWith("VALID:"))
            {
                ValidTokens = line[6..]
                    .Split(Separator, StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();
            }
            else if (line.StartsWith("INVALID:"))
            {
                InvalidTokens = line[8..]
                    .Split(Separator, StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();
            }
            else
            {
                throw new ArgumentException("Invalid cache file.");
            }
        }
    }

    public void AddToken(string token, bool valid)
    {
        if (valid)
        {
            ValidTokens.Add(token);
        }
        else
        {
            InvalidTokens.Add(token);
        }
    }

    public void ClearCache()
    {
        ValidTokens.Clear();
        InvalidTokens.Clear();
    }

    public void Save()
    {
        var content = new List<string>
        {
            $"VALID:{string.Join(Separator, ValidTokens)}",
            $"INVALID:{string.Join(Separator, InvalidTokens)}"
        };
        
        File.WriteAllLines(Location, content);
    }

    public CacheStatus GetTokenStatus(string authorizationParameter)
    {
        if (ValidTokens.Contains(authorizationParameter)) return CacheStatus.Valid;
        if (InvalidTokens.Contains(authorizationParameter)) return CacheStatus.Invalid;
        return CacheStatus.Unknown;
    }
}