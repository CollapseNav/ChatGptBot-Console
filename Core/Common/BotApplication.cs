namespace EasyChatGptBot;

public abstract class BotApplication : IBotApplication
{
    /// <summary>
    /// 中间件集合
    /// </summary>
    protected List<Func<IBotMsg, Func<Task>, Task>> MiddleWares;
    /// <summary>
    /// 消息管道
    /// </summary>
    protected readonly IMsgPipeline pipeline;
    protected readonly ObjContainer container;

    protected BotApplication(IMsgPipeline pipeline, ObjContainer container)
    {
        MiddleWares = new();
        this.pipeline = pipeline;
        this.container = container;
    }

    /// <summary>
    /// 创建builder
    /// </summary>
    public static BotApplicationBuilder CreateBuilder()
    {
        return new BotApplicationBuilder();
    }

    /// <summary>
    /// 添加消息
    /// </summary>
    public virtual void AddMsg(IBotMsg msg)
    {
        pipeline.AddMsg(msg);
    }

    /// <summary>
    /// 获取消息
    /// </summary>
    public virtual IBotMsg GetMsg()
    {
        return pipeline.GetMsg();
    }
    /// <summary>
    /// 获取消息
    /// </summary>
    public virtual async Task<IBotMsg> GetMsgAsync()
    {
        return await pipeline.GetMsgAsync();
    }

    /// <summary>
    /// TODO 添加中间件
    /// </summary>
    public IBotApplication Use(Func<IBotMsg, Func<Task>, Task> middleware)
    {
        MiddleWares.Add(middleware);
        return this;
    }

    public IBotApplication Use<T>() where T : class, IMiddleware
    {
        container.AddType<T>();
        var middleware = container.GetObj<T>();
        MiddleWares.Add((botMsg, next) =>
        {
            return middleware.Invoke(botMsg, next);
        });
        return this;
    }
    public abstract Task RunAsync();

    protected Func<Task> ExecMiddleware(IBotMsg botMsg, int index = 0, CancellationToken? cancellationToken = null)
    {
        if ((cancellationToken?.IsCancellationRequested ?? false) || index == MiddleWares.Count)
            return () => Task.CompletedTask;
        return () => MiddleWares[index](botMsg, ExecMiddleware(botMsg, index + 1, cancellationToken));
    }
}