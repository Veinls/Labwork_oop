using labvork_3_csharp.Domain;
using labvork_3_csharp.OrderCreation;

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
                var preparationTime = order.CompletedAt.Value - order.CreatedAt;
                Console.WriteLine($"Время приготовления: {preparationTime:mm\\:ss} минут");
            }
        }
    }
}