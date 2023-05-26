namespace EasyChatGptBot;

public class ChatSessionManager<T> where T : IChatSessionKey
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

    public IOpenAiChatSession GetSession(T key)
    {
        if (!HasSession(key))
            Sessions.Add(key, container.GetObj<BaseChatSession>());
        // Sessions.Add(key, new BaseChatSession(config.Data, chatConfig.Data, client));
        return Sessions[key];
    }

    public IOpenAiChatSession GetSessionByBotMsg(IBotMsg botMsg)
    {
        if (botMsg is IBotMsg<T> chatBotMsg)
            return GetSession(chatBotMsg.From);
        throw new Exception("错误的对象结构");
    }
}