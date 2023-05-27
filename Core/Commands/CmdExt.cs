using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public static class CmdExt
{
    public static BotApplicationBuilder AddCommand(this BotApplicationBuilder builder)
    {
        builder.AddType<CmdManagement>();
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item => item.GetTypes())
        .Where(item => item.IsType<ICommand>() && !item.IsAbstract && !item.IsInterface).ToList();
        types.ForEach(item => builder.AddType(item));
        builder.AddAction((application, container) =>
        {
            builder.Add(container.GetObj<CmdManagement>());
        });
        return builder;
    }
}