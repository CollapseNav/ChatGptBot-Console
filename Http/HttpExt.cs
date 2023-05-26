using System.Net;

namespace EasyChatGptBot;

public static class HttpExt
{
    public static BotApplicationBuilder AddHttpBot(this BotApplicationBuilder builder, params string[] listen)
    {
        builder.AddAction(async (application, container) =>
        {
            HttpListener listener = new();
            foreach (var ip in listen)
                listener.Prefixes.Add(ip.EndsWith("/") ? ip : (ip + "/"));
            listener.Start();
            while (true)
            {
                var context = await listener.GetContextAsync();
                var botMsg = new HttpMsg(context);
                application.AddMsg(botMsg);
            }
        });
        builder.AddType<HttpBotApplication>();
        builder.AddType<MsgPipeLine>();
        return builder;
    }
}