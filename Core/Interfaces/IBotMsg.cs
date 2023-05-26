namespace EasyChatGptBot;

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
}
/// <summary>
/// 包含发送人的消息
/// </summary>
public interface IBotMsg<T> : IBotMsg
{
    T? From { get; }
}

/// <summary>
/// 包含发送人和接收人的消息
/// </summary>
public interface IBotMsg<T, E> : IBotMsg<T>
{
    E[]? To { get; }
}

