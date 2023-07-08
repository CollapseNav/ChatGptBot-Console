namespace ChatGptBotConsole;

public class NewBingComposeSession : INewBingSession
{
    private readonly NewBingConfig config;
    private readonly HttpClient client;
    private string AiContent = string.Empty;
    public NewBingComposeSession(IConfig<NewBingConfig> config, HttpClient client)
    {
        this.config = config.Data;
        this.client = client;
    }

    public async Task<string> AskAsync(string question)
    {
        var builder = new NewBingComposeBuilder(client);
        return string.Empty;
    }

    public void SetContent(string text)
    {
        AiContent = text;
    }
}