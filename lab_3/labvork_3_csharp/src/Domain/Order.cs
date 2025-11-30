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
}
    