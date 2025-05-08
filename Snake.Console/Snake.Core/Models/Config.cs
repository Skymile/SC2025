public class Config
{
    public int   MapWidth         { get; set; } = 20;
    public int   MapHeight        { get; set; } = 15;
    public int   SnakeSpeed       { get; set; } = 100;
    public int   WallsCount       { get; set; } = 20;
    public int   ApplesCount      { get; set; } = 4;
    public Point StartingPosition { get; set; } = new(4, 5);
    public bool  IsAsync          { get; set; } = true;
}
