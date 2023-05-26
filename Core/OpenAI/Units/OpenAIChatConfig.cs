namespace EasyChatGptBot;

public class OpenAIChatConfig : IStartInit
{
    public int LimitTime { get; set; } = 300;
    public int LimitCount { get; set; } = 5;
    public int MaxHistory { get; set; } = 20;
    public string DefaultContent { get; set; }
    public void InitConfig()
    {
    }
}