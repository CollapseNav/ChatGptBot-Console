namespace EasyChatGptBot;

public class OpenAIChatUnit
{
    public OpenAIChatUnit()
    {
    }

    public OpenAIChatUnit(string role, string content)
    {
        Role = role;
        Content = content;
    }

    public string Role { get; set; }
    public string Content { get; set; }
}