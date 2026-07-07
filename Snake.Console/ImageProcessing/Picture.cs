using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Shapes;

namespace ImageProcessing;

public class ThinnedPicture(string filename, Picture picture) : Picture(filename, picture)
{
    public (string Filename, IReadOnlyDictionary<MinutiaeType, int> Minutaie) GetMinutiate()
    {
        using var data = LockBits(ImageLockMode.ReadOnly);
        byte[] bytes = new byte[Height * Stride];
        Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

        int offset = Stride + 3;
        var result = new Dictionary<MinutiaeType, int>() 
        {
            [MinutiaeType.Island     ] = 0,
            [MinutiaeType.Ending     ] = 0,
            [MinutiaeType.Line       ] = 0,
            [MinutiaeType.Crossing   ] = 0,
            [MinutiaeType.Bifurcation] = 0,
        };

        int[] offsets = [
            -Stride + 3 * -1,
            -Stride + 3 *  0,
            -Stride + 3 * +1,
                      3 * -1,
                      3 *  0,
                      3 * +1,
             Stride + 3 * -1,
             Stride + 3 *  0,
             Stride + 3 * +1,
        ];

        for (int i = offset; i < bytes.Length - offset; i += 3)
        {
            int count = 0;
            for (int o = 0; o < offsets.Length; o++)
                if (bytes[i + o] == 0)
                    ++count;
            count = Math.Min(count, 4);

            ++result[(MinutiaeType)count];
        }

        return (filename, result);
    }
}

public class Picture(string filename) : IPicture
{
    public Picture(string filename, Picture picture) : this(filename)
    {
        this.data = picture.data;
        this.bmp = picture.bmp;
    }

    public static Picture Create(string filename) => new(filename);

    public void Reset() => bmp = new(filename);

    public Picture Apply(MultimatrixTransformer transformer, Size size)
    {
        using var data = LockBits(ImageLockMode.ReadWrite);
        int stride = data.Stride;
        byte[] input = new byte[Height * Stride];
        byte[] output = new byte[input.Length];
        Marshal.Copy(data.Scan0, input, 0, input.Length);

        var matrix = new Pixel[size.Width * size.Height];

        int[] offsets = new int[size.Width * size.Height];
        for (int i = 0; i < offsets.Length; ++i)
            offsets[i] =
                ((i % size.Width) - size.Width / 2) * 3 +
                ((i / size.Width) - size.Height / 2) * stride;

        int minOffset = -offsets[0];

        for (int x = size.Width / 2; x < Width - size.Width / 2; x += size.Width)
            for (int y = size.Height / 2; y < Height - size.Height / 2; y += size.Height)
            {
                int o = x * 3 + y * stride;
                for (int i = 0; i < offsets.Length; i++)
                {
                    int offset = o + offsets[i];

                    matrix[i] = new Pixel(
                        input[offset + 0],
                        input[offset + 1],
                        input[offset + 2]
                    );
                }

                var result = transformer(matrix, size);

                for (int i = 0; i < offsets.Length; i++)
                {
                    int offset = o + offsets[i];

                    output[offset + 0] = result[i].R;
                    output[offset + 1] = result[i].G;
                    output[offset + 2] = result[i].B;
                }
            }

        Marshal.Copy(output, 0, data.Scan0, input.Length);
        return this;
    }

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
                ((i % size.Width) - size.Width / 2) * 3 +
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

    public Picture Apply(PixelPictureTransformer algorithm) =>
        Apply(i => algorithm(i, Width), false);

    public ThinnedPicture Apply(ThinningTransformer algorithm) =>
        new(filename, Apply(i => algorithm(i, Width), true));

    public Picture Apply(BinarizedPictureTransformer algorithm) =>
        Apply(i => algorithm(i, Width), true);

    private Picture Apply(Func<byte[], byte[]> algorithm, bool isBinarized)
    {
        if (isBinarized)
            Apply(Algorithms.Grayscale);

        using var data = LockBits(ImageLockMode.ReadWrite);
        byte[] bytes = new byte[Height * Stride];
        Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

        byte[] output = isBinarized
            ? CompactResult(algorithm, bytes)
            : algorithm(bytes);

        Marshal.Copy(output, 0, data.Scan0, bytes.Length);
        return this;
    }

    public PictureData LockBits(ImageLockMode lockMode) =>
        data = new PictureData(
            bmp,
            bmp.LockBits(
            new System.Drawing.Rectangle(Point.Empty, bmp.Size),
            lockMode,
            PixelFormat.Format24bppRgb
        ));

    public System.Drawing.Size Size => bmp.Size;
    public int Height => bmp.Height;
    public int Width => bmp.Width;
    public int Stride => data?.Stride ?? throw new InvalidOperationException();
    public int FormatSize => Image.GetPixelFormatSize(bmp.PixelFormat) / 8;

    protected byte[] CompactResult(Func<byte[], byte[]> algorithm, byte[] rgb)
    {
        byte[] binarized = new byte[Height * Width];
        for (int i = 0; i < binarized.Length; ++i)
            binarized[i] = rgb[i * 3];
        var result = algorithm(binarized);
        for (int i = 0; i < rgb.Length; i++)
            rgb[i] = result[i / 3];
        return rgb;
    }

    protected PictureData? data;
    protected Bitmap bmp = new(filename);
}
