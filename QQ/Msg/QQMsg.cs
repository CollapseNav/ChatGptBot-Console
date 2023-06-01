using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Transactions;
using Collapsenav.Net.Tool;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.Post;

namespace ChatGptBotConsole;

public abstract class QQMsg<MsgContext> : BotMsg where MsgContext : CqMessagePostContext
{
    public QQSimpleUser? From { get; protected set; }
    protected Stream Audio;
    protected Stream[] Images;
    protected readonly MsgContext context;
    protected readonly CqWsSession session;
    private readonly HttpClient client;
    private readonly IConfig<NlpConfig> nlpconfig;

    public QQMsg(CqWsSession session)
    {
        this.session = session;
    }
    public QQMsg(MsgContext context, CqWsSession session, HttpClient client = null, IConfig<NlpConfig> nlpconfig = null)
    {
        this.context = context;
        this.session = session;
        this.client = client;
        this.nlpconfig = nlpconfig;
    }

    public void InitByMsgContext(CqMessagePostContext message)
    {
        var msg = message.Message.Where(item => item is CqRecordMsg).FirstOrDefault();
        if (msg is CqRecordMsg recordMsg)
        {
            try
            {

                using var stream = client.GetStreamAsync(recordMsg.Url).Result;
                Audio ??= new MemoryStream();
                stream.CopyTo(Audio);
                var base64 = Audio.ToBytes().ToBase64();
                var res = client.PostAsJsonAsync($"{nlpconfig.Data.Url}/voice", new NlpPostModel
                {
                    Content = base64
                }).Result;
                if (res.IsSuccessStatusCode)
                {
                    Msg = res.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(Msg);
                }
                else
                {
                    Audio.Dispose();
                    Audio = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}