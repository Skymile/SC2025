namespace Keystrokes.Core.Models;

public delegate double DistanceCallback(double[] left, double[] right);

public record Distances
{
    public static DistanceCallback Manhattan = 
        Create((i, j) => Math.Abs(i - j), data => data.Sum());
    public static DistanceCallback Chebyshev = 
        Create((i, j) => Math.Abs(i - j), data => data.Max());
    public static DistanceCallback Euclidean = 
        Create((i, j) => Math.Sqrt(i * i - j * j), data => data.Sum());

    private static DistanceCallback Create(
        Func<double, double, double> func, Func<IEnumerable<double>, double> flatten) =>
        (left, right) => flatten(left.Zip(right, func));
}
