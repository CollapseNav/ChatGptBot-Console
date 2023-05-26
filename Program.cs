using Collapsenav.Net.Tool;
using EasyChatGptBot;
var builder = BotApplication.CreateBuilder();
var configPath = args.FirstOrDefault().IsEmpty("AppConfig.json");
builder
.AddAppConfig(configPath)
.AddJsonConfig<OpenAIChatConfig>("ChatConfig")
.AddJsonConfig<OpenAIConfig>("OpenAIConfig")
.AddJsonConfig<AiContent>("Content")
// .AddQQBot("ws://localhost:8080")
.AddHttpBot("http://localhost:8080/")
.AddType<ChatSessionManager<HttpSimpleUser>>()
.AddType<BaseChatSession>()
.Add(new HttpClient())
;
var app = builder.Build();
app.Use<DefaultErrorHandleMiddleware>();
app.Use<BaseChatMiddleware<HttpSimpleUser>>();
await app.RunAsync();
