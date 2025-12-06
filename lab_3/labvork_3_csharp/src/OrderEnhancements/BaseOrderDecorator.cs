using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public abstract class BaseOrderDecorator : IDecoratableOrder
{
    protected readonly IDecoratableOrder _decoratedOrder;
    
    protected BaseOrderDecorator(IDecoratableOrder decoratedOrder)
    {
        _decoratedOrder = decoratedOrder ?? throw new ArgumentNullException(nameof(decoratedOrder));
    }
    
    public virtual string Id => _decoratedOrder.Id;
    public virtual Customer Customer => _decoratedOrder.Customer;
    public virtual List<OrderItem> Items => _decoratedOrder.Items;
    public virtual decimal Total => _decoratedOrder.Total;
    public virtual string DeliveryType
    {
        get => _decoratedOrder.DeliveryType;
        set => _decoratedOrder.DeliveryType = value;
    }
    
    public virtual void DisplayEnhancedOrder()
    {
        _decoratedOrder.DisplayEnhancedOrder();
    }
    
    public virtual string GetOrderInfo()
    {
        return _decoratedOrder.GetOrderInfo();
    }
}