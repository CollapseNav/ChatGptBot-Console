namespace EasyChatGptBot;

/// <summary>
/// openai的chat session
/// </summary>
public interface IOpenAiChatSession
{
    /// <summary>
    /// 设置上下文
    /// </summary>
    void SetContent(string text);
    /// <summary>
    /// 重置chat历史
    /// </summary>
    void Reset();
    /// <summary>
    /// chat历史
    /// </summary>
    Queue<OpenAIChatUnit> History { get; }
    /// <summary>
    /// 问chatgpt
    /// </summary>
    Task<string> AskAsync(string question);
}