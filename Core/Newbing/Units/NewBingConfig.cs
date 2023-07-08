namespace ChatGptBotConsole;

public class NewBingConfig : IStartInit
{
    /// <summary>
    /// 在cf上部署的worker地址
    /// </summary>
    public string WorkerUrl { get; set; }
    public string DefaultContent { get; set; }
    public void InitConfig()
    {
    }
}