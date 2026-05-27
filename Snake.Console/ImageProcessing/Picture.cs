using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing;

public class Picture(string filename)
{
    public void Reset() => bmp = new(filename);

    public Picture Apply(MatrixTransformer transformer, Size size)
    {
        using var data = LockBits(ImageLockMode.ReadWrite);
        int stride = data.Stride;
        byte[] input = new byte[Height * Stride];
        byte[] output = new byte[input.Length];
        Marshal.Copy(data.Scan0, input, 0, input.Length);

        int[] offsets = new int[size.Width * size.Height];
        for (int i = 0; i < offsets.Length; ++i)
            offsets[i] =
                ((i % size.Width) - size.Width  / 2) * 3 +
                ((i / size.Width) - size.Height / 2) * stride;

        int minOffset = -offsets[0];
        var matrix = new Pixel[size.Width * size.Height];

        for (int i = minOffset; i < input.Length - minOffset; i += 3)
        {
            for (int o = 0; o < offsets.Length; ++o)
                matrix[o] = new(
                    input[i + offsets[o] + 0],
                    input[i + offsets[o] + 1],
                    input[i + offsets[o] + 2]
                );

            var pix = transformer(matrix, size);

            output[i + 0] = pix.R;
            output[i + 1] = pix.G;
            output[i + 2] = pix.B;
        }

        Marshal.Copy(output, 0, data.Scan0, input.Length);
        return this;
    }

    public Picture Apply(PixelTransformer transformer)
    {
        using var data = LockBits(ImageLockMode.ReadWrite);
        byte[] bytes = new byte[Height * Stride];
        Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

        for (int i = 0; i < bytes.Length; i += 3)
        {
            var output = transformer(new(
                bytes[i + 2],
                bytes[i + 1],
                bytes[i + 0]
            ));

            bytes[i + 0] = output.B;
            bytes[i + 1] = output.G;
            bytes[i + 2] = output.R;
        }

        Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
        return this;
    }

    public PictureData LockBits(ImageLockMode lockMode) =>
        data = new PictureData(
            bmp,
            bmp.LockBits(
            new Rectangle(Point.Empty, bmp.Size),
            lockMode,
            PixelFormat.Format24bppRgb
        ));

    public System.Drawing.Size Size => bmp.Size;
    public int Height => bmp.Height;
    public int Width => bmp.Width;
    public int Stride => data?.Stride ?? throw new InvalidOperationException();
    public int FormatSize => Image.GetPixelFormatSize(bmp.PixelFormat) / 8;

    private PictureData? data;
    private Bitmap bmp = new(filename);
}
