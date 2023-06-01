namespace ChatGptBotConsole;

public class NlpPostModel
{
    public string? Content { get; set; }
    public string[]? Labels { get; set; } = Array.Empty<string>();
    public bool Multi { get; set; } = false;
}