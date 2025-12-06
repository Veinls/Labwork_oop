using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class CustomerCompletedNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order.Status == OrderStatus.Completed)
        {
            Console.WriteLine($"Клиенту: Заказ {order.Id} доставлен. Спасибо!");
        }
    }
}