using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using EleCho.GoCqHttpSdk;

namespace ChatGptBotConsole;

public static class QQExt
{
    /// <summary>
    /// 使用Uri初始化qq机器人的地址
    /// </summary>
    public static BotApplicationBuilder AddQQBot(this BotApplicationBuilder builder, [NotNull] Uri botSocketUri)
    {
        return builder.AddQQBot(options =>
        {
            options.BaseUri = botSocketUri;
            return options;
        });
    }
    /// <summary>
    /// 使用string初始化qq机器人的地址
    /// </summary>
    public static BotApplicationBuilder AddQQBot(this BotApplicationBuilder builder, [NotNull] string botSocketUri)
    {
        return builder.AddQQBot((options) =>
        {
            options.BaseUri = new Uri(botSocketUri);
            return options;
        });
    }
    public static BotApplicationBuilder AddQQBot(this BotApplicationBuilder builder, Func<CqWsSessionOptions, CqWsSessionOptions> action)
    {
        builder.AddAction(async (application, container) =>
        {
            var option = new CqWsSessionOptions();
            option = action(option);
            // 创建bot session
            CqWsSession session = new(option);
            session.UseGroupMessage(async context =>
            {
                // 尝试创建群at消息，暂时只接受群消息

                // var msg = QQGroupMsg.CreateMsg(context, session);
                var msg = new QQGroupMsg(context, session, container.GetObj<HttpClient>(), container.GetObj<IConfig<NlpConfig>>(), container.GetObj<IConfig<AccountListData>>());
                if (msg != null && msg.Msg != null)
                    application.AddMsg(msg);
            });
            session.UsePrivateMessage(async context =>
            {
                var msg = QQPrivateMsg.CreateMsg(context, session);
                if (msg != null)
                    application.AddMsg(msg);
            });
            await session.StartAsync();
            await session.WaitForShutdownAsync();
        });
        builder.AddType<QQBotApplication>();
        builder.AddType<MsgPipeLine>();
        return builder;
    }
}