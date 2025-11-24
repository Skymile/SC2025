public class Config : IConfig
{
    public bool IsDebug     { get; set; } = false;
    public int  TimeoutInMs { get; set; } = 100;
    public int  MapWidth    { get; set; } = 30;
    public int  MapHeight   { get; set; } = 20;
}
