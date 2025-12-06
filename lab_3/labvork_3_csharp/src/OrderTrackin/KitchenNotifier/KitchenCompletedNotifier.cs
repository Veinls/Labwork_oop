using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class KitchenCompletedNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order.Status == OrderStatus.Completed)
        {
            Console.WriteLine($"Кухня: Заказ {order.Id} выполнен");
            if (order.CompletedAt.HasValue)
            {
                var totalTime = order.CompletedAt.Value - order.CreatedAt;
                Console.WriteLine($"Общее время выполнения заказа: {totalTime:mm\\:ss} минут");
            }
        }
    }
}