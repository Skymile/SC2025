using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Biometrics.Core;

namespace Biometrics.xTests;

public class UnitTest1
{
    [Fact]
    public void Biometrics_Example()
    {
        byte[] input = [
            0, 0, 0,
            255, 255, 255,
            127, 127, 127,
            50, 100, 150
        ];
        byte[] expected = [
            0, 0, 0,
            255, 255, 255,
            0, 0, 0,
            0, 0, 0,
        ];

        var output = TestAlgorithm(new Grayscale(), input, 1, 4);
        output = TestAlgorithm(new ThresholdBinarization(), output, 1, 4);

        for (int i = 0; i < input.Length; i++)
            Assert.Equal(expected[i], output[i]);
    }

    [Fact]
    public void Grayscale_Test()
    {
        byte[] input = [
            0, 0, 0,
            255, 255, 255,
            127, 127, 127,
            50, 100, 150
        ];

        var output = TestAlgorithm(new Grayscale(), input, 1, 4);

        for (int i = 0; i < input.Length; i += 3)
        {
            int sumInput  = input[i] + input[i + 1] + input[i + 2];
            int sumOutput = output[i] + output[i + 1] + output[i + 2];

            Assert.Equal(sumInput, sumOutput);
            Assert.Equal(output[i], output[i + 1]);
            Assert.Equal(output[i + 1], output[i + 2]);
        }
    }

    private unsafe byte[] TestAlgorithm(
        IAlgorithm algorithm, byte[] input, int width, int height)
    {
        var output = new byte[input.Length];
        fixed (byte* inputPtr = input)
        fixed (byte* outputPtr = output)
            algorithm.Apply(inputPtr, outputPtr,
                input.Length,
                width * 3,
                width,
                height
            );
        return output;
    }
}
