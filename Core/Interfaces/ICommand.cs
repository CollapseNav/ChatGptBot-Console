namespace ChatGptBotConsole;
public interface ICommand
{
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
}