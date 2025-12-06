using labvork_3_csharp.Domain;
using labvork_3_csharp.OrderTrackin;

namespace labvork_3_csharp.OrderState;

public class OrderStateContext
{
    private IOrderState _currentState;
    private readonly Order _order;
    private readonly OrderObservable _observable;

    public OrderStateContext(Order order)
    {
        _order = order;
        _observable = new OrderObservable();
        
        _observable.Subscribe(new CustomerPreparingNotifier());
        _observable.Subscribe(new CustomerDeliveryNotifier());
        _observable.Subscribe(new CustomerCompletedNotifier());
        _observable.Subscribe(new KitchenPreparingNotifier());
        _observable.Subscribe(new KitchenCompletedNotifier());
        
        _currentState = new PreparingState();
        _currentState.Process(_order);
        _observable.Notify(_order);
    }

    public void SetState(IOrderState state)
    {
        _currentState = state;
        _currentState.Process(_order);
        _observable.Notify(_order);
    }

    public string GetCurrentStatus()
    {
        return _order.Status.ToString();
    }
}