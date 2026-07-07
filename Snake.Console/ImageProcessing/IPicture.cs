using System.Drawing.Imaging;

namespace ImageProcessing;

public interface IPicture
{
    int FormatSize { get; }
    int Height { get; }
    System.Drawing.Size Size { get; }
    int Stride { get; }
    int Width { get; }

    Picture Apply(BinarizedPictureTransformer algorithm);
    Picture Apply(MatrixTransformer transformer, Size size);
    Picture Apply(MultimatrixTransformer transformer, Size size);
    Picture Apply(PixelPictureTransformer algorithm);
    Picture Apply(PixelTransformer transformer);
    PictureData LockBits(ImageLockMode lockMode);
    void Reset();
}