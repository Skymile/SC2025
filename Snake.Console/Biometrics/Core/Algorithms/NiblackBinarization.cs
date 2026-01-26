namespace Biometrics.Core.Algorithms;

public class NiblackBinarization : Algorithm
{
    public double K { get; set; } = -0.2;

    public virtual double Formulae(double mean, double std) =>
        mean + K * std;

    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        const int bpp = 3;
        int windowSize = 3;
        int border = windowSize / 2;
        int offset = bpp + stride;

        int* operations = stackalloc int[windowSize * windowSize];
        OperationMatrix.GetStandard3x3(operations, bpp, stride);

        for (int i = offset; i < length - offset; i++)
        {
            double mean = 0;
            for (int j = 0; j < 9; j++)
                mean += read[i + operations[j]];
            mean /= 9;

            double std = 0;
            for (int j = 0; j < 9; j++)
            {
                var diff = read[i + operations[j]] - mean;
                std += diff * diff;
            }

            var value = (byte)double.Clamp(Formulae(mean, std), 0.0, 255.0);

            write[i] = read[i] > value ? byte.MinValue : byte.MaxValue;
        }
    }
}
