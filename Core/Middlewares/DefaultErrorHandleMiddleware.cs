namespace EasyChatGptBot;

/// <summary>
/// 一个简单的错误处理中间件，只是简单的输出异常
/// </summary>
public class DefaultErrorHandleMiddleware : IMiddleware
{
    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            botMsg.Response(ex.Message);
        }
    }
}