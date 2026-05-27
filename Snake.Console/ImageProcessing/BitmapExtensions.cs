using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing;

using BitmapSource = System.Windows.Media.Imaging.BitmapSource;
using MediaPixelFormats = System.Windows.Media.PixelFormats;

public static class BitmapExtensions
{
    public static BitmapSource ToBitmapSource(this Picture bitmap)
    {
        using var data = bitmap.LockBits(ImageLockMode.ReadOnly);
        byte[] arr = new byte[data.Stride * bitmap.Height];
        Marshal.Copy(data.Scan0, arr, 0, data.Stride * bitmap.Height);
        return BitmapSource.Create(
            bitmap.Width,
            bitmap.Height,
            96, 96,
            MediaPixelFormats.Bgr24,
            null,
            arr,
            bitmap.Width * bitmap.FormatSize
        );
    }
}