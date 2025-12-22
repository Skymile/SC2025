namespace Biometrics.Core;

public class OtsuBinarization : Algorithm
{
    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        int[] histogram = new int[256];
        for (int i = 0; i < length; i += 3)
            ++histogram[(
                read[i + 0] +
                read[i + 1] +
                read[i + 2]
            ) / 3];

        float sum = 0;
        float sumB = 0;
        for (int i = 0; i < 256; i++)
            sum += i * histogram[i];

        float varMax = 0;
        int back = 0;
        int threshold = 0;

        for (int i = 0; i < 256; i++)
        {
            back += histogram[i];
            int fore = length / 3 - back;

            if (back == 0 || fore == 0)
                continue;

            sumB += i * histogram[i];

            float backMean = sumB / back;
            float foreMean = (sum - sumB) / fore;
            float variance = back * fore * (backMean - foreMean) * (backMean - foreMean);

            if (variance > varMax)
            {
                varMax = variance;
                threshold = i;
            }
        }

        new ThresholdBinarization()
        {
            Threshold = (byte)threshold
        }.Apply(read, write, length, stride, width, height);
    }
}
