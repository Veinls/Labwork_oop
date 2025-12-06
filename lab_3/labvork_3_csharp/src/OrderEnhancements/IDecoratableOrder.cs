using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public interface IDecoratableOrder
{
    string Id { get; }
    Customer Customer { get; }
    List<OrderItem> Items { get; }
    decimal Total { get; }
    string DeliveryType { get; set; }
    void DisplayEnhancedOrder();
    string GetOrderInfo();
}