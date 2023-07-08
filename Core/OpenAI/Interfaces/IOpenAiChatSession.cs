namespace ChatGptBotConsole;

/// <summary>
/// openai的chat session
/// </summary>
public interface IOpenAiChatSession : IChatSession
{
    /// <summary>
    /// 重置chat历史
    /// </summary>
    void Reset();
    /// <summary>
    /// chat历史
    /// </summary>
    Queue<OpenAIChatUnit> History { get; }
}