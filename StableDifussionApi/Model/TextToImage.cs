namespace ChatGptBotConsole;

public class TextToImageModel
{
    /// <summary>
    /// 使用高清修复
    /// </summary>
    public bool enable_hr { get; set; } = false;
    /// <summary>
    /// 高清修复幅度
    /// </summary>
    public decimal denoising_strength { get; set; } = 0.2m;
    public int firstphase_width { get; set; } = 0;
    public int firstphase_height { get; set; } = 0;
    /// <summary>
    /// 高清放大倍数
    /// </summary>
    public int hr_scale { get; set; } = 2;
    /// <summary>
    /// 高清放大倍数
    /// </summary>
    public string hr_upscaler { get; set; }
    public int hr_second_pass_steps { get; set; }
    public int hr_resize_x { get; set; } = 0;
    public int hr_resize_y { get; set; } = 0;
    /// <summary>
    /// 提示词
    /// </summary>
    public string prompt { get; set; }
    public string[] styles { get; set; }
    public int seed { get; set; } = -1;
    public int subseed { get; set; } = -1;
    public int subseed_strength { get; set; } = 0;
    public int seed_resize_from_h { get; set; } = -1;
    public int seed_resize_from_w { get; set; } = -1;
    public string sampler_name { get; set; } = "DPM++ 2S a Karras";
    // public int batch_size { get; set; }
    // public int n_iter { get; set; }
    public int steps { get; set; } = 20;
    public decimal cfg_scale { get; set; } = 7;
    public int width { get; set; } = 512;
    public int height { get; set; } = 512;
    /// <summary>
    /// 修复脸
    /// </summary>
    public bool restore_faces { get; set; } = false;
    public bool tiling { get; set; } = false;
    public bool do_not_save_samples { get; set; } = false;
    public bool do_not_save_grid { get; set; } = false;
    /// <summary>
    /// 反向词
    /// </summary>
    public string negative_prompt { get; set; }
    // public int eta { get; set; }
    // public int s_churn { get; set; }
    // public int s_tmax { get; set; }
    // public int s_tmin { get; set; }
    // public int s_noise { get; set; }
    // public object override_settings { get; set; }
    // public bool override_settings_restore_afterwards { get; set; }
    // public string[] script_args { get; set; }
    // public string sampler_index { get; set; }
    // public string script_name { get; set; }
    // public bool send_images { get; set; }
    /// <summary>
    /// 保存图片
    /// </summary>
    public bool save_images { get; set; } = true;
    // public object alwayson_scripts { get; set; }
}