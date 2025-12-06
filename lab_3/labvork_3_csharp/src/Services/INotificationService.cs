using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Services;

public interface INotificationService
{
    void NotifyOrderCreated(Order order);
    void NotifyStatusChanged(Order order);
}