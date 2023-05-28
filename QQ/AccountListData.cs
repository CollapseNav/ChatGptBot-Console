using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;
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
    public void RemoveWhiteList(params long[] ids)
    {
        foreach (var id in ids)
        {
            if (WhiteList.Contains(id))
                WhiteList.Remove(id);
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

