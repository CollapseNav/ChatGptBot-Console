using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

[Prefix("列出黑名单")]
public class ShowBlackListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;

    public ShowBlackListCmd(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (accountList.Data.BlackList.IsEmpty())
        {
            botMsg.Response("当前系统内没有黑名单");
            return true;
        }
        var qqmsg = botMsg as QQGroupMsg;
        if (!accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
        {
            qqmsg.Response("你不是管理员,无法操作黑名单");
            return true;
        }
        botMsg.Response(accountList.Data.BlackList.Join("\n"));
        return true;
    }
}