namespace ChatGptBotConsole;

public class NlpPostModel
{
    public string? Content { get; set; }
    public string[]? Labels { get; set; }
    public bool Multi { get; set; } = false;
}