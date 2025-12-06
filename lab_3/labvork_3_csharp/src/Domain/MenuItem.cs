namespace labvork_3_csharp.Domain;

public class MenuItem
{
    public string Id { get;}
    public string Name {get;}
    public decimal Price {get;}

    public MenuItem(string id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
        
    }
    
}