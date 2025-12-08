namespace Biometrics;

public class MedianFilter : Algorithm
{
    public override unsafe void Apply(
        byte* read, byte* write, int length, int stride, int width, int height)
    {
        int windowSize = 3;
        var list = new List<byte>();
        int border = windowSize / 2;

        for (int k = 0; k < 3; k++)
            for (int y = border; y < height - border; y++)
                for (int x = border; x < width - border; x++)
                {
                    int i = x * 3 + y * stride;

                    list.Clear();

                    for (int yy = -border; yy < border; yy++)
                        for (int xx = -border; xx < border; xx++)
                        {
                            int offset = xx * 3 + yy * stride;

                            list.Add(read[i + offset + k]);
                        }

                    list.Sort();
                    write[i + k] = list[list.Count / 2];
                }
    }
}
