namespace ChatGptBotConsole;

public class NLPNode
{
    public NLPNode()
    {
    }

    public NLPNode(string label, decimal matchWeight, bool enable = true)
    {
        Label = label;
        MatchWeight = matchWeight;
        Enable = enable;
    }

    public string Label { get; init; }
    public decimal MatchWeight { get; init; }
    public bool Enable { get; init; } = true;
}