using Collapsenav.Net.Tool;
using EleCho.GoCqHttpSdk.Post;

namespace ChatGptBotConsole;

[Prefix("block", "addblocklist", "addblacklist", "addblock", "addblack")]
public class AddBlackListCmd : AbstractCmd
{
    private readonly AccountListData accountList;

    public AddBlackListCmd(AccountListData accountList)
    {
        Description = "添加黑名单";
        Help = "添加黑名单，会限制全局的bot使用权限，多个账号使用英文逗号','分隔";
        this.accountList = accountList;
    }
    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        var qqmsg = botMsg as QQGroupMsg;
        if (qqmsg == null)
            return true;
        if (qqmsg.AtMsgs.Count <= 1)
            return true;
        accountList.AddToBlackList(qqmsg.AtMsgs.Select(item => item.Target).ToArray());
        return true;
    }
}


public class AccountListData
{
    public List<long> BlackList { get; set; } = new List<long>();
    public List<long> AdminList { get; set; } = new List<long>();
    public List<long> WhiteList { get; set; } = new List<long>();

    public void RemoveBlackList(params long[] ids)
    {
        foreach (var id in ids)
        {
            if (BlackList.Contains(id))
                BlackList.Remove(id);
        }
    }
    public void AddToBlackList(params long[] ids)
    {
        foreach (var id in ids)
        {
            if (id.In(AdminList) || id.In(WhiteList) || BlackList.Contains(id))
                continue;
            BlackList.Add(id);
        }
    }
    public void AddToWhiteList(params long[] ids)
    {
        foreach (var id in ids)
        {
            if (id.In(BlackList))
                BlackList.Remove(id);
            if (!WhiteList.Contains(id))
                WhiteList.Add(id);
        }
    }
    public void AddToAdminList(params long[] ids)
    {
        foreach (var id in ids)
        {
            if (!AdminList.Contains(id))
                AdminList.Add(id);
            if (!WhiteList.Contains(id))
                WhiteList.Add(id);
        }
    }
}

