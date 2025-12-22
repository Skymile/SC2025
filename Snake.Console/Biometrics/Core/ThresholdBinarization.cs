namespace Biometrics.Core;

public class ThresholdBinarization : Algorithm
{
    public byte Threshold { get; set; } = 128;

    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        for (int i = 0; i < length; i += 3)
            write[i + 0] = 
            write[i + 1] = 
            write[i + 2] = ((
                    read[i + 0] + 
                    read[i + 1] + 
                    read[i + 2]
                ) / 3) > Threshold ? byte.MaxValue : byte.MinValue;
    }
}
