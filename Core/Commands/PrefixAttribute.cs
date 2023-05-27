namespace ChatGptBotConsole;
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class PrefixAttribute : Attribute
{
    public string[] Prefix { get; set; }
    public PrefixAttribute(params string[] prefix)
    {
        Prefix = prefix;
    }
}