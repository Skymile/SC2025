namespace Biometrics.Core;

public static class OperationMatrix
{
    public unsafe static void GetStandard3x3(int* ptr, int bpp, int stride)
    {
        int i = 0;
        ptr[i++] = bpp * -1 + stride * -1;
        ptr[i++] = bpp *  0 + stride * -1;
        ptr[i++] = bpp *  1 + stride * -1;
        ptr[i++] = bpp * -1 + stride *  0;
        ptr[i++] = bpp *  0 + stride *  0;
        ptr[i++] = bpp *  1 + stride *  0;
        ptr[i++] = bpp * -1 + stride * +1;
        ptr[i++] = bpp *  0 + stride * +1;
        ptr[i++] = bpp *  1 + stride * +1;
    }
}
