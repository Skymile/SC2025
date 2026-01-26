namespace Biometrics.Core.Algorithms;

public class SauvolaBinarization : NiblackBinarization
{
    public SauvolaBinarization() => K = 0.5;

    public double R { get; set; } = 128.0;

    public override double Formulae(double mean, double std) =>
        mean * (1 + K * ((std / R) - 1));
}
