using System.Drawing;

using Biometrics.Core;

namespace Biometrics.ImageProcessing;

public class Image(string filename) : IImage
{
    public IImage Apply(IAlgorithm algorithm)
    {
        bmp = algorithm.Apply(bmp);
        return this;
    }

    internal Bitmap bmp = new(filename);
}