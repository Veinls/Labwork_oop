using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public interface IPricingStrategy
{
    decimal CalculateTotal(Order order);
}