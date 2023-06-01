namespace ChatGptBotConsole;
[Prefix("移除白名单", "移出白名单")]
public class RemoveWhiteListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;
    public RemoveWhiteListCmd(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg || !accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
            return true;
        if (!accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
        {
            qqmsg.Response("你不是管理员,无法操作白名单");
            return true;
        }
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.Data.RemoveBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}