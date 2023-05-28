namespace ChatGptBotConsole;

[Prefix("添加白名单", "加白名单")]
public class AddWhiteListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;

    public AddWhiteListCmd(IConfig<AccountListData> accountList)
    {
        Description = "添加白名单";
        Help = "添加白名单，在使用白名单中间件时才有效，多个账号使用英文逗号','分隔";
        this.accountList = accountList;
    }
    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg)
            return true;
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.Data.AddToWhiteList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}
