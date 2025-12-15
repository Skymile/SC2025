namespace Biometrics;

public class MedianFilter : Algorithm
{
    public override unsafe void Apply(
        byte* read, byte* write, int length, int stride, int width, int height)
    {
        const int bpp = 3;
        int windowSize = 3;
        var list = new List<byte>();
        int border = windowSize / 2;
        int offset = bpp + stride;

        int[] operationOffsets = [
            bpp * -1 + stride * -1, bpp * 0 + stride * -1, bpp * 1 + stride * -1,
            bpp * -1 + stride *  0, bpp * 0 + stride *  0, bpp * 1 + stride *  0,
            bpp * -1 + stride * +1, bpp * 0 + stride * +1, bpp * 1 + stride * +1,
        ];

        for (int i = offset; i < length - offset; i++)
        {
            list.Clear();

            for (int j = 0; j < operationOffsets.Length; j++)
                list.Add(read[i + operationOffsets[j]]);

            list.Sort();
            write[i] = list[list.Count / 2];
        }
    }
}
