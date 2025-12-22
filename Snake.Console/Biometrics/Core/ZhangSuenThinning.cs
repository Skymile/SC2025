using System.Runtime.CompilerServices;

namespace Biometrics.Core;

public class ZhangSuenThinning : Algorithm
{
    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        const byte Black = 0;
        const byte White = 255;

        int border = stride + 3;

        for (int i = 0; i < length; i++)
            write[i] = read[i];

        bool Step(bool isOdd)
        {
            var indicesToDelete = new List<int>();

            for (int i = border; i < length - border; i += 3)
            {
                if (write[i] == White) continue;

                byte[] p = GetIndices(write + i, stride, 3);

                int A = 0;
                for (int j = 2; j < 9; j++)
                    if (p[j] == White && p[j + 1] == Black)
                        ++A;
                if (p[9] == White && p[2] == Black)
                    ++A;

                int B = 0;
                for (int j = 2; j < 10; j++)
                    if (p[j] == Black)
                        ++B;

                bool condition12 = 2 <= B && B <= 6 && A == 1;
                bool condition3 =
                    p[2] == Black ||
                    p[4] == Black ||
                    p[6] == Black;
                bool condition4 =
                    p[4] == Black ||
                    p[6] == Black ||
                    p[8] == Black;

                if (condition12 && (
                    (isOdd && condition3) || 
                    (!isOdd && condition4)))
                    indicesToDelete.Add(i);
            }

            foreach (var i in indicesToDelete)
                write[i + 0] =
                write[i + 1] =
                write[i + 2] = White;
            return indicesToDelete.Count > 0;
        }

        bool isAnyChange = true;
        bool isOdd = true;
        while (isAnyChange)
            isAnyChange = Step(isOdd = !isOdd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetIndex(int x, int y, int stride, int bpp) =>
        x * 3 + y * stride;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private unsafe static byte[] GetIndices(byte* ptr, int stride, int bpp) =>
    [
        0,
        ptr[GetIndex( 0,  0, stride, bpp)], // p1
        ptr[GetIndex( 0, -1, stride, bpp)],
        ptr[GetIndex( 1, -1, stride, bpp)],
        ptr[GetIndex( 1,  0, stride, bpp)],
        ptr[GetIndex( 1,  1, stride, bpp)],
        ptr[GetIndex( 0,  1, stride, bpp)],
        ptr[GetIndex(-1,  1, stride, bpp)],
        ptr[GetIndex(-1,  0, stride, bpp)], 
        ptr[GetIndex(-1, -1, stride, bpp)], // p9
    ];
}
