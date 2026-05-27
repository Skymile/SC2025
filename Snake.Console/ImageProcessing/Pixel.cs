namespace ImageProcessing;

public readonly record struct Pixel(byte R, byte G, byte B)
{
    public Pixel(byte rgb) : this(rgb, rgb, rgb) { }

    public readonly double Average = (R + G + B) / 3.0;
}
