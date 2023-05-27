using System.Reflection;
using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public class CmdManagement
{
    private readonly ObjContainer container;
    public CmdManagement(ObjContainer container)
    {
        this.container = container;
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item => item.GetTypes())
        .Where(item => item.IsType<ICommand>() && !item.IsAbstract && !item.IsInterface).ToList();
        Cmds = types.Select(item =>
        {
            var prefix = item.GetCustomAttribute<PrefixAttribute>();
            if (prefix == null)
                return (null, null);
            return (item, prefix.Prefix);
        }).Where(item => item.item != null).ToDictionary(item => item.item, item => item.Prefix);
    }
    public Dictionary<Type, string[]> Cmds { get; set; }
    private ICommand GetCommand(Type type)
    {
        return (container.GetObj(type) as ICommand)!;
    }

    public ICommand GetCommand(string prefix)
    {
        if (!Cmds.Any(item => item.Value.Contains(prefix)))
            return null;
        var data = Cmds.FirstOrDefault(item => item.Value.Contains(prefix));
        return GetCommand(data.Key);
    }
}