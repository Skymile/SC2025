namespace Snake.Core.Services;

public enum IoCLifetime
{
    Singleton, Transient
}

public class IoC 
    // Inversion of Control 
    // Dependency Injection
{
    public T Inject<T>() where T : class =>
        (T)factory[typeof(T)]();

    public IoC Set(IoCLifetime lifetime) 
    {
        this.lifetime = lifetime;
        return this;
    }

    public IoC Register<TInterface, TImplementation>(
            TImplementation? data = null)
        where TInterface : class 
        where TImplementation : class
    {
        mapper[typeof(TInterface)] = typeof(TImplementation);

        switch (lifetime)
        {
            case IoCLifetime.Singleton:
                var singleton = data ??
                    Construct(typeof(TImplementation)) ??
                    throw new KeyNotFoundException(typeof(TImplementation).Name);
                factory[typeof(TInterface)] = () => singleton;
                break;

            case IoCLifetime.Transient:
                factory[typeof(TInterface)] = () =>
                    data ?? 
                    Construct(typeof(TImplementation)) ??
                    throw new KeyNotFoundException(typeof(TImplementation).Name);
                break;

            default: throw new NotImplementedException();
        }

        return this;
    }

    private object Construct(Type type) 
    {
        var ctor = type.GetConstructors().SingleOrDefault();
        if (ctor == null)
            type = mapper[type];
        ctor = type.GetConstructors().Single();
        var parameters = ctor.GetParameters();
        if (parameters.Length == 0)
            return ctor.Invoke([]);

        var objects = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
            objects[i] = Construct(
                parameters[i].ParameterType
            );

        return ctor.Invoke(objects);
    }

    private readonly Dictionary<Type, Type> mapper = [];
    private readonly Dictionary<Type, Func<object>> factory = [];
    private IoCLifetime lifetime;
}
