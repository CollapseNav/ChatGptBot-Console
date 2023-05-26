namespace EasyChatGptBot;

public class OpenAIChatRequestModel
{
    private int max_tokens1 = 2048;
    private decimal temperature1 = 0.5m;
    private decimal top_p1 = 0.5m;
    private decimal frequency_penalty1 = 1;
    private decimal presence_penalty1 = 1;

    /// <summary>
    /// 模型名称
    /// </summary>
    public string model { get; set; }
    /// <summary>
    /// 单次返回的最大token长度
    /// </summary>
    public int max_tokens
    {
        get => max_tokens1; set
        {
            if (value <= 0 || value > 2048)
                throw new Exception("max_tokens的值只能在 1~2048 之间");
            max_tokens1 = value;
        }
    }
    /// <summary>
    /// 温度
    /// </summary>
    /// <remarks>一般来说，值越大越放飞自我</remarks>
    public decimal temperature
    {
        get => temperature1; set
        {
            if (value < 0 || value > 1)
                throw new Exception("temperature 的值只能在0~1之间");
            temperature1 = value;
        }
    }
    /// <summary>
    /// 核采样
    /// </summary>
    /// <remarks>一般来说，值越大越啰嗦</remarks>
    public decimal top_p
    {
        get => top_p1; set
        {
            if (value <= 0 || value > 1)
                throw new Exception("top_p 的值只能在0~1之间");
            top_p1 = value;
        }
    }
    /// <summary>
    /// 重复字惩罚
    /// </summary>
    public decimal frequency_penalty
    {
        get => frequency_penalty1; set
        {
            if (value <= 0 || value > 2)
                throw new Exception("frequency_penalty 的值只能在0~1之间");
            frequency_penalty1 = value;
        }
    }
    /// <summary>
    /// 重复主题惩罚
    /// </summary>
    public decimal presence_penalty
    {
        get => presence_penalty1; set
        {
            if (value <= 0 || value > 2)
                throw new Exception("presence_penalty 的值只能在0~1之间");
            presence_penalty1 = value;
        }
    }
    public IEnumerable<OpenAIChatUnit> messages { get; set; }
}