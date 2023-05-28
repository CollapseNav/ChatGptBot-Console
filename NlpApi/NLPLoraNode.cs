namespace ChatGptBotConsole;

public class NLPLoraNode
{
    public string? Lora { get; set; }
    public decimal? MatchWeight { get; set; }
    public decimal? LoraWeight { get; set; }
    public string[]? TriggerWords { get; set; }
    public LoraAddPosition Position { get; set; } = LoraAddPosition.E;
}

public enum LoraAddPosition
{
    F, M, E
}