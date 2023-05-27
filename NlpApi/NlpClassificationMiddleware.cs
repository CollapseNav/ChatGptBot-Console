using System.Net.Http.Json;
using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public class NlpClassificationMiddleware : IMiddleware
{
    private IConfig<NlpConfig> Config;
    private readonly HttpClient client;
    private readonly ObjContainer container;
    private readonly CmdManagement management;

    public NlpClassificationMiddleware(IConfig<NlpConfig> Config, HttpClient client, ObjContainer container, CmdManagement management)
    {
        this.Config = Config;
        this.client = client;
        this.container = container;
        this.management = management;
    }

    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        var res = await client.PostAsJsonAsync($"{Config.Data.Url}/text", new NlpPostModel
        {
            Content = botMsg.Msg,
            Labels = Config.Data.DefaultLables,
        });
        Dictionary<string, decimal> result = new();
        if (res.IsSuccessStatusCode)
            result = (await res.Content.ReadFromJsonAsync<Dictionary<string, decimal>>())!;
        else
        {
            await next();
            return;
        }
        var cmdStr = result.First().Key;
        if (cmdStr.In("other", "unknow", "chat"))
        {
            await next();
            return;
        }
        var cmd = management.GetCommand(cmdStr);
        await cmd.ExecAsync(botMsg);
    }
}