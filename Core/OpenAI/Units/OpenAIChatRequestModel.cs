namespace ChatGptBotConsole;

public class OpenAIChatRequestModel : OpenAIRequestModel
{
    public IEnumerable<OpenAIChatUnit>? messages { get; set; }
}