namespace ImageProcessing;

public delegate byte[] ThinningTransformer        (byte[] data, int width);
public delegate byte[] PixelPictureTransformer    (byte[] data, int width);
public delegate byte[] BinarizedPictureTransformer(byte[] data, int width);
public delegate Pixel[] MultimatrixTransformer(Pixel[] input, Size size);
public delegate Pixel MatrixTransformer(Pixel[] input, Size size);
public delegate Pixel PixelTransformer(Pixel input);

public enum MinutiaeType
{
    Island,
    Ending,
    Line,
    Crossing,
    Bifurcation
}

public static class Algorithms
{
    private static readonly HashSet<int> A0 = [3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254];
    private static readonly HashSet<int> A1 = [7, 14, 28, 56, 112, 131, 193, 224];
    private static readonly HashSet<int> A2 = [7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135, 193, 195, 224, 225, 240];
    private static readonly HashSet<int> A3 = [7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120, 124, 131, 135, 143, 193, 195, 199, 224, 225, 227, 240, 241, 248];
    private static readonly HashSet<int> A4 = [7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 193, 195, 199, 207, 224, 225, 227, 231, 240, 241, 243, 248, 249, 252];
    private static readonly HashSet<int> A5 = [7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 191, 193, 195, 199, 207, 224, 225, 227, 231, 239, 240, 241, 243, 248, 249, 251, 252, 254];
    private static readonly HashSet<int> A1pix = [3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254];

    private const byte Black = 1;
    private const byte White = 0;

    private unsafe static int GetTransition2(byte* ptr, int width)
    {
        byte[] values = [
            ptr[-width + 0],
            ptr[-width + 1],
            ptr[+1        ],
            ptr[+width + 1],
            ptr[+width + 0],
            ptr[+width - 1],
            ptr[-1        ],
            ptr[-width - 1],
            ptr[-width + 0],
        ];

        int count = 0;

        for (int i = 0; i < values.Length - 1; i++)
            if (values[i] == White && values[i + 1] == Black)
                ++count;

        return count;
    }

    private unsafe static int GetTransition(byte* ptr, int width) =>
        (ptr[-width + 0] == White && ptr[-width + 1] == Black ? 1 : 0) +
        (ptr[-width + 1] == White && ptr[+1        ] == Black ? 1 : 0) +
        (ptr[+1        ] == White && ptr[+width + 1] == Black ? 1 : 0) +
        (ptr[+width + 1] == White && ptr[+width + 0] == Black ? 1 : 0) +
        (ptr[+width + 0] == White && ptr[+width - 1] == Black ? 1 : 0) +
        (ptr[+width - 1] == White && ptr[-1        ] == Black ? 1 : 0) +
        (ptr[-1        ] == White && ptr[-width - 1] == Black ? 1 : 0) +
        (ptr[-width - 1] == White && ptr[-width + 0] == Black ? 1 : 0);

    private unsafe static int CountBlackNeighbours(byte* ptr, int width) =>
        ptr[-width + 0] +
        ptr[-width + 1] +
        ptr[       + 1] +
        ptr[+width + 1] +
        ptr[+width + 0] +
        ptr[+width - 1] +
        ptr[       - 1] +
        ptr[-width - 1];

    private unsafe static int GetWeight(byte* ptr, int width) =>
        (ptr[-width + 0] << 0) |
        (ptr[-width + 1] << 1) |
        (ptr[       + 1] << 2) |
        (ptr[+width + 1] << 3) |
        (ptr[+width + 0] << 4) |
        (ptr[+width - 1] << 5) |
        (ptr[       - 1] << 6) |
        (ptr[-width - 1] << 7);

    public unsafe static readonly ThinningTransformer ZhangSuen =
        (input, width) =>
        {
            Func<bool[], bool>[] oneIsWhite = [
                p => (p[2] || p[4] || p[6]) &&
                     (p[4] || p[6] || p[8]),
                p => (p[2] || p[4] || p[8]) &&
                     (p[2] || p[6] || p[8])
            ];

            int o = width + 1;
            fixed (byte* p = input)
            {
                for (int i = 0; i < input.Length; i++)
                    p[i] = p[i] == byte.MaxValue ? White : Black;

                var indices = new List<int>();
                do
                {
                    for (int j = 0; j < 2; j++)
                    {
                        indices.Clear();
                        for (int i = o; i < input.Length - o; ++i)
                        {
                            if (p[i] != Black)
                                continue;

                            byte* ptr = p + i;

                            bool[] P = [
                                false,
                                ptr[-width + 0] == White,
                                ptr[-width + 1] == White,
                                ptr[+1        ] == White,
                                ptr[+width + 1] == White,
                                ptr[+width + 0] == White,
                                ptr[+width - 1] == White,
                                ptr[-1        ] == White,
                                ptr[-width - 1] == White,
                            ];
                            int B = CountBlackNeighbours(p + i, width);
                            int A = GetTransition(p + i, width);

                            if (2 <= B && B <= 6 &&
                                A == 1 &&
                                oneIsWhite[j](P))
                                indices.Add(i);
                        }

                        foreach (var index in indices)
                            p[index] = White;
                    }

                } while (indices.Count > 0);

                for (int i = 0; i < input.Length; i++)
                    p[i] = p[i] == White ? byte.MaxValue : byte.MinValue;
            }

            return input;
        };

    public unsafe static readonly ThinningTransformer K3M = 
        (input, width) =>
        {
            var border = new HashSet<int>();

            int o = width + 1;
            fixed (byte* p = input)
            {
                for (int i = 0; i < input.Length; i++)
                    p[i] = p[i] == byte.MaxValue ? White : Black;

                while (true)
                {
                    for (int i = o; i < input.Length - o; ++i)
                        if (p[i] == Black)
                        {
                            int weight = GetWeight(p + i, width);

                            if (A0.Contains(weight))
                                border.Add(i);
                        }

                    if (border.Count <= 0)
                        break;

                    HashSet<int>[] phases = [A1, A2, A3, A4, A5, A1pix];
                    foreach (var Ai in phases)
                        foreach (var i in border)
                        {
                            int weight = GetWeight(p + i, width);

                            if (Ai.Contains(weight))
                                p[i] = White;
                        }

                    border.Clear();
                }

                for (int i = o; i < input.Length - o; ++i)
                    if (p[i] == Black)
                    {
                        int weight = GetWeight(p + i, width);

                        if (A1pix.Contains(weight))
                            p[i] = White;
                    }

                for (int i = 0; i < input.Length; i++)
                    p[i] = p[i] == White ? byte.MaxValue : byte.MinValue;
            }

            return input;
        };

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

    public static readonly PixelTransformer Grayscale = i =>
        new((byte)i.Average);

    public static readonly PixelTransformer R = i => new(i.R, 0, 0);
    public static readonly PixelTransformer G = i => new(0, i.G, 0);
    public static readonly PixelTransformer B = i => new(0, 0, i.B);

    public static MultimatrixTransformer Pixelization =
        (input, size) =>
        {
            double avgR = 0.0;
            double avgG = 0.0;
            double avgB = 0.0;

            for (int i = 0; i < input.Length; i++)
            {
                avgR += input[i].R;
                avgG += input[i].G;
                avgB += input[i].B;
            }

            avgR /= input.Length;
            avgG /= input.Length;
            avgB /= input.Length;

            var pixel = new Pixel((byte)avgR, (byte)avgG, (byte)avgB);
            var output = new Pixel[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = pixel;
            return output;
        };

    public static MatrixTransformer GetNiblackAlgorithm(
            Func<double, double, double> formula
        ) =>
        (input, size) =>
        {
            double std = 0.0;
            double mean = 0.0;

            for (int i = 0; i < input.Length; i++)
                mean += input[i].Average;
            mean /= input.Length;
            for (int i = 0; i < input.Length; i++)
                std = (input[i].Average - mean) * (input[i].Average - mean);
            std = Math.Sqrt(std);

            return new Pixel(
                formula(mean, std) > input[input.Length / 2].Average
                    ? byte.MaxValue
                    : byte.MinValue
            );
        };

    public static readonly MatrixTransformer Niblack = GetNiblackAlgorithm(
        (mean, std) => mean + 0.8 * std);

    public static readonly MatrixTransformer Sauvola = GetNiblackAlgorithm(
        (mean, std) => mean * (1 - 0.8 * (1 - 1.2 / std)));

    public static MatrixTransformer Phansalkar(
            double k = 0.8,
            double p = 1.2
        ) => GetNiblackAlgorithm(
            (mean, std) => mean * (1 + k * (p / std - 1))
        );

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
