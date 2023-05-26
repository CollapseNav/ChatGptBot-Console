using System.Data;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.Post;
namespace ChatGptBotConsole;

/// <summary>
/// qq群at消息
/// </summary>
public class QQPrivateMsg : QQMsg<CqPrivateMessagePostContext>, IBotMsg<QQSimpleUser, QQSimpleUser>
{
    /// <summary>
    /// 群号
    /// </summary>
    public long? GroupId { get; set; }
    public QQSimpleUser[]? To { get; protected set; }
    public QQSimpleUser? From { get; protected set; }
    public QQPrivateMsg(CqWsSession session) : base(session) { }
    public QQPrivateMsg(CqPrivateMessagePostContext context, CqWsSession session) : base(context, session)
    {
        InitMsg(this, context);
    }

    /// <summary>
    /// 通过context创建消息
    /// </summary>
    public static QQPrivateMsg CreateMsg(CqPrivateMessagePostContext context, CqWsSession session)
    {
        return InitMsg(new QQPrivateMsg(session), context);
    }
    /// <summary>
    /// 通过context初始化群at消息
    /// </summary>
    public static QQPrivateMsg InitMsg(QQPrivateMsg botMsg, CqPrivateMessagePostContext context)
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
        session.SendPrivateMessageAsync(From.UserId.Value, new CqMessage { new CqTextMsg(data) }).Wait();
    }

    public void InitByMsgContext(CqPrivateMessagePostContext context)
    {
        var msg = context.Message;
        Msg = context.Message.Text;
        From = new QQSimpleUser(context.UserId, context?.Sender?.Nickname); ;
        var atmsg = msg.Where(item => item is CqAtMsg).ToList();
        To = new[] { new QQSimpleUser(context.SelfId, "SELF") };
    }
}