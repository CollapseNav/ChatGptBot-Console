namespace ChatGptBotConsole;
[Prefix("移除黑名单", "移出黑名单")]
public class RemoveBlackListCmd : ClassificationCmd
{
    private readonly AccountListData accountList;

    public RemoveBlackListCmd(AccountListData accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg)
            return true;
        if (qqmsg!.AtMsgs!.Count <= 1)
            return true;
        accountList.RemoveBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}