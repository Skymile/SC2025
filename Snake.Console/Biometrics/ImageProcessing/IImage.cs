using Biometrics.Core;

namespace Biometrics.ImageProcessing;

public interface IImage
{
    IImage Apply(IAlgorithm algorithm);
}
