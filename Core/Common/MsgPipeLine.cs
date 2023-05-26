using System.Collections.Concurrent;

namespace EasyChatGptBot;

/// <summary>
/// 消息管道
/// </summary>
public class MsgPipeLine : IMsgPipeline
{
    /// <summary>
    /// bot接收消息的队列
    /// </summary>
    private ConcurrentQueue<IBotMsg> MsgQueue;
    public MsgPipeLine()
    {
        MsgQueue = new();
    }
    public virtual void AddMsg(IBotMsg botMsg) => MsgQueue.Enqueue(botMsg);
    public virtual void Clear() => MsgQueue.Clear();
    public virtual IBotMsg GetMsg()
    {
        if (MsgQueue.TryDequeue(out var msg))
            return msg;
        else
        {
            do
            {
                Thread.Sleep(100);
                if (MsgQueue.TryDequeue(out msg))
                    return msg;
            } while (true);
        }
    }
    public virtual async Task<IBotMsg> GetMsgAsync()
    {
        if (MsgQueue.TryDequeue(out var msg))
            return msg;
        else
        {
            do
            {
                await Task.Delay(100);
                if (MsgQueue.TryDequeue(out msg))
                    return msg;
            } while (true);
        }
    }
}
