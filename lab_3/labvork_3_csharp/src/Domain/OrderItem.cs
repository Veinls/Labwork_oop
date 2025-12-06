namespace labvork_3_csharp.Domain;

public class OrderItem
{
    public MenuItem MenuItem { get; }
    public int Quantity { get; }
    public decimal Price => MenuItem.Price;

    public OrderItem(MenuItem item, int quantity)
    {
        MenuItem = item;
        Quantity = quantity;
    }
}