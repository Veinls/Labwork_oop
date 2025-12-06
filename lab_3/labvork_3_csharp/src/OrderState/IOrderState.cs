using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderState;

public interface IOrderState
{
    void Process(Order order);
}