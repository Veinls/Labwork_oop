using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderTrackin;

public interface IOrderObserver
{
    void Update(Order order);
}