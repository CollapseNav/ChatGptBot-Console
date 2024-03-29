namespace ChatGptBotConsole;

public class ChatSessionManager<T> : IChatSessionManager where T : IChatSessionKey
{
    private readonly IConfig<OpenAIConfig> config;
    private readonly IConfig<OpenAIChatConfig> chatConfig;
    private readonly HttpClient client;
    private readonly ObjContainer container;
    protected Dictionary<T, IOpenAiChatSession> Sessions;
    public ChatSessionManager(IConfig<OpenAIConfig> config, IConfig<OpenAIChatConfig> chatConfig, HttpClient client, ObjContainer container)
    {
        Sessions = new();
        this.config = config;
        this.chatConfig = chatConfig;
        this.client = client;
        this.container = container;
    }
    public bool HasSession(T key)
    {
        return Sessions.ContainsKey(key);
    }
    public IChatSession? GetSession(T key)
    {
        if (!HasSession(key))
            Sessions.Add(key, container.GetObj<BaseChatSession>());
        return Sessions[key];
    }
    public IChatSession? GetSessionByBotMsg(IBotMsg botMsg)
    {
        if (botMsg is IBotMsg<T> chatBotMsg)
            return GetSession(chatBotMsg!.From!);
        return null;
    }
}