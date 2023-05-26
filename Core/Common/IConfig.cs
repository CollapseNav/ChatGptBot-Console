namespace ChatGptBotConsole;

public interface IConfig<T>
{
    T Data { get; set; }
}