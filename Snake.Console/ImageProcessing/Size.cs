namespace ImageProcessing;

public readonly record struct Size(int Width, int Height) 
{
    public readonly int Length = Width * Height;
}
