using Biometrics.Validations.Base;

namespace Biometrics.Core.Algorithms;

public class PhansalkarBinarization : SauvolaBinarization
{
    public static Validation<PhansalkarBinarization> TryCreate(
            string k,
            string p,
            string q
        ) => Validation.Validate(
            nameof(PhansalkarBinarization),
            () => new PhansalkarBinarization
            {
                K = double.Parse(k),
                P = double.Parse(p),
                Q = double.Parse(q)
            },

            double.TryParse(k, out _)
                ? null : new Error(nameof(K), "K must be a valid double"),
            double.TryParse(p, out _)
                ? null : new Error(nameof(P), "P must be a valid double"),
            double.TryParse(q, out _)
                ? null : new Error(nameof(Q), "Q must be a valid double"),

            double.TryParse(k, out var kk) && kk is > -1 and < 1
                ? null : new Error(nameof(K), "K must be in the range (-1, 1)"),
            double.TryParse(p, out var pp) && pp is > 0 and < 100
                ? null : new Error(nameof(P), "P must be in the range (0, 100)"),
            double.TryParse(q, out var qq) && qq is > 0 and < 100
                ? null : new Error(nameof(Q), "Q must be in the range (0, 100)")
        );

    public PhansalkarBinarization() => K = 0.25;
    public double P { get; set; } = 2;
    public double Q { get; set; } = 10;

    public override double Formulae(double mean, double std) =>
        mean * (1 + P * Math.Pow(Math.E, -Q * mean) + K * ((std / R) - 1));
}
