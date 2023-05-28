namespace ChatGptBotConsole;

/// <summary>
/// 角色上下文
/// </summary>
public class AiContent
{
    public Dictionary<string, string>? DefaultPrompt { get; set; }

    public string GetPrompt(string key)
    {
        if (DefaultPrompt == null)
            throw new Exception();
        if (!DefaultPrompt.ContainsKey(key))
            return key;
        return DefaultPrompt[key];
    }
}