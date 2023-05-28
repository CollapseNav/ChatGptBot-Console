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
        botMsg.Response(accountList.Data.BlackList.Join("\n"));
        return true;
    }
}