namespace ChatGptBotConsole;

public interface IChatSessionManager
{
    IChatSession GetSessionByBotMsg(IBotMsg botMsg);
}