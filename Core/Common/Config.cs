namespace EasyChatGptBot;

public class Config<T> : IConfig<T>
{
    public Config(SimpleJsonConfiguration configuration, string nodePath)
    {
        Data = configuration.Get<T>(nodePath);
    }
    public T Data { get; set; }
}