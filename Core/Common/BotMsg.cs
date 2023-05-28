namespace ChatGptBotConsole;
/// <summary>
/// bot消息
/// </summary>
public abstract class BotMsg : IBotMsg
{
    public string? Msg { get; protected set; }
    public abstract void Response(string content);
    public abstract void Response(MultiResponseData data);
}
public abstract class BotMsg<T> : BotMsg, IBotMsg<T> where T : IChatSessionKey
{
    public T? From { get; protected set; }
}

public abstract class BotMsg<T, E> : BotMsg<T>, IBotMsg<T, E> where T : IChatSessionKey
{
    public E[]? To { get; protected set; }
}