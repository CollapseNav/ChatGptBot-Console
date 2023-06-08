using System.Data;
using System.Text;
using Collapsenav.Net.Tool;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.Post;
namespace ChatGptBotConsole;

/// <summary>
/// qq群at消息
/// </summary>
public class QQGroupMsg : QQMsg<CqGroupMessagePostContext>, IBotMsg<QQGroupUser, QQGroupUser>
{
    public List<CqAtMsg>? AtMsgs { get; protected set; }
    /// <summary>
    /// 群号
    /// </summary>
    public long? GroupId { get; set; }
    public QQGroupUser[]? To { get; protected set; }
    public QQGroupUser? From { get; protected set; }
    public IConfig<AccountListData> AccountList { get; }

    public QQGroupMsg(CqWsSession session) : base(session) { }
    public QQGroupMsg(CqGroupMessagePostContext context, CqWsSession session, HttpClient client, IConfig<NlpConfig> nlpconfig, IConfig<AccountListData> accountList) : base(context, session, client, nlpconfig)
    {
        AccountList = accountList;
        InitMsg(this, context);
    }

    /// <summary>
    /// 通过context创建消息
    /// </summary>
    public static QQGroupMsg? CreateMsg(CqGroupMessagePostContext context, CqWsSession session)
    {
        return InitMsg(new QQGroupMsg(session), context);
    }
    /// <summary>
    /// 通过context初始化群at消息
    /// </summary>
    public static QQGroupMsg? InitMsg(QQGroupMsg botMsg, CqGroupMessagePostContext context)
    {
        if (botMsg == null || context == null)
            throw new NoNullAllowedException();
        botMsg.InitByMsgContext(context);
        if ((botMsg.Audio != null && botMsg.AccountList.Data.AdminList.Contains(botMsg.From.UserId.Value)) || botMsg!.To!.Any(item => item.UserId == context.SelfId))
            return botMsg;
        botMsg.Msg = null;
        return null;
    }

    public override void Response(string data)
    {
        if (!GroupId.HasValue)
            throw new Exception();
        var msg = new CqMessage { };
        if (From != null && From.UserId.HasValue)
            msg.Add(new CqAtMsg(From.UserId.Value));
        msg.Add(new CqTextMsg(data));
        session.SendGroupMessageAsync(GroupId.Value, msg).Wait();
    }

    public void InitByMsgContext(CqGroupMessagePostContext context)
    {
        base.InitByMsgContext(context);
        var msg = context.Message;
        StringBuilder sb = new();
        foreach (var m in msg)
        {
            if (m is CqTextMsg mt)
            {
                sb.Append(mt.Text);
                continue;
            }
            if (m is CqAtMsg mat && mat.Target != context.SelfId)
            {
                sb.Append($" {mat.Target}");
                continue;
            }
        }
        Msg += sb.ToString();
        GroupId = context.GroupId;
        From = new QQGroupUser(context.UserId, context?.Sender?.Nickname, context!.GroupId); ;
        var atmsg = msg.Where(item => item is CqAtMsg).ToList();
        AtMsgs = atmsg.Select(item => (item as CqAtMsg)!).ToList();
        To = AtMsgs.Select(item => new QQGroupUser(item.Target, item.Name, context.GroupId)).ToArray();
    }

    public override void Response(MultiResponseData data)
    {
        var msg = new CqMessage { };
        if (data.IsAt && From != null && From.UserId.HasValue)
            msg.Add(new CqAtMsg(From.UserId.Value));
        if (data.Msg.NotEmpty())
            msg.Add(new CqTextMsg(data!.Msg!));
        if (data.Images.NotEmpty())
        {
            foreach (var image in data!.Images!)
            {
                var guid = Guid.NewGuid().ToString();
                var filename = $"{guid}.png";
                image.SaveTo($"G:\\go-cqhttp_windows_amd64\\data\\images\\{filename}");
                msg.Add(new CqImageMsg(filename));
            }
        }
        session.SendGroupMessage(GroupId!.Value, msg);
    }
}