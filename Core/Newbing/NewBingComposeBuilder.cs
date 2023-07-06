using System.Net.Http.Json;
namespace ChatGptBotConsole;
public class NewBingComposeBuilder
{
    public IEnumerable<string> DefaultOptions = new[] {
        "nlu_direct_response_filter",
        "deepleo",
        "enable_debug_commands",
        "disable_emoji_spoken_text",
        "responsible_ai_policy_235",
        "enablemm",
        "soedgeca"
    };
    public IEnumerable<string> DefaultAllowMessagbeTypes = new[] {
        "ActionRequest",
        "Chat",
        "Context",
        "InternalSearchQuery",
        "InternalSearchResult",
        "Disengaged",
        "InternalLoaderMessage",
        "RenderCardRequest",
        "AdsQuery",
        "SemanticSerp",
        "GenerateContentQuery",
        "SearchQuery"
    };
    public async Task<NewBingCompose> Build()
    {
        var model = new NewBingCompose();
        var arg = new NewBingComposeArgument()
        {
            Source = "edge_coauthor_prod",
            Verbosity = "verbose",
        };
        arg.OptionSets = DefaultOptions.ToList();
        arg.AllowedMessageTypes = DefaultAllowMessagbeTypes.ToList();
        arg.SliceIds = Enumerable.Empty<string>();
        HttpClient client = new HttpClient();
        var sec = await client.GetFromJsonAsync<NewBingComposeSec>("https://mulit.collapsenav.com/turing/conversation/create");
        arg.ConversationId = sec?.ConversationId;
        arg.ConversationSignature = sec?.ConversationSignature;
        arg.Participant = new() { Id = sec?.ClientId };

        var msg = BuildMsg();
        arg.Message = msg;
        model.Arguments = new[] { arg };
        return model;
    }

    public NewBingComposeArgumentMessage BuildMsg()
    {
        var msg = new NewBingComposeArgumentMessage();
        return msg;
    }
}