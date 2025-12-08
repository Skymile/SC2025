using System.Drawing;
using System.Drawing.Imaging;

namespace Biometrics;

public class Grayscale : Algorithm
{
    public override unsafe void Apply(byte* read, byte* write, int length, int stride, int width, int height)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                int i = x * 3 + y * stride;
                int value =
                    read[i + 0] +
                    read[i + 1] +
                    read[i + 2];

                write[i + 0] =
                write[i + 1] =
                write[i + 2] = (byte)(value / 3);
            }
    }
}

public class MedianFilter : Algorithm
{
    public override unsafe void Apply(
        byte* read, byte* write, int length, int stride, int width, int height)
    {
        int windowSize = 3;
        var list = new List<byte>();
        int border = windowSize / 2;

        for (int k = 0; k < 3; k++)
            for (int y = border; y < height - border; y++)
                for (int x = border; x < width - border; x++)
                {
                    int i = x * 3 + y * stride;

                    list.Clear();

                    for (int yy = -border; yy < border; yy++)
                        for (int xx = -border; xx < border; xx++)
                        {
                            int offset = xx * 3 + yy * stride;

                            list.Add(read[i + offset + k]);
                        }

                    list.Sort();
                    write[i + k] = list[list.Count / 2];
                }
    }
}

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

public interface IImage
{
    IImage Apply(IAlgorithm algorithm);
}

public class Image(string filename) : IImage
{
    public IImage Apply(IAlgorithm algorithm)
    {
        bmp = algorithm.Apply(bmp);
        return this;
    }

    internal Bitmap bmp = new(filename);
}