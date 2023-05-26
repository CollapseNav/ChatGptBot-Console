namespace EasyChatGptBot;

public class OpenAiChatResult
{
    public string Id { get; set; } = string.Empty;
    public string @Object { get; set; } = string.Empty;
    public int Created { get; set; }
    public OpenAiChatResultChoice[]? Choices { get; set; }
    public OpenAiChatResultUsage? Usage { get; set; }
    public OpenAiChatResultError? Error { get; set; }
}
public class OpenAiChatResultError
{
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
public class OpenAiChatResultUsage
{
    public int Prompt_tokens { get; set; }
    public int Completion_tokens { get; set; }
    public int Total_tokens { get; set; }
}
public class OpenAiChatResultChoice
{
    public int Index { get; set; }
    public OpenAIChatUnit? Message { get; set; }
    public string Finish_reason { get; set; } = string.Empty;
}