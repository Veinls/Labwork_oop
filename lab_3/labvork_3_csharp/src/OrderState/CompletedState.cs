using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderState;

public class CompletedState : IOrderState
{
    public void Process(Order order)
    {
        order.Status = OrderStatus.Completed;
        order.CompletedAt = DateTime.Now;
    }
    // Своя какая то логика...
}