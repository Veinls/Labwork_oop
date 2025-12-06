using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public class KitchenPreparingNotifier : IOrderObserver
{
    public void Update(Order order)
    {
        if (order.Status == OrderStatus.Preparing)
        {
            Console.WriteLine($"Кухня: Начать приготовление заказа {order.Id}");
            Console.WriteLine($"Состав: {string.Join(", ", order.Items.Select(i => i.MenuItem.Name))}");
        }
    }
}