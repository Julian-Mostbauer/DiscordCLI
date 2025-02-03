namespace DiscordCLI.SerializableTypes;

public interface IFromJsonAble<out T>
{
    public static abstract T FromJson(string json);
    public static abstract T[] ManyFromJson(string json);
}