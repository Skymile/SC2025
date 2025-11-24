public interface IConfig
{
    bool IsDebug { get; set; }
    int MapHeight { get; set; }
    int MapWidth { get; set; }
    int TimeoutInMs { get; set; }
}