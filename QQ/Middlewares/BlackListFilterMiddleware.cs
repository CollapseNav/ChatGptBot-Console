using Collapsenav.Net.Tool;
using EleCho.GoCqHttpSdk.Post;

namespace ChatGptBotConsole;

public class BlackListFilterMiddleware : IMiddleware
{
    private readonly IConfig<AccountListData> accountList;

    public BlackListFilterMiddleware(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        if (botMsg is not IBotMsg<QQSimpleUser> qqmsg)
            return;
        if (qqmsg!.From!.UserId!.Value.In(accountList.Data.BlackList))
        {
            botMsg.Response("你已被拉入黑名单");
            return;
        }
        await next();
    }
}