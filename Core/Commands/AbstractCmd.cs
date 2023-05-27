using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

public abstract class AbstractCmd : ICommand
{
    public string Description { get; protected set; }
    public string Help { get; protected set; }
    public abstract Task<bool> ExecAsync(IBotMsg botMsg);
}