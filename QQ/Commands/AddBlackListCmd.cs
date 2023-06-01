namespace ChatGptBotConsole;

[Prefix("加黑名单", "拉入黑名单")]
public class AddBlackListCmd : ClassificationCmd
{
    private readonly IConfig<AccountListData> accountList;

    public AddBlackListCmd(IConfig<AccountListData> accountList)
    {
        Description = "添加黑名单";
        Help = "添加黑名单，会限制全局的bot使用权限，多个账号使用英文逗号','分隔";
        this.accountList = accountList;
    }
    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        if (botMsg is not QQGroupMsg qqmsg)
            return true;
        if (!accountList.Data.AdminList.Contains(qqmsg.From.UserId.Value))
        {
            qqmsg.Response("你不是管理员，无法操作黑名单");
            return true;
        }
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.Data.AddToBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}
