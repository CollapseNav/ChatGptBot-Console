using System.Net.Http.Json;
using System.Reflection;
using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public class NlpClassificationMiddleware : IMiddleware
{
    private IConfig<NlpConfig> Config;
    private readonly HttpClient client;
    private readonly ObjContainer container;
    private readonly CmdManagement management;
    private static string[]? Labels;

    public NlpClassificationMiddleware(IConfig<NlpConfig> Config, HttpClient client, ObjContainer container, CmdManagement management)
    {
        this.Config = Config;
        this.client = client;
        this.container = container;
        this.management = management;
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item => item.GetTypes()).Where(item => item.IsType<ClassificationCmd>() && !item.IsAbstract).ToList();
        Labels ??= types.SelectMany(item =>
        {
            var prefix = item.GetCustomAttribute<PrefixAttribute>();
            return prefix!.Prefix;
        }).Unique().ToArray();
    }

    public async Task Invoke(IBotMsg botMsg, Func<Task> next)
    {
        var res = await client.PostAsJsonAsync($"{Config.Data.Url}/text", new NlpPostModel
        {
            Content = botMsg!.Msg!,
            Labels = Labels!,
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
        Console.WriteLine("-------------------------------------");
        Console.WriteLine(result.ToJson());
        var cmd = management.GetCommand(cmdStr);
        if (cmd != null && cmd.GetType() == typeof(DefaultCmd))
        {
            await next();
        }
        else if (cmd != null)
            await cmd.ExecAsync(botMsg);
    }
}