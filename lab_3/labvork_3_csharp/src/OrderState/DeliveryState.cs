using labvork_3_csharp.Domain;

namespace labvork_3_csharp.OrderState;

public class DeliveryState : IOrderState
{
    public void Process(Order order)
    {
        order.Status = OrderStatus.InDelivery;
    }
    
    /* Тут может быть своя бизнес логика
     - назначить курьера,
     - уведомленя,
     - отслеживание геолокации,
     ну и тд. */
}