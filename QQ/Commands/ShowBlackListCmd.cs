using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

[Prefix("showblacklist", "showblocklist")]
public class ShowBlackListCmd : AbstractCmd
{
    private readonly AccountListData accountList;

    public ShowBlackListCmd(AccountListData accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (accountList.BlackList.IsEmpty())
        {
            botMsg.Response("当前系统内没有黑名单");
            return true;
        }
        botMsg.Response(accountList.BlackList.Join("\n"));
        return true;
    }
}