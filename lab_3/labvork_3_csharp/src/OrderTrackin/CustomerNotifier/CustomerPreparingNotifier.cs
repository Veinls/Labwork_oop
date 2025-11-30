using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class CustomerPreparingNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order.Status == OrderStatus.Preparing)
        {
            Console.WriteLine($"Клиенту: Заказ {order.Id} принят в работу");
        }
    }
}