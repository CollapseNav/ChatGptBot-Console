using Collapsenav.Net.Tool;

namespace EasyChatGptBot;
public class BaseChatMiddleware<T> : IMiddleware where T : IChatSessionKey
{
    private readonly ChatSessionManager<T> manager;
    public BaseChatMiddleware(ChatSessionManager<T> manager)
    {
        this.manager = manager;
    }
    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        string content = await manager.GetSessionByBotMsg(botMsg).AskAsync(botMsg.Msg);
        if (content.NotEmpty())
            botMsg.Response(content);
    }
}