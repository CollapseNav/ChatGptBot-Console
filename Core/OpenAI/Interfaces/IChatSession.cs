namespace ChatGptBotConsole;
public interface IChatSession
{
    /// <summary>
    /// 设置上下文
    /// </summary>
    void SetContent(string text);
    /// <summary>
    /// 问chatgpt
    /// </summary>
    Task<string> AskAsync(string question);
}