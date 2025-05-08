public class Config
{
    public int   MapWidth         { get; set; } = 20;
    public int   MapHeight        { get; set; } = 15;
    public int   SnakeSpeed       { get; set; } = 20;
    public int   WallsCount       { get; set; } = 40;
    public int   ApplesCount      { get; set; } = 2;
    public Point StartingPosition { get; set; } = new(3, 4);
    public bool  IsAsync          { get; set; } = true;
}
