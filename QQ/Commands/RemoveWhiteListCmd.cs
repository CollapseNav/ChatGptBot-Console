namespace ChatGptBotConsole;
[Prefix("移除白名单", "移出白名单")]
public class RemoveWhiteListCmd : ClassificationCmd
{
    private readonly AccountListData accountList;
    public RemoveWhiteListCmd(AccountListData accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg)
            return true;
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.RemoveBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}