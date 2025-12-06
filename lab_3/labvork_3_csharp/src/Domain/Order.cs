using System.ComponentModel;

namespace labvork_3_csharp.Domain;

public class Order
{
    public string Id { get;}
    public Customer Customer { get; }
    public List<OrderItem> Items { get; } = new();

    public decimal Total
    {
        get { return Items.Sum(x => x.Price * x.Quantity); }
    }

    public OrderStatus Status { get; set; } = OrderStatus.Preparing;
    public string DeliveryType { get; set; } = "Standard";
    
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; set; }
    public Order(Customer customer)
    {
        Customer = customer;
        Id = GenerateId();
        CreatedAt = DateTime.Now;
    }
    
    public void AddItem(MenuItem item, int quantity)
    {
        Items.Add(new OrderItem(item, quantity));
    }
    
    private string GenerateId()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
    }
    
    public void DisplayEnhancedOrder()
    {
        Console.WriteLine($"Заказ {Id}");
        Console.WriteLine($"Клиент: {Customer.Name} ({Customer.Phone})");
        Console.WriteLine($"Тип доставки: {DeliveryType}");
        Console.WriteLine($"Статус: {Status}");
        Console.WriteLine("Состав заказа:");
        foreach (var item in Items)
        {
            var priceInfo = item.Price == 0 ? "БЕСПЛАТНО" : $"{item.Price:C}";
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {priceInfo}");
        }
        Console.WriteLine($"Итого: {Total:C}");
        Console.WriteLine($"Создан: {CreatedAt:HH:mm:ss}");
    }

    public string GetOrderInfo()
    {
        return $"Заказ {Id} | Клиент: {Customer.Name} | Сумма: {Total:C} | Тип: {DeliveryType}";
    }
}
    