using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Biometrics;

public class Image
{
    public static Bitmap Apply(Bitmap bmp)
    {
        var data = bmp.LockBits(
            new(Point.Empty, bmp.Size),
            ImageLockMode.ReadWrite,
            bmp.PixelFormat
        );

        byte[] bytes = new byte[data.Stride * bmp.Height];
        Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

        for (int j = 50; j < 80; j++)
            for (int i = 0; i < bmp.Width; i++)
            {
                int index = i * 3 + j * data.Stride; 

                bytes[index + 0] = 0;
                bytes[index + 1] = 0;
                bytes[index + 2] = 255;
            }

        Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
        bmp.UnlockBits(data);
        return bmp;
    }
}