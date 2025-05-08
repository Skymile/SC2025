public class Config
{
    public int   MapWidth         { get; set; } = 15;
    public int   MapHeight        { get; set; } = 10;
    public int   SnakeSpeed       { get; set; } = 100;
    public int   WallsCount       { get; set; } = 4;
    public int   ApplesCount      { get; set; } = 2;
    public Point StartingPosition { get; set; } = new(4, 5);
    public bool  IsAsync          { get; set; } = false;
}
