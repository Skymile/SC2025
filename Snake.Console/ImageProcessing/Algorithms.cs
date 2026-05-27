namespace ImageProcessing;

public delegate Pixel MatrixTransformer(Pixel[] input, Size size);
public delegate Pixel PixelTransformer(Pixel input);

public static class Algorithms
{
    public static MatrixTransformer GetConvolutionTransformer(
            int[] convolution, int width = 3, int height = 3) =>
        (input, size) =>
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(size, new(width, height));

            double valueR = 0.0;
            double valueG = 0.0;
            double valueB = 0.0;

            for (int i = 0; i < size.Length; i++)
            {
                valueR += input[i].R * convolution[i];
                valueG += input[i].G * convolution[i];
                valueB += input[i].B * convolution[i];
            }

            double sum = convolution.Sum();

            return new(
                (byte)(valueR / sum),
                (byte)(valueG / sum),
                (byte)(valueB / sum)
            );
        };

    public static readonly PixelTransformer ThresholdBinarization =
        i => new(i.Average > 128 ? byte.MaxValue : byte.MinValue);

    public static readonly PixelTransformer Mean =
        i => new((byte)i.Average);

    public static readonly PixelTransformer R = i => new(i.R, 0, 0);
    public static readonly PixelTransformer G = i => new(0, i.G, 0);
    public static readonly PixelTransformer B = i => new(0, 0, i.B);

    public static readonly MatrixTransformer Sharpen =
        GetConvolutionTransformer([
             0, -1,  0,
            -1,  5, -1,
             0, -1,  0
        ]);

    public static readonly MatrixTransformer Emboss =
        GetConvolutionTransformer([
             1,  0, 0,
             0, -2, 0,
             0,  0, 0
        ]);

    // Blur
    public static readonly MatrixTransformer GaussianBlur =
        GetConvolutionTransformer([
             1,  2,  1,
             2,  4,  2,
             1,  2,  1
        ]);

    public static readonly MatrixTransformer BoxBlur =
        GetConvolutionTransformer([
             1, 1, 1,
             1, 1, 1,
             1, 1, 1
        ]);

    // Edge detection
    public static readonly MatrixTransformer Laplacian4 =
        GetConvolutionTransformer([
          0, -1,  0,
         -1,  4, -1,
          0, -1,  0
    ]);

    public static readonly MatrixTransformer Laplacian8 =
        GetConvolutionTransformer([
         -1, -1, -1,
         -1,  8, -1,
         -1, -1, -1
    ]);

    public static readonly MatrixTransformer SobelHorizontal =
        GetConvolutionTransformer([
             -1, -2, -1,
              0,  0,  0,
             -1, -2, -1
        ]);


    public static readonly MatrixTransformer SobelVertical =
        GetConvolutionTransformer([
             -1, 0, 1,
             -2, 0, 2,
             -1, 0, 1
        ]);

    public static readonly MatrixTransformer SobelDiagonal =
        GetConvolutionTransformer([
             -2, -1, 0,
             -1,  0, 1,
              0,  1, 2
        ]);

    public static readonly MatrixTransformer SobelCounterdiagonal =
        GetConvolutionTransformer([
              0,  1, 2,
             -1,  0, 1,
             -2, -1, 0
        ]);

    public static readonly MatrixTransformer PrewittHorizontal =
        GetConvolutionTransformer([
             -1, -1, -1,
              0,  0,  0,
             -1, -1, -1
        ]);


    public static readonly MatrixTransformer PrewittVertical =
        GetConvolutionTransformer([
             -1, 0, 1,
             -1, 0, 1,
             -1, 0, 1
        ]);

    public static readonly MatrixTransformer PrewittDiagonal =
        GetConvolutionTransformer([
             -1, -1, 0,
             -1,  0, 1,
              0,  1, 1
        ]);

    public static readonly MatrixTransformer PrewittCounterdiagonal =
        GetConvolutionTransformer([
              0,  1, 1,
             -1,  0, 1,
             -1, -1, 0
        ]);
    //

    public static readonly MatrixTransformer Median =
        (input, size) => input.OrderBy(i => i.Average)
            .ElementAt(size.Length / 2);

    public static readonly MatrixTransformer Dilation = 
        (input, _) => input.OrderBy(i => i.Average).Last();

    public static readonly MatrixTransformer Erosion = 
        (input, _) => input.OrderBy(i => i.Average).First();
}
