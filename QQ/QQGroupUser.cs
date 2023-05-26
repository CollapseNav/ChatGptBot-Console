namespace EasyChatGptBot;

public class QQGroupUser : QQSimpleUser
{
    public QQGroupUser(long userId, string? name, long? groupId) : base(userId, name)
    {
        GroupId = groupId;
    }

    public long? GroupId { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is QQGroupUser user &&
               base.Equals(obj) &&
               UserId == user.UserId &&
               Name == user.Name &&
               GroupId == user.GroupId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), UserId, Name, GroupId);
    }
}