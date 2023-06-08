namespace ChatGptBotConsole;

public class OpenAIConfig : IStartInit
{
    public string? ApiKey { get; set; }
    public string GptModel { get; set; } = "gpt-3.5-turbo";
    public decimal Temperature { get; set; } = 0.5m;
    public decimal Top_P { get; set; } = 0.5m;
    public decimal Frequency_penalty { get; set; } = 0;
    public decimal Presence_penalty { get; set; } = 0;
    public int MaxLen { get; set; } = 2048;
    public string TextApiUrl { get; set; } = "http://127.0.0.1:8000/v1/completions";
    public string ChatApiUrl { get; set; } = "https://api.openai.com/v1/chat/completions";
    public void InitConfig()
    {
    }
}