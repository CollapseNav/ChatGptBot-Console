using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public class WhiteListFilterMiddleware : IMiddleware
{
    private readonly IConfig<AccountListData> accountList;

    public WhiteListFilterMiddleware(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        var qqmsg = botMsg as IBotMsg<QQSimpleUser>;
        if (qqmsg == null)
            return;
        if (!qqmsg!.From!.UserId!.Value.In(accountList.Data.WhiteList))
        {
            botMsg.Response("你不在白名单内");
            return;
        }
        await next();
    }
}