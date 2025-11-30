using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderEnhancements;

public interface IOrderDecorator
{
    void DisplayEnhancedOrder();
    string GetOrderInfo();
}