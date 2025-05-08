
public class IoC
{
    public TInterface GetService<TInterface>() =>
        (TInterface)singleton[typeof(TInterface)];

    public IoC RegisterSingleton<TInstance>()
    {
        var type = typeof(TInstance);
        var ctors = type.GetConstructors();

        var ctor = ctors.Single();
        var parameters = ctor.GetParameters();
        var objects = new object[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            if (!singleton.TryGetValue(
                parameters[i].ParameterType,
                out var paramInstance))
                throw new NotImplementedException();

            objects[i] = paramInstance;
        }

        singleton[typeof(TInstance)] = ctor.Invoke(objects)
            ?? throw new NullReferenceException(); ;
        return this;
    }

    public IoC RegisterSingleton<TInterface>(
        TInterface instance)
    {
        singleton[typeof(TInterface)] = instance 
            ?? throw new NullReferenceException(); ;
        return this;
    }

    private readonly Dictionary<Type, object> singleton = [];
}