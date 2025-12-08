using System.Drawing;

namespace Biometrics;

public class Image(string filename) : IImage
{
    public IImage Apply(IAlgorithm algorithm)
    {
        bmp = algorithm.Apply(bmp);
        return this;
    }

    internal Bitmap bmp = new(filename);
}