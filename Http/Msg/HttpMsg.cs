using System.Data;
using System.Net;
using Collapsenav.Net.Tool;

namespace EasyChatGptBot;

public class HttpMsg : BotMsg<HttpSimpleUser>
{
    protected readonly HttpListenerContext context;
    public HttpMsg() { }
    public HttpMsg(HttpListenerContext context)
    {
        InitMsg(this, context);
        this.context = context;
    }

    public override void Response(string data)
    {
        context.Response.OutputStream.Write(data.ToBytes());
        context.Response.Close();
    }
    public static HttpMsg CreateMsg(HttpListenerContext context)
    {
        return InitMsg(new HttpMsg(), context);
    }
    public static HttpMsg InitMsg(HttpMsg botMsg, HttpListenerContext context)
    {
        if (botMsg == null || context == null)
            throw new NoNullAllowedException();
        using var ms = new MemoryStream();
        context.Request.InputStream.CopyTo(ms);
        var name = context.Request.Headers["Name"];
        if (name.NotEmpty())
            botMsg.From = new HttpSimpleUser(name);

        var msg = ms.ToBytes().BytesToString();
        botMsg.Msg = msg;
        return botMsg;
    }
}