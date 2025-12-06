using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class OrderObservable
{
    private readonly List<IOrderObserver> _observers = new();

    public void Subscribe(IOrderObserver observer)
    {
        _observers.Add(observer);
    }
    public void Notify(Order order)
    { 
        _observers.ForEach(observer => observer.Update(order));
    } 
}