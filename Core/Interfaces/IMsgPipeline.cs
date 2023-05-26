namespace EasyChatGptBot;

public interface IMsgPipeline
{
    /// <summary>
    /// 添加消息
    /// </summary>
    void AddMsg(IBotMsg botMsg);
    /// <summary>
    /// 获取消息
    /// </summary>
    Task<IBotMsg> GetMsgAsync();
    /// <summary>
    /// 获取消息
    /// </summary>
    IBotMsg GetMsg();
    /// <summary>
    /// 清空消息队列
    /// </summary>
    void Clear();
}