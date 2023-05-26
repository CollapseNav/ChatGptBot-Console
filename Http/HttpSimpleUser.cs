namespace EasyChatGptBot;

public class HttpSimpleUser : IChatSessionKey
{
    public HttpSimpleUser()
    {
    }

    public HttpSimpleUser(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is HttpSimpleUser user &&
               Name == user.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}