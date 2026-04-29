using Casino.Domain;

namespace Casino;

public abstract class ReactiveBase
{
    protected ReactiveBase() =>
        observers.Add(this);

    ~ReactiveBase() =>
        observers.Remove(this);

    public void SendEvent(DomainEvent domainEvent)
    {
        events.Add(domainEvent);
        foreach (var observer in observers)
            observer.ApplyEvent(domainEvent);
    }

    public virtual void ApplyEvent(DomainEvent domainEvent) { }

    private static readonly List<ReactiveBase> observers = [];
    private static readonly List<DomainEvent> events = [];
}
