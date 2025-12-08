using System.Drawing;
using System.Drawing.Imaging;

namespace Biometrics;

public class Image
{
    public unsafe static Bitmap Apply(Bitmap readBitmap)
    {
        int windowSize = 3;

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

        var list = new List<byte>();
        int border = windowSize / 2;

        for (int y = border; y < readBitmap.Height - border; y++)
            for (int x = border; x < readBitmap.Width - border; x++)
            {
                int i = x * 3 + y * readData.Stride;

                list.Clear();

                for (int yy = -border; yy < border; yy++)
                    for (int xx = -border; xx < border; xx++)
                    {
                        int offset = xx * 3 + yy * readData.Stride;

                        list.Add(read[i + offset]);
                    }

                list.Sort();
                write[i + 0] = 
                write[i + 1] = 
                write[i + 2] = list[list.Count / 2];
            }

        readBitmap.UnlockBits(readData);
        writeBitmap.UnlockBits(writeData);
        return writeBitmap;
    }
}