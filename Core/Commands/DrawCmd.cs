using System.Net.Http.Json;
using System.Reflection;
using Collapsenav.Net.Tool;

namespace ChatGptBotConsole;

[Prefix("描绘图像", "画画", "画图")]
public class DrawCmd : ClassificationCmd
{
    private readonly BaseChatSession session;
    private readonly IConfig<SdConfig> sdconfig;
    private readonly IConfig<NlpConfig> nlpconfig;
    private readonly IConfig<OpenAIConfig> aiconfig;
    private readonly HttpClient client;

    private readonly Random Rand = new();

    public DrawCmd(BaseChatSession session, IConfig<List<NLPLoraNode>> lora, IConfig<SdConfig> sdconfig, IConfig<NlpConfig> nlpconfig, IConfig<OpenAIConfig> aiconfig, HttpClient client)
    {
        this.session = session;
        this.sdconfig = sdconfig;
        this.nlpconfig = nlpconfig;
        this.aiconfig = aiconfig;
        this.client = client;
        LoraNodes = lora.Data;
    }

    private string[] SampleNames = new[] {
        "Euler a",
        "Euler a",
        "Euler a",
        "Euler",
        "Euler",
        "Euler",
        "Heun",
        // "DPM2",
        // "DPM++ 2S a",
        // "DPM++ 2M",
        // "DPM++ SDE",
        "DPM adaptive",
        "DPM2 Karras",
        "DPM++ 2S a Karras",
        // "DPM++ SDE Karras",
    };

    // private List<NLPLoraNode> LoraNodes = new() {
    //     new NLPLoraNode("彩虹颜色的头发",0.9m,"RainbowHairV1",0.8m,LoraType.HairStyle){},
    //     new NLPLoraNode("裙子边缘像水流",0.9m,"LiquidClothesV1fixed",0.8m,LoraType.Style){},
    //     new NLPLoraNode("裙子是水做的",0.9m,"LiquidClothesV1fixed",0.8m,LoraType.Style){},
    //     new NLPLoraNode("让画面整体看起来像是杂志的封面",0.9m,"Magazine-10",0.8m,LoraType.Style){},
    //     new NLPLoraNode("一种叫写轮眼的眼睛，主要为红色，里面有黑色的勾玉环绕",0.9m,"sharingan",1,LoraType.Eye,new[]{"sharingan"}){},
    //     // new NLPLoraNode("水彩",0.99m,"Colorwater_v4",0.7m,LoraType.Style){},
    //     // new NLPLoraNode("在水面下,有种流光溢彩的感觉",0.9m,"[LoRa] Glistening Light   流光 Concept (With multires noise version)",1,LoraType.Style){},
    //     // new NLPLoraNode("骑在某种怪物或者神话生物身上",0.9m,"[LoRa] Riding Monster怪獣騎士 Concept[ridingmonster]",0.8m,LoraType.Style,new[]{"ridingmonster"}){},
    // };
    private List<NLPLoraNode> LoraNodes;
    public override async Task<bool> ExecAsync(IBotMsg botMsg)
    {
        session.SetContent(string.Empty);
        string quesiton =
        $"""
        我希望你扮演Kanibot。Kanibot是一个热心而有礼貌的AI助理，帮助人类通过自然语言改进stable diffusion的输入prompt，提供更优化的描述。Kanibot按以下规则行动：

        - Kanibot的任务是为stable diffusion的输入prompt提供更优化的描述。stable diffusion是一个程序，它接收自然语言的prompt输入，并生成图片。一个prompt的例子是：

        (masterpiece), best quality, highly detailed, ultra-detailed, official art, A girl is sitting in the classroom. (1girl), short hair, blue eyes, long sleeves, white thighhighs, classroom, sunshine, cloudy, windows, bookshelf

        这个例子分为三个部分：

        第一部分是 (masterpiece), best quality, highly detailed, ultra-detailed, official art 这一段是质量控制关键词。这些关键词可以帮助stable diffusion提升画面质量。任何输出的最开头都应该完整复制这段质量控制关键词！

        第二部分是 A girl is sitting in the classroom. 这是根据输入的文本生成的优化描述。

        第三部分是之后的部分 (1girl), short hair, blue eyes, long sleeves, white thighhighs, classroom, sunshine, cloudy, windows, bookshelf 这些是描述图片的关键词，这些关键词细致的决 定了画面中的每一个对象。

        - Kanibot在生成优化的描述前，会将质量控制关键词添加在任何输出的前面。这将组成prompt的第一部 分。

        - Kanibot在生成优化的描述时，无论原始语言是什么，都只能使用英文输出。这是因为stable diffusion只接受英文的输入。Kanibot需要根据输入的文本，尽可能详细而精确的描述应生成图片的细节；Kanibot不应只限于已知输入的内容，而是在精确表达下自由 添加未表达的细节，包括场景的氛围和艺术特征。这将组成prompt的第二部分。

        - Kanibot在生成优化的描述后，会在之后添加描述 图片的关键词。Kanibot会以关键词形式单独描述图片中出现的每一个对象，以及增加对于主题、背景、色调和样式等抽象概念的关键字 ，来丰富画面的细节。Kanibot应当忽略有关"高清重绘,画面尺寸,横屏竖屏"之类的内容。Kanibot可以自由选择画面中应有的细节添加关键词。Kanibot会在每两个关键词中间加上逗号，表示关键词之间的分隔符。这将组成prompt的第三部分。

        - Kanibot生成描述图片的关键词时，可能会给一些关键词加上一层或者两层的括号，括号层 数越多，表示这个关键词越重要，例如：A ， (A) 和 ((A)) 其重要程度逐渐递增。一般情况下，不需要给关键词加括号。

        - Kanibot会将以上生成的三部分文本用逗号连接，产生只有一行的英文文本，中间不包含任何换行符的prompt作为最终结果。

        - Kanibot输出时将直接输出用于stable diffusion的prompt，而不包含任何说明和解释。

        - Kanibot会理解下面提供的文本，并对文本内容进行优化，然后按照规则输出stable diffusion的prompt。prompt由上文提到的三部分组成，每部分之间用逗号连接。然后Kanibot会将prompt输出。

        需要处理的文本是：
        {botMsg.Msg}
        """;

        // var labels = LoraNodes.Select(item => item.Label!).ToList();
        // labels.AddRange("other", "not sure", "unknow");
        // var res = await client.PostAsJsonAsync($"{nlpconfig.Data.Url}/text", new NlpPostModel
        // {
        //     Content = botMsg.Msg,
        //     Labels = labels.ToArray(),
        //     Multi = true,
        // });

        // Dictionary<string, decimal> result = new();
        // if (res.IsSuccessStatusCode)
        // {
        //     result = (await res.Content.ReadFromJsonAsync<Dictionary<string, decimal>>())!;
        //     Console.WriteLine(result.ToJson());
        // }

        var oldAiConfig = aiconfig.Data.ToJson();
        aiconfig.Data.Temperature = 0.1m;
        aiconfig.Data.Top_P = 0.1m;
        aiconfig.Data.Frequency_penalty = 1;
        aiconfig.Data.Presence_penalty = 1;
        // 从openai获取prompt
        var promptTask = session.AskAsync(quesiton);

        // 获取大致的lora分类
        var labels = LoraNodes.Where(item => item.Enable).SelectMany(item => item.Labels!).Unique().ToList();
        labels.AddRange("other", "not sure", "unknow");
        var paramTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(item => item.GetTypes()).Where(item => item.IsType<SdParam>() && !item.IsAbstract).ToArray();
        var paramLabels = paramTypes.SelectMany(item => item.GetCustomAttribute<PrefixAttribute>().Prefix).Unique().ToArray();
        var loraTask = client.PostAsJsonAsync($"{nlpconfig.Data.Url}/text", new NlpPostModel
        {
            Content = botMsg.Msg,
            Labels = labels.ToArray(),
            Multi = true,
        });
        var sdparamsTask = client.PostAsJsonAsync($"{nlpconfig.Data.Url}/text", new NlpPostModel
        {
            Content = botMsg.Msg,
            Labels = paramLabels,
            Multi = true,
        });
        var aiPrompt = await promptTask;
        var res = await loraTask;
        var sdRes = await sdparamsTask;

        aiconfig.Data = oldAiConfig.ToObj<OpenAIConfig>();

        // 组合prompts
        var prompts = aiPrompt.Split(',', '.', '|').Select(item => item.Trim()).Where(item => item.NotEmpty()).ToList();
        var qualityPropmts = new[] { "(masterpiece)", "best quality", "highly detailed", "ultra-detailed", };
        var negativePrompts = "(worst quality, low quality:1.4), monochrome, zombie, (nsfw:1.4), (nude:1.4), wrong hand, (watermark:1.4),";

        // 暂时优先添加质量词
        prompts = qualityPropmts.Concat(prompts).Unique().ToList();

        var sdPostData = new TextToImageModel
        {
            sampler_name = SampleNames[Rand.Next(SampleNames.Length)]
        };
        // 选取lora
        Dictionary<string, decimal> result = new();
        if (res.IsSuccessStatusCode)
        {
            result = (await res.Content.ReadFromJsonAsync<Dictionary<string, decimal>>())!;
            Console.WriteLine(result.ToJson());
            var matchLoras = LoraNodes.Where(item => result.First(r => item.Labels.Contains(r.Key)).Value > item.MatchWeight).ToArray();
            matchLoras = matchLoras.GroupBy(item => item.Lora).Select(item => item.First()).ToArray();
            foreach (var lora in matchLoras)
            {
                var fullLora = $"<lora:{lora.Lora}:{Math.Round(lora.LoraWeight!.Value, 1)}>";
                prompts.AddRange(fullLora);
                if (lora.TriggerWords.NotEmpty())
                    prompts.AddRange(lora.TriggerWords!);
            }
        }

        if (sdRes.IsSuccessStatusCode)
        {
            result = (await sdRes.Content.ReadFromJsonAsync<Dictionary<string, decimal>>())!;
            Console.WriteLine(result.ToJson());
            HireParam.UpdateTextToImage(sdPostData, result);
            SizeParam.UpdateTextToImage(sdPostData, result);
        }
        sdPostData.prompt = prompts.Join(",") + ",";
        sdPostData.negative_prompt = negativePrompts;

        // 发给sd画图
        string url = $"{sdconfig.Data.Url}/sdapi/v1/txt2img";
        res = await client.PostAsJsonAsync(url, sdPostData);

        var str = await res.Content.ReadAsStringAsync();
        if (str == null)
        {
            botMsg.Response("生成图像失败");
            return false;
        }
        var bytes = str.ToObj<ImageResult>()?.Images?.FirstOrDefault().FromBase64();
        var stream = bytes.ToStream();
        botMsg.Response(new MultiResponseData
        {
            Images = new[] { stream },
            IsAt = false,
        });
        return true;
    }
}

public class ImageResult
{
    public string[]? Images { get; set; }
}