namespace EasyChatGptBot;

public interface IMiddleware
{
    Task Invoke(IBotMsg botMsg, Func<Task> next);
}