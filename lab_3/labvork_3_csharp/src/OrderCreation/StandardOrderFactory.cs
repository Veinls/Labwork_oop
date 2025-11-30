using labvork_3_csharp.Domain;
using labvork_3_csharp.Pricing;

namespace labvork_3_csharp.OrderCreation;

public class StandardOrderFactory : OrderFactory
{
    public StandardOrderFactory(IDeliveryStrategy deliveryStrategy) 
        : base(deliveryStrategy){}

    protected override string GetOrderType() => "Standard";
}