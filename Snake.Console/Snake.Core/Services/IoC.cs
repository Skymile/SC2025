namespace Snake.Core.Services;

public class IoC // Inversion of Control // Dependency Injection
{
    public T Inject<T>() where T : class =>
        (T)singletons[typeof(T)];

    public IoC RegisterSingleton<TInterface, TImplementation>(
            TImplementation? data = null)
        where TInterface : class 
        where TImplementation : class
    {
        singletons[typeof(TInterface)] = 
            data ?? 
            Construct<TImplementation>() ??
            throw new KeyNotFoundException(typeof(TImplementation).Name);

        return this;
    }

    private T Construct<T>() 
        where T : class =>
        Activator.CreateInstance<T>();

    private readonly Dictionary<Type, object> singletons = [];
}
