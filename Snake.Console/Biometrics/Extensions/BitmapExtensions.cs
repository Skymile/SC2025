using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

using Biometrics.Extensions;
using Biometrics.ImageProcessing;

namespace Biometrics.Extensions;

using Image = ImageProcessing.Image;
using MediaPixelFormats = System.Windows.Media.PixelFormats;

internal static class ImageExtensions
{
    internal static BitmapSource ToBitmapSource(this IImage image) =>
        BitmapExtensions.ToBitmapSource(((Image)image).bmp);
}

public static class BitmapExtensions
{
    public static BitmapSource ToBitmapSource(this Bitmap bitmap)
    {
        var data = bitmap.LockBits(
            new Rectangle(Point.Empty, bitmap.Size),
            ImageLockMode.ReadOnly,
            PixelFormat.Format24bppRgb
        );
        byte[] arr = new byte[data.Stride * bitmap.Height];
        Marshal.Copy(data.Scan0, arr, 0, data.Stride * bitmap.Height);
        bitmap.UnlockBits(data);
        return BitmapSource.Create(
            bitmap.Width,
            bitmap.Height,
            96, 96,
            MediaPixelFormats.Bgr24,
            null,
            arr,
            bitmap.Width * 
            System.Drawing.Image.GetPixelFormatSize(bitmap.PixelFormat) / 8
        );
    }
}