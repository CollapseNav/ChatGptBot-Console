namespace ChatGptBotConsole;
public class HttpBotApplication : BotApplication
{
    public HttpBotApplication(IMsgPipeline pipeline, ObjContainer container) : base(pipeline, container)
    {
    }

    public override async Task RunAsync()
    {
        while (true)
        {
            if (await pipeline.GetMsgAsync() is HttpMsg msg)
                await ExecMiddleware(msg).Invoke();
        }
    }
}