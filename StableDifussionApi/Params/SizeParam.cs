using System.Reflection;

namespace ChatGptBotConsole;

[Prefix("横屏", "横屏画面", "竖屏", "竖屏画面")]
public class SizeParam : SdParam
{
    public static int Width { get; set; } = 512;
    public static int Height { get; set; } = 512;

    public static void UpdateTextToImage(TextToImageModel model, Dictionary<string, decimal> result)
    {
        var labels = typeof(SizeParam).GetCustomAttribute<PrefixAttribute>()!.Prefix;
        var matches = result.Where(item => labels.Contains(item.Key)).ToArray();
        var match = matches.First();
        model.width = Width;
        model.height = Height;
        if (match.Value < 0.9m)
            return;
        if (match.Key.StartsWith("横屏"))
        {
            model.width = 768;
            model.height = 512;
            return;
        }
        if (match.Key.StartsWith("竖屏"))
        {
            model.width = 512;
            model.height = 768;
            return;
        }
    }
}