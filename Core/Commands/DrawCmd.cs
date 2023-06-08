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
        根据"画一个女孩坐在教师中",我可以联想到英语:
        A girl is sitting in the classroom. (1girl), short hair, blue eyes, long sleeves, white thighhighs, classroom, sunshine, cloudy, windows, bookshelf

        根据"一个男人穿着黑色衬衣和白色背带裤，有着一头灰色中分发型，一边跳舞一边打篮球",我可以联想到英语:
        A man with a center-parted hairstyle is playing basketball,He is wearing a black shirt and white suspenders,basketball,outdoor,athletic,sport,exercise,competition,sunny

        根据"{botMsg.Msg}",我可以联想到英语:

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
        aiPrompt = aiPrompt.Split("\n").Where(item => item.NotEmpty()).SelectMany(item => item.Split(',', '.', ';', '|').Where(s => s.NotEmpty())).Join(",");
        Console.WriteLine(aiPrompt);
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