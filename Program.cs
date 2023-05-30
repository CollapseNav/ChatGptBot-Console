using ChatGptBotConsole;
using Collapsenav.Net.Tool;
var builder = BotApplication.CreateBuilder();
var configPath = args.FirstOrDefault().IsEmpty("AppConfig.json");
builder
.AddAppConfig(configPath)
.AddJsonConfig<OpenAIChatConfig>("ChatConfig")
.AddJsonConfig<OpenAIConfig>("OpenAIConfig")
.AddJsonConfig<AiContent>("Content")
.AddJsonConfig<NlpConfig>("NlpConfig")
.AddJsonConfig<AccountListData>("AccountConfig")
.AddJsonConfig<SdConfig>("SdConfig")
.AddJsonConfig<List<NLPLoraNode>>("Lora")
.AddQQBot("ws://localhost:8080")
.AddType<ChatSessionManager<QQGroupUser>>()
.AddType<ChatSessionManager<QQSimpleUser>>()
.AddType<BaseChatSession>()
.Add(new HttpClient())
.AddCommand()
.Add(new AccountListData())
;
JsonExt.DefaultJsonSerializerOption.WriteIndented = true;
var app = builder.Build();
app.Use<DefaultErrorHandleMiddleware>();
app.Use<WhiteListFilterMiddleware>();
app.Use<NlpClassificationMiddleware>();
app.Use<BaseChatMiddleware<QQSimpleUser>>();
app.Use<BaseChatMiddleware<QQGroupUser>>();
await app.RunAsync();
