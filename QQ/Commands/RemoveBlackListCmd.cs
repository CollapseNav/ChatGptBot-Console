namespace ChatGptBotConsole;
[Prefix("removeblacklist", "removeblocklist")]
public class RemoveBlackListCmd : AbstractCmd
{
    private readonly AccountListData accountList;

    public RemoveBlackListCmd(AccountListData accountList)
    {
        this.accountList = accountList;
    }

    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        var qqmsg = botMsg as QQGroupMsg;
        if (qqmsg == null)
            return true;
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.RemoveBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}