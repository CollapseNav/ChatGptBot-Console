namespace ChatGptBotConsole;

public interface IChatSessionManager
{
    IOpenAiChatSession GetSessionByBotMsg(IBotMsg botMsg);
}