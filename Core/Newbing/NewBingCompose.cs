namespace ChatGptBotConsole;

public class NewBingCompose
{
    public IEnumerable<NewBingComposeArgument> Arguments { get; set; }
    public string InvocationId { get; set; } = "0";
    public string Target { get; set; } = "chat";
    public int Type { get; set; } = 4;
}

public class NewBingComposeArgument
{
    public string Source { get; set; }
    public string Verbosity { get; set; }
    public string SpokenTextMode { get; set; } = "None";
    public string ConversationSignature { get; set; }
    public string ConversationId { get; set; }
    public bool IsStartOfSession { get; set; } = true;
    public IEnumerable<string> OptionSets { get; set; }
    public IEnumerable<string> AllowedMessageTypes { get; set; }
    public IEnumerable<string> SliceIds { get; set; }
    public NewBingComposeParticipant Participant { get; set; }
    public NewBingComposeArgumentMessage Message { get; set; }
}

public class NewBingComposeParticipant
{
    public string Id { get; set; }
}

public class NewBingComposeArgumentMessage
{
    public string Locale { get; set; } = "zh-CN";
    public string Market { get; set; } = "en-US";
    public string Regino { get; set; } = "US";
    public string Location { get; set; } = "lat:47.639557;long:-122.128159;re=1000m;";
    public string Author { get; set; } = "user";
    public string Timestamp { get; set; } = DateTime.Now.ToString();
    public string InputMethod { get; set; } = "Keyboard";
    public string MessageType { get; set; } = "Chat";
}

public class NewBingComposeSec
{
    public string ClientId { get; set; }
    public string ConversationId { get; set; }
    public string ConversationSignature { get; set; }
}

