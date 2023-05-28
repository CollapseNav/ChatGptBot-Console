namespace ChatGptBotConsole;

[Prefix("提问", "编程", "代码", "聊天", "不确定", "其他")]
public class DefaultCmd : ClassificationCmd
{
    public override Task<bool> ExecAsync(IBotMsg botMsg)
    {
        throw new NotImplementedException();
    }
}