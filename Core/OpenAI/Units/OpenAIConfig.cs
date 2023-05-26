namespace EasyChatGptBot;

public class OpenAIConfig : IStartInit
{
    public string ApiKey { get; set; }
    public string GptModel { get; set; } = "gpt-3.5-turbo";
    public decimal Temperature { get; set; } = 0.5m;
    public int MaxLen { get; set; } = 2048;
    public string ChatApiUrl { get; set; } = "https://api.openai.com/v1/chat/completions";
    public void InitConfig()
    {
    }
}