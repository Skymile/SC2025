namespace Biometrics.Core;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class SliderAttribute : Attribute
{
    public SliderAttribute(int min, int max)
    {
        this.Min = min;
        this.Max = max;
    }

    public int Min { get; }
    public int Max { get; }
}
