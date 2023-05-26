using System.Data;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.Post;
namespace EasyChatGptBot;

/// <summary>
/// qq群at消息
/// </summary>
public class QQGroupMsg : BotMsg<QQGroupUser, QQGroupUser>
{
    /// <summary>
    /// 群号
    /// </summary>
    public long? GroupId { get; set; }
    private readonly CqWsSession session;
    public QQGroupMsg(CqWsSession session)
    {
        this.session = session;
    }
    public QQGroupMsg(CqGroupMessagePostContext context, CqWsSession session)
    {
        this.session = session;
        InitMsg(this, context);
    }
    public QQGroupMsg(CqGroupMessagePostContext context)
    {
        InitMsg(this, context);
    }

    public QQGroupMsg() { }

    /// <summary>
    /// 通过context创建消息
    /// </summary>
    public static QQGroupMsg CreateMsg(CqGroupMessagePostContext context, CqWsSession session)
    {
        return InitMsg(new QQGroupMsg(session), context);
    }
    /// <summary>
    /// 通过context初始化群at消息
    /// </summary>
    public static QQGroupMsg InitMsg(QQGroupMsg botMsg, CqGroupMessagePostContext context)
    {
        if (botMsg == null || context == null)
            throw new NoNullAllowedException();
        botMsg.InitByMsgContext(context);
        if (botMsg.To.Any(item => item.UserId == context.SelfId))
            return botMsg;
        return null;
    }

    public override void Response(string data)
    {
        if (!GroupId.HasValue)
            throw new Exception();
        session.SendGroupMessageAsync(GroupId.Value, new CqMessage{
            new CqTextMsg(data)
        }).Wait();
    }

    public void InitByMsgContext(CqGroupMessagePostContext context)
    {
        var msg = context.Message;
        Msg = context.Message.Text;
        From = new QQGroupUser(context.UserId, context?.Sender?.Nickname, context.GroupId); ;
        var atmsg = msg.Where(item => item is CqAtMsg).ToList();
        To = atmsg.Select(item =>
        {
            var at = (item as CqAtMsg)!;
            return new QQGroupUser(at.Target, at.Name, context.GroupId);
        }).ToArray();
    }
}