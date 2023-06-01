using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

[Prefix("列出白名单")]
public class ShowWhiteListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;

    public ShowWhiteListCmd(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (accountList.Data.WhiteList.IsEmpty())
        {
            botMsg.Response("当前系统内没有白名单");
            return true;
        }
        var qqmsg = botMsg as QQGroupMsg;
        if (!accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
        {
            qqmsg.Response("你不是管理员,无法操作白名单");
            return true;
        }
        botMsg.Response(accountList.Data.WhiteList.Join("\n"));
        return true;
    }
}