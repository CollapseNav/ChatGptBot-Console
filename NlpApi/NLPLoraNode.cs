namespace ChatGptBotConsole;

public class NLPLoraNode : NLPNode
{
    public NLPLoraNode()
    {
    }

    public NLPLoraNode(string[] label, decimal matchWeight, bool enable = true) : base(label, matchWeight, enable)
    {
    }

    public NLPLoraNode(string[] label, decimal matchWeight, string? lora, decimal? loraWeight, LoraType type, string[]? triggerWords = null, LoraAddPosition position = LoraAddPosition.E, bool enable = true) : base(label, matchWeight, enable)
    {
        Lora = lora;
        LoraWeight = loraWeight;
        TriggerWords = triggerWords;
        Position = position;
        Type = type;
    }

    public string? Lora { get; init; }
    public decimal? LoraWeight { get; init; } = 1;
    public string[]? TriggerWords { get; set; }
    public LoraAddPosition Position { get; set; } = LoraAddPosition.E;
    public LoraType Type { get; init; }
}

public enum LoraAddPosition
{
    F, M, E
}

public enum LoraType
{
    Person,
    HairStyle,
    Clothes,
    Style,
    Eye,
}