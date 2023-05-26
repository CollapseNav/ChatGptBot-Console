using System.Diagnostics;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Post;

namespace ChatGptBotConsole;

public abstract class QQMsg<MsgContext> : BotMsg where MsgContext : CqMessagePostContext
{
    protected readonly MsgContext context;
    protected readonly CqWsSession session;
    public QQMsg(CqWsSession session)
    {
        this.session = session;
    }
    public QQMsg(MsgContext context, CqWsSession session)
    {
        this.context = context;
        this.session = session;
    }
}