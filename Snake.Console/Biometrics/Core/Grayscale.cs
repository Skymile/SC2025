namespace Biometrics.Core;

public class Grayscale : Algorithm
{
    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        for (int i = 0; i < length; i += 3)
        {
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
