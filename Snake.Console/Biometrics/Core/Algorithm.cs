using System.Drawing;
using System.Drawing.Imaging;

namespace Biometrics.Core;

public abstract class Algorithm : IAlgorithm
{
    public abstract unsafe void Apply(
        byte* read,
        byte* write,
        int length,
        int stride,
        int width,
        int height
    );

    public unsafe Bitmap Apply(Bitmap readBitmap)
    {
        var writeBitmap = new Bitmap(
            readBitmap.Width, readBitmap.Height, readBitmap.PixelFormat
        );

        var readData = readBitmap.LockBits(
            new(Point.Empty, readBitmap.Size),
            ImageLockMode.ReadOnly,
            readBitmap.PixelFormat
        );
        var writeData = writeBitmap.LockBits(
            new(Point.Empty, writeBitmap.Size),
            ImageLockMode.WriteOnly,
            writeBitmap.PixelFormat
        );

        byte* read = (byte*)readData.Scan0.ToPointer();
        byte* write = (byte*)writeData.Scan0.ToPointer();

        Apply(read,
              write,
              readData.Stride * readData.Height,
              readData.Stride,
              readData.Width,
              readData.Height);

        readBitmap.UnlockBits(readData);
        writeBitmap.UnlockBits(writeData);
        return writeBitmap;
    }
}
