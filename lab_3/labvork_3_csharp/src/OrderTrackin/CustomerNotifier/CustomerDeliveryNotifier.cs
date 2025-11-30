using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class CustomerDeliveryNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order.Status == OrderStatus.InDelivery)
        {
            Console.WriteLine($"Клиенту: Курьер забрал заказ {order.Id}");
        }
    }
}