using System.Drawing;

namespace Biometrics.Core;

public interface IAlgorithm
{
    Bitmap Apply(Bitmap readBitmap);
    unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height);
}