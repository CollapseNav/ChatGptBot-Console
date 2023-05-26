namespace EasyChatGptBot;
public interface ICommand
{
    /// <summary>
    /// 前缀
    /// </summary>
    string Prefix { get; set; }
    /// <summary>
    /// 简单描述
    /// </summary>
    string Description { get; }
    /// <summary>
    /// 帮助信息
    /// </summary>
    string Help { get; }
    /// <summary>
    /// 尝试执行命令
    /// </summary>
    Task<bool> ExecAsync(IBotMsg botMsg);
    /// <summary>
    /// 检查消息能否匹配到前缀
    /// </summary>
    bool CheckPrefix(string msg);
}