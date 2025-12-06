namespace labvork_3_csharp.Domain;

public class Customer
{
    public string Name { get; }
    public string Phone { get; }

    public Customer(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}