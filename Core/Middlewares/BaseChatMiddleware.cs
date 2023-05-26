using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;
public class BaseChatMiddleware<T> : IMiddleware where T : IChatSessionKey
{
    private readonly ChatSessionManager<T> manager;
    public BaseChatMiddleware(ChatSessionManager<T> manager)
    {
        this.manager = manager;
    }
    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        var session = manager.GetSessionByBotMsg(botMsg);
        if (session == null)
            await next();
        else
        {
            string content = await session.AskAsync(botMsg.Msg);
            if (content.NotEmpty())
                botMsg.Response(content);
        }
    }
}