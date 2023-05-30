namespace ChatGptBotConsole;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class LoraDesAttribute : Attribute
{
    public string[] Desc { get; set; }
    public LoraDesAttribute(params string[] desc)
    {
        Desc = desc;
    }
}