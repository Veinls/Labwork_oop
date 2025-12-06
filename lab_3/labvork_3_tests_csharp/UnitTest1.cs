using labvork_3_csharp.Domain;
using labvork_3_csharp.OrderCreation;
using labvork_3_csharp.Pricing;
using labvork_3_csharp.Repositories;
using labvork_3_csharp.OrderState;
using labvork_3_csharp.OrderEnhancements;

namespace labvork_3_tests_csharp;

public class UnitTest1
{
    [Fact]
    public void CreateExpressOrder_WithValidItems_ShouldAddExpressDelivery()
    {
        var deliveryStrategy = new ExpressDeliveryStrategy();
        var factory = new ExpressOrderFactory(deliveryStrategy);
        var customer = new Customer("Test", "+79990000000");
        var items = new List<MenuItem>
        {
            new MenuItem("2", "Суши", 25.00m),
            new MenuItem("5", "Кофе", 4.50m)
        };

        var order = factory.CreateOrder(customer, items);

        Assert.NotNull(order);
        Assert.Equal("Express", order.DeliveryType);
        Assert.Contains(order.Items, item => item.MenuItem.Name.Contains("доставка"));
        Assert.Equal(29.50m, order.Items.Where(i => !i.MenuItem.Name.Contains("доставка")).Sum(i => i.Price * i.Quantity));
    }

    [Fact]
    public void ExpressDeliveryStrategy_WithSmallOrder_ShouldThrowException()
    {
        var strategy = new ExpressDeliveryStrategy();
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("5", "Кофе", 4.50m), 1);

        var exception = Assert.Throws<InvalidOperationException>(() => strategy.CalculateDeliveryCost(order));
        Assert.Contains("не подходит", exception.Message);
    }

    [Fact]
    public void ExpressDeliveryStrategy_WithLargeOrder_ShouldBeFree()
    {
        var strategy = new ExpressDeliveryStrategy();
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Товар", 100m), 1);

        var deliveryCost = strategy.CalculateDeliveryCost(order);

        Assert.Equal(0m, deliveryCost);
    }

    [Fact]
    public void StandardPricingStrategy_ShouldCalculateTax()
    {
        var strategy = new StandardPricingStrategy();
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 100m), 1);

        var total = strategy.CalculateTotal(order);

        Assert.Equal(110m, total);
    }

    [Fact]
    public void DiscountPricingStrategy_WithEligibleOrder_ShouldApplyDiscount()
    {
        var strategy = new DiscountPricingStrategy(10, 50);
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 60m), 1);

        var total = strategy.CalculateTotal(order);

        Assert.Equal(54m, total);
    }

    [Fact]
    public void OrderState_ShouldChangeStatusCorrectly()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        var stateContext = new OrderStateContext(order);

        Assert.Equal("Preparing", stateContext.GetCurrentStatus());

        stateContext.SetState(new DeliveryState());
        Assert.Equal("InDelivery", stateContext.GetCurrentStatus());

        stateContext.SetState(new CompletedState());
        Assert.Equal("Completed", stateContext.GetCurrentStatus());
    }

    [Fact]
    public void SpecialRequestDecorator_ShouldCalculateAdditionalCost()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 12.99m), 1);

        var adapter = new OrderAdapter(order);
        var decorator = new SpecialRequestDecorator(adapter, "дополнительный соус");

        Assert.Equal(14.49m, decorator.Total); 
        Assert.Equal("Special", decorator.DeliveryType);
    }

    [Fact]
    public void SpecialRequestDecorator_ShouldNotModifyOriginalOrder()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 12.99m), 1);
        
        var originalTotal = order.Total;
        var adapter = new OrderAdapter(order);
        var decorator = new SpecialRequestDecorator(adapter, "дополнительный соус");

        Assert.Equal(originalTotal, order.Total);
        Assert.Equal("Standard", order.DeliveryType);
        
        Assert.Equal(originalTotal + 1.50m, decorator.Total);
    }

    [Fact]
    public void ExpressDeliveryDecorator_WithValidOrder_ShouldCalculateDeliveryCost()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 25.00m), 1);

        var adapter = new OrderAdapter(order);
        var decorator = new ExpressDeliveryDecorator(adapter);

        Assert.True(decorator.Total > 25.00m);
        Assert.Equal("Express", decorator.DeliveryType);
    }

    [Fact]
    public void ExpressDeliveryDecorator_WithSmallOrder_ShouldNotBeAvailable()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("5", "Кофе", 4.50m), 1);

        var adapter = new OrderAdapter(order);
        var decorator = new ExpressDeliveryDecorator(adapter);

        Assert.Equal("Standard", decorator.DeliveryType);
        Assert.Equal(4.50m, decorator.Total);
    }

    [Fact]
    public void DecoratorChain_ShouldWorkCorrectly()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 25.00m), 1);

        var adapter = new OrderAdapter(order);
        
        IDecoratableOrder decorated = adapter;
        decorated = new SpecialRequestDecorator(decorated, "дополнительный соус");
        decorated = new ExpressDeliveryDecorator(decorated);

        Assert.True(decorated.Total > 25.00m + 1.50m); 
        Assert.Equal("Express", decorated.DeliveryType);
    }

    [Fact]
    public void MenuRepository_GetById_ShouldReturnCorrectItem()
    {
        var repo = new MenuRepository();

        var item = repo.GetById("1");

        Assert.NotNull(item);
        Assert.Equal("Пицца", item.Name);
        Assert.Equal(12.99m, item.Price);
    }

    [Fact]
    public void OrderRepository_SaveAndGet_ShouldWorkCorrectly()
    {
        var repo = new OrderRepository();
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        order.AddItem(new MenuItem("1", "Пицца", 12.99m), 1);

        repo.Save(order);
        var retrieved = repo.GetById(order.Id);

        Assert.NotNull(retrieved);
        Assert.Equal(order.Id, retrieved.Id);
        Assert.Equal(order.Total, retrieved.Total);
    }

    [Fact]
    public void OrderRepository_GetByCustomer_ShouldReturnCustomerOrders()
    {
        var repo = new OrderRepository();
        var customer = new Customer("Test", "+79991112233");
        var order1 = new Order(customer);
        var order2 = new Order(customer);

        repo.Save(order1);
        repo.Save(order2);

        var customerOrders = repo.GetByCustomer("+79991112233");

        Assert.Equal(2, customerOrders.Count);
        Assert.Contains(customerOrders, o => o.Id == order1.Id);
        Assert.Contains(customerOrders, o => o.Id == order2.Id);
    }

    [Fact]
    public void Order_AddItem_ShouldCalculateTotalCorrectly()
    {
        var customer = new Customer("Test", "+79990000000");
        var order = new Order(customer);
        
        order.AddItem(new MenuItem("1", "Пицца", 10m), 2);
        order.AddItem(new MenuItem("2", "Кофе", 5m), 1);

        Assert.Equal(25m, order.Total);
        Assert.Equal(2, order.Items.Count);
    }
}