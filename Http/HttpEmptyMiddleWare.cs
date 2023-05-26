using Collapsenav.Net.Tool;

namespace EasyChatGptBot;

public class HttpEmptyMiddleware : IMiddleware
{
    public HttpEmptyMiddleware()
    {
    }

    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        var msg = botMsg as HttpMsg;
        Console.WriteLine(msg.ToJson());
        msg.Response(DateTime.Now.ToDefaultMilliString());
        await next();
    }
}