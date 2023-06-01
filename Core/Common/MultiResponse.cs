namespace ChatGptBotConsole;

public class MultiResponseData
{
    public string? Msg { get; set; }
    public IEnumerable<Stream>? Images { get; set; }
    public IEnumerable<Stream>? Sounds { get; set; }
    public IEnumerable<string>? Links { get; set; }
    public bool IsAt { get; set; } = false;
}