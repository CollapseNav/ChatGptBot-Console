namespace EasyChatGptBot;

public interface IBotApplication
{
    /// <summary>
    /// 使用中间件
    /// </summary>
    IBotApplication Use<T>() where T : class, IMiddleware;
    /// <summary>
    /// 使用中间件
    /// </summary>
    IBotApplication Use(Func<IBotMsg, Func<Task>, Task> middleware);
    /// <summary>
    /// 添加消息
    /// </summary>
    void AddMsg(IBotMsg msg);
    /// <summary>
    /// 获取消息
    /// </summary>
    IBotMsg GetMsg();
    /// <summary>
    /// 获取消息
    /// </summary>
    Task<IBotMsg> GetMsgAsync();
    Task RunAsync();
}