namespace EasyChatGptBot;
public class QQBotApplication : BotApplication
{
    public QQBotApplication(IMsgPipeline pipeline, ObjContainer container) : base(pipeline, container)
    {
    }

    public override async Task RunAsync()
    {
        while (true)
        {
            var msg = await pipeline.GetMsgAsync() as QQGroupMsg;
            await ExecMiddleware(msg).Invoke();
        }
    }
}