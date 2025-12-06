using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public interface IDeliveryStrategy
{
    decimal CalculateDeliveryCost(Order order);
}