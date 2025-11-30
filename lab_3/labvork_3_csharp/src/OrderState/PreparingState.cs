using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderState;

public class PreparingState : IOrderState
{
    public void Process(Order order)
    {
        order.Status = OrderStatus.Preparing;
    }
    /* Тут может быть своя бизнес-логика:
     - проверить, все ли товары доступны,
     - зарезирмировать товар,
     - уведомления,
     и тд. */
}