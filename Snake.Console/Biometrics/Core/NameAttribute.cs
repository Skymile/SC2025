namespace Biometrics.Core;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class NameAttribute : Attribute
{
    public NameAttribute(string name) =>
        this.Name = name;

    public string Name { get; set; }
}
