namespace Biometrics;

public class Grayscale : Algorithm
{
    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int i = x * 3 + y * stride;
                int value =
                    read[i + 0] +
                    read[i + 1] +
                    read[i + 2];

                write[i + 0] =
                write[i + 1] =
                write[i + 2] = (byte)(value / 3);
            }
    }
}
