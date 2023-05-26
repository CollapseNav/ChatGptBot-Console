namespace EasyChatGptBot;

public interface IConfig<T>
{
    T Data { get; set; }
}