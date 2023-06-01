namespace ChatGptBotConsole;

public class NLPNode
{
    public NLPNode()
    {
    }

    public NLPNode(string[] label, decimal matchWeight, bool enable = true)
    {
        Labels = label;
        MatchWeight = matchWeight;
        Enable = enable;
    }

    public string[] Labels { get; init; }
    public decimal MatchWeight { get; init; }
    public bool Enable { get; init; } = true;
}