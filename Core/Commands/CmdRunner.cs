namespace ChatGptBotConsole;

public class CmdRunner<Cmd> where Cmd : ICommand
{
    private readonly Cmd cmd;

    public CmdRunner(Cmd cmd)
    {
        this.cmd = cmd;
    }
}