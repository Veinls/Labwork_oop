using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Services;

public class NotificationService : INotificationService
{
    public void NotifyOrderCreated(Order order)
    {
        Console.WriteLine($"Новый заказ создан: {order.Id}");
        Console.WriteLine($"Клиент: {order.Customer.Name}");
        Console.WriteLine($"Сумма: {order.Total:C}");
    }

    public void NotifyStatusChanged(Order order)
    {
        Console.WriteLine($"Статус заказа {order.Id} изменен на: {order.Status}");
    }
}