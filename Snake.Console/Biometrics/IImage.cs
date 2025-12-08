namespace Biometrics;

public interface IImage
{
    IImage Apply(IAlgorithm algorithm);
}
