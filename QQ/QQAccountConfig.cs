namespace ChatGptBotConsole;

public class QQAccountConfig
{
    /// <summary>
    /// 白名单
    /// </summary>
    public long[] AllowList { get; set; } = Array.Empty<long>();
    /// <summary>
    /// 黑名单
    /// </summary>
    public long[] BlockList { get; set; } = Array.Empty<long>();
    /// <summary>
    /// 管理员名单
    /// </summary>
    public long[] AdminList { get; set; } = Array.Empty<long>();
}