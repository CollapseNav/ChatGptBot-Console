using System.Text.Json.Nodes;

namespace EasyChatGptBot;

public class BotApplicationBuilder
{
    public List<Action<BotApplication, ObjContainer>> Actions { get; set; }
    public SimpleJsonConfiguration Configuration { get; set; }
    public ObjContainer Container { get; set; }
    public BotApplicationBuilder()
    {
        Container = new ObjContainer();
        Actions = new();
    }

    public BotApplicationBuilder AddAction(Action<IBotApplication, ObjContainer> action)
    {
        Actions.Add(action);
        return this;
    }
    /// <summary>
    /// 类似单例注册
    /// </summary>
    public BotApplicationBuilder Add<T>(T obj)
    {
        Container.AddOrUpdate(obj);
        return this;
    }

    /// <summary>
    /// 注册类型
    /// </summary>
    public BotApplicationBuilder AddType<T>()
    {
        Container.AddType<T>();
        return this;
    }
    /// <summary>
    /// 构建 botapplication
    /// </summary>
    public BotApplication Build()
    {
        Container.AddOrUpdate(Container);
        var botapplication = Container.GetObj<BotApplication>();
        foreach (var action in Actions)
            action(botapplication, Container);
        return botapplication;
    }
}