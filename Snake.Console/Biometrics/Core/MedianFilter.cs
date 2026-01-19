namespace Biometrics.Core;

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

        int* operations = stackalloc int[windowSize * windowSize];
        OperationMatrix.GetStandard3x3(operations, bpp, stride);

        for (int i = offset; i < length - offset; i++)
        {
            list.Clear();

            for (int j = 0; j < 9; j++)
                list.Add(read[i + operations[j]]);

            list.Sort();
            write[i] = list[list.Count / 2];
        }
    }
}
