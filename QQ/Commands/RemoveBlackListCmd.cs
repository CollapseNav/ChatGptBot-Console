namespace ChatGptBotConsole;
[Prefix("移除黑名单", "移出黑名单")]
public class RemoveBlackListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;

    public RemoveBlackListCmd(IConfig<AccountListData> accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg || !accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
            return true;
        if (!accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
        {
            qqmsg.Response("你不是管理员,无法操作黑名单");
            return true;
        }
        if (qqmsg!.AtMsgs!.Count <= 1)
            return true;
        accountList.Data.RemoveBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}