using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing;

public sealed class PictureData(
        Bitmap bmp,
        BitmapData data
    ) : IDisposable
{
    public nint Scan0 => data.Scan0;
    public int Stride => data.Stride;

    public void Dispose() =>
        bmp.UnlockBits(data);
}
