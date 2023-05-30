using System.Reflection;

namespace ChatGptBotConsole;

[Prefix("重绘 0.1", "重绘0.2", "重绘0.3", "重绘0.4", "重绘0.5")]
public class HireParam : SdParam
{
    public static int Steps { get; set; } = 5;
    public static decimal Denoising { get; set; } = 0.2m;
    public static void UpdateTextToImage(TextToImageModel model, Dictionary<string, decimal> result)
    {
        var labels = typeof(HireParam).GetCustomAttribute<PrefixAttribute>()!.Prefix;
        var matches = result.Where(item => labels.Contains(item.Key)).ToArray();
        var match = matches.First();
        if (match.Value < 0.95m)
            return;
        model.enable_hr = true;
        model.hr_second_pass_steps = Steps;
        model.hr_upscaler = "R-ESRGAN 4x+ Anime6B";
        model.denoising_strength = match.Key switch
        {
            "重绘 0.1" => 0.1m,
            "重绘 0.2" => 0.2m,
            "重绘 0.3" => 0.3m,
            "重绘 0.4" => 0.4m,
            "重绘 0.5" => 0.5m,
            "重绘 0.6" => 0.6m,
            "重绘 0.7" => 0.7m,
            "重绘 0.8" => 0.8m,
            "重绘 0.9" => 0.9m,
            _ => Denoising
        };
    }
}