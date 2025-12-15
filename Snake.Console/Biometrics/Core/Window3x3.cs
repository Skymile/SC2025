namespace Biometrics.Core;

public static class Window3x3
{
    public static readonly double[] GaussianBlur = [
        1, 2, 1,
        2, 4, 2,
        1, 2, 1,
    ];

    public static readonly double[] BoxBlur = [
        1, 1, 1,
        1, 1, 1,
        1, 1, 1,
    ];

    public static readonly double[] Sharpen = [
         0, -1,  0,
        -1,  5, -1,
         0, -1,  0,
    ];

    public static readonly double[] Laplacian8 = [
        -1, -1, -1,
        -1,  8, -1,
        -1, -1, -1,
    ];

    public static readonly double[] Laplacian4 = [
         0, -1,  0,
        -1,  4, -1,
         0, -1,  0,
    ];

    public static readonly double[] Emboss = [
        -1, 0, 0,
         0, 2, 0,
         0, 0, 0,
    ];

    public static readonly double[] SobelVertical = [
        -1, 0, 1,
        -2, 0, 2,
        -1, 0, 1,
    ];

    public static readonly double[] SobelHorizontal = [
        -1, -2, -1,
         0,  0,  0,
         1,  2,  1,
    ];

    public static readonly double[] SobelDiagonal = [
        -2, -1, 0,
        -1,  0, 1,
         0,  1, 2,
    ];

    public static readonly double[] SobelCounterdiagonal = [
         0, -1, -2,
         1,  0, -1,
         2,  1,  0,
    ];


    public static readonly double[] PrewittVertical = [
        -1, 0, 1,
        -1, 0, 1,
        -1, 0, 1,
    ];

    public static readonly double[] PrewittHorizontal = [
        -1, -1, -1,
         0,  0,  0,
         1,  1,  1,
    ];

    public static readonly double[] PrewittDiagonal = [
        -1, -1, 0,
        -1,  0, 1,
         0,  1, 1,
    ];

    public static readonly double[] PrewittCounterdiagonal = [
         0, -1, -1,
         1,  0, -1,
         1,  1,  0,
    ];
}
