namespace ChatGptBotConsole;

/// <summary>
/// 基本消息
/// </summary>
public interface IBotMsg
{
    /// <summary>
    /// 消息
    /// </summary>
    string? Msg { get; }
    void Response(string content);
    void Response(MultiResponseData data);
}
/// <summary>
/// 包含发送人的消息
/// </summary>
public interface IBotMsg<out T> : IBotMsg where T : IChatSessionKey
{
    T? From { get; }
}

/// <summary>
/// 包含发送人和接收人的消息
/// </summary>
public interface IBotMsg<out T, E> : IBotMsg<T> where T : IChatSessionKey
{
    E[]? To { get; }
}

