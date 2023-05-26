namespace EasyChatGptBot;

/// <summary>
/// qq用户
/// </summary>
public class QQSimpleUser : IChatSessionKey
{
    public QQSimpleUser() { }

    public QQSimpleUser(long userId, string? name)
    {
        UserId = userId;
        Name = name;
    }

    /// <summary>
    /// 用户qq号
    /// </summary>
    public long? UserId { get; set; }
    /// <summary>
    /// 用户名称
    /// </summary>
    public string? Name { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is QQSimpleUser user &&
               UserId == user.UserId &&
               Name == user.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Name);
    }
}