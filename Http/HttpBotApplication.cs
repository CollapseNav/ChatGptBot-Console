namespace EasyChatGptBot;
public class HttpBotApplication : BotApplication
{
    public HttpBotApplication(IMsgPipeline pipeline, ObjContainer container) : base(pipeline, container)
    {
    }

    public override async Task RunAsync()
    {
        while (true)
        {
            var msg = await pipeline.GetMsgAsync() as HttpMsg;
            await ExecMiddleware(msg).Invoke();
        }
    }
}