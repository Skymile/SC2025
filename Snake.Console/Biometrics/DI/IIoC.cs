namespace Biometrics.DI;

public interface IIoC
{
    T Inject<T>() where T : class;
    
    IIoC Register<TInterface, TImplementation>(TImplementation? data = null)
        where TInterface : class
        where TImplementation : class;

    IIoC Set(IoCLifetime lifetime);
}