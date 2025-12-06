using labvork_3_csharp.Domain;

namespace labvork_3_csharp.Pricing;

public class StandardPricingStrategy : IPricingStrategy
{
    private const decimal TaxPercentage = 10.00m;
    public decimal CalculateTotal(Order order)
    {
        var tax = order.Total * (TaxPercentage / 100);
        var totalWithTax = order.Total + tax;
        
        Console.WriteLine($"Начислен налог {TaxPercentage}%: +{tax:C}");
        Console.WriteLine($"Сумма с налогом: {totalWithTax:C}");
        
        return totalWithTax;
    }
}