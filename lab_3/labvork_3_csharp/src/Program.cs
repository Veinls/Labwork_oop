using labvork_3_csharp.Domain;
using labvork_3_csharp.OrderCreation;
using labvork_3_csharp.Pricing;
using labvork_3_csharp.OrderState;
using labvork_3_csharp.OrderEnhancements;
using labvork_3_csharp.Repositories;
using labvork_3_csharp.Services;

namespace labvork_3_csharp;

class Program
{
    static void Main()
    {
        Console.WriteLine("СИСТЕМА ДОСТАВКИ ЕДЫ");
        Console.WriteLine("====================");

        var menuRepo = new MenuRepository();
        var orderRepo = new OrderRepository();
        var notificationService = new NotificationService();

        DemonstrateOrderCreation(menuRepo, orderRepo, notificationService);
        DemonstrateOrderProcessing(menuRepo, orderRepo);
        DemonstrateSpecialOrders(menuRepo, orderRepo);
        DemonstratePricingStrategies(menuRepo, orderRepo);
        DemonstrateDecorators(menuRepo, orderRepo);
        DemonstrateCustomerOrders(orderRepo);
        
        Console.WriteLine("РАБОТА СИСТЕМЫ ЗАВЕРШЕНА");
        Console.WriteLine("========================");
    }

    static void DemonstrateOrderCreation(IMenuRepository menuRepo, IOrderRepository orderRepo, INotificationService notificationService)
    {
        Console.WriteLine("1. СОЗДАНИЕ ЗАКАЗОВ");
        Console.WriteLine("-------------------");

        var standardDelivery = new StandardDeliveryStrategy();
        var expressDelivery = new ExpressDeliveryStrategy();

        var customer1 = new Customer("Анна", "+79991234567");
        var customer2 = new Customer("Иван", "+79997654321");

        var pizza = GetMenuItemSafe(menuRepo, "1", "Пицца");
        var sushi = GetMenuItemSafe(menuRepo, "2", "Суши");
        var coffee = GetMenuItemSafe(menuRepo, "5", "Кофе");

        if (pizza == null || coffee == null || sushi == null)
        {
            Console.WriteLine("Ошибка: не удалось найти необходимые товары в меню");
            return;
        }

        var items1 = new List<MenuItem> { pizza, coffee };
        var items2 = new List<MenuItem> { sushi, sushi, coffee }; 

        Console.WriteLine("ЗАКАЗ 1: Стандартная доставка");
        var standardFactory = new StandardOrderFactory(standardDelivery);
        var standardOrder = standardFactory.CreateOrder(customer1, items1);
        notificationService.NotifyOrderCreated(standardOrder);
        orderRepo.Save(standardOrder);
        Console.WriteLine($"Итоговая стоимость: {standardOrder.Total:C}");

        Console.WriteLine("ЗАКАЗ 2: Экспресс доставка");
        var expressFactory = new ExpressOrderFactory(expressDelivery);
        var expressOrder = expressFactory.CreateOrder(customer2, items2);
        notificationService.NotifyOrderCreated(expressOrder);
        orderRepo.Save(expressOrder);
        Console.WriteLine($"Итоговая стоимость: {expressOrder.Total:C}");
        Console.WriteLine();
    }

    static void DemonstrateOrderProcessing(IMenuRepository menuRepo, IOrderRepository orderRepo)
    {
        Console.WriteLine("2. ОБРАБОТКА ЗАКАЗОВ С НАБЛЮДАТЕЛЯМИ");
        Console.WriteLine("-----------------------------------");

        var customer = new Customer("Петр", "+79998887766");
        
        var burger = GetMenuItemSafe(menuRepo, "3", "Бургер");
        var salad = GetMenuItemSafe(menuRepo, "4", "Салат");

        if (burger == null || salad == null)
        {
            Console.WriteLine("Ошибка: не удалось найти необходимые товары в меню");
            return;
        }

        var items = new List<MenuItem> { burger, salad };

        var order = new Order(customer);
        items.ForEach(item => order.AddItem(item, 1)); 
        orderRepo.Save(order);

        Console.WriteLine($"Заказ создан: {order.Id}");
        Console.WriteLine($"Сумма: {order.Total:C}");
        Console.WriteLine();

        var stateContext = new OrderStateContext(order);
        
        Console.WriteLine("Заказ принят, начинаем приготовление...");
        Thread.Sleep(2000); 

        Console.WriteLine("Приготовление завершено, передаем курьеру...");
        stateContext.SetState(new DeliveryState()); 
        Thread.Sleep(1500); 

        Console.WriteLine("Курьер доставил заказ...");
        stateContext.SetState(new CompletedState()); 

        Console.WriteLine($"Финальный статус: {stateContext.GetCurrentStatus()}");
        Console.WriteLine();
    }

    static void DemonstrateSpecialOrders(IMenuRepository menuRepo, IOrderRepository orderRepo)
    {
        Console.WriteLine("3. ОСОБЫЕ ЗАКАЗЫ И ДОСТАВКА");
        Console.WriteLine("----------------------------");

        var customer = new Customer("Мария", "+79995554433");
        
        var pizza = GetMenuItemSafe(menuRepo, "1", "Пицца");
        var sushi = GetMenuItemSafe(menuRepo, "2", "Суши");
        var coffee = GetMenuItemSafe(menuRepo, "5", "Кофе");

        if (pizza == null || sushi == null || coffee == null)
        {
            Console.WriteLine("Ошибка: не удалось найти необходимые товары в меню");
            return;
        }

        var items = new List<MenuItem> { pizza, sushi, coffee };

        var order = new Order(customer);
        items.ForEach(item => order.AddItem(item, 1));
        orderRepo.Save(order);

        var specialOrder = new SpecialRequestDecorator(order, "Без лука, добавить соус");
        specialOrder.DisplayEnhancedOrder();

        var expressStrategy = new ExpressDeliveryStrategy();
        try
        {
            var deliveryCost = expressStrategy.CalculateDeliveryCost(order);
            Console.WriteLine($"Стоимость экспресс-доставки: {deliveryCost:C}");
            Console.WriteLine("Заказ подходит для экспресс-доставки");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Экспресс-доставка недоступна: {ex.Message}");
        }
        Console.WriteLine();
    }

        static void DemonstratePricingStrategies(IMenuRepository menuRepo, IOrderRepository orderRepo)
    {
        Console.WriteLine("4. СТРАТЕГИИ ЦЕНООБРАЗОВАНИЯ");
        Console.WriteLine("----------------------------");

        var customer = new Customer("Дмитрий", "+79994445566");
        
        var pizza = GetMenuItemSafe(menuRepo, "1", "Пицца");
        var sushi = GetMenuItemSafe(menuRepo, "2", "Суши");

        if (pizza == null || sushi == null)
        {
            Console.WriteLine("Ошибка: не удалось найти необходимые товары в меню");
            return;
        }

        var items = new List<MenuItem> { pizza, sushi };

        Console.WriteLine("Стандартная стратегия (с налогом):");
        
        var orderWithTax = new Order(customer);
        items.ForEach(item => orderWithTax.AddItem(item, 1));
        
        var standardPricing = new StandardPricingStrategy();
        var totalWithTax = standardPricing.CalculateTotal(orderWithTax);
        
        orderWithTax.AddItem(new MenuItem("tax", "Налог 10%", totalWithTax - orderWithTax.Total), 1);
        
        orderRepo.Save(orderWithTax);
        Console.WriteLine($"Создан заказ: {orderWithTax.Id}");
        Console.WriteLine($"Итоговая сумма с налогом: {orderWithTax.Total:C}");

        Console.WriteLine("\nСтратегия со скидкой (15%):");
        
        var orderWithDiscount = new Order(customer);
        items.ForEach(item => orderWithDiscount.AddItem(item, 1));
        
        var discountPricing = new DiscountPricingStrategy(15, 30);
        var totalWithDiscount = discountPricing.CalculateTotal(orderWithDiscount);
        
        orderWithDiscount.AddItem(new MenuItem("discount", "Скидка 15%", -(orderWithDiscount.Total - totalWithDiscount)), 1);
        
        orderRepo.Save(orderWithDiscount);
        Console.WriteLine($"Создан заказ: {orderWithDiscount.Id}");
        Console.WriteLine($"Итоговая сумма со скидкой: {orderWithDiscount.Total:C}");

        var difference = orderWithTax.Total - orderWithDiscount.Total;
        Console.WriteLine($"\nЭкономия со скидкой: {difference:C}");
        Console.WriteLine();
    }

    
    static void DemonstrateDecorators(IMenuRepository menuRepo, IOrderRepository orderRepo)
    {
        Console.WriteLine("5. ДЕКОРАТОРЫ");
        Console.WriteLine("--------------");

        var customer = new Customer("Алексей", "+79991112233");
        var pizza = GetMenuItemSafe(menuRepo, "1", "Пицца");
        var coffee = GetMenuItemSafe(menuRepo, "5", "Кофе");

        if (pizza == null || coffee == null)
        {
            Console.WriteLine("Ошибка: не удалось найти необходимые товары в меню");
            return;
        }

        var items = new List<MenuItem> { pizza, coffee };

        Console.WriteLine("1. Базовый заказ (без декораторов):");
        var baseOrder = new Order(customer);
        items.ForEach(item => baseOrder.AddItem(item, 1));
        DisplayOrderDetailed(baseOrder);
        orderRepo.Save(baseOrder);
        Console.WriteLine();

        Console.WriteLine("2. Заказ с особыми пожеланиями:");
        var specialOrder = new Order(customer);
        items.ForEach(item => specialOrder.AddItem(item, 1));
        var specialDecorator = new SpecialRequestDecorator(specialOrder, "дополнительный соус и премиум упаковка");
        specialDecorator.DisplayEnhancedOrder();
        orderRepo.Save(specialOrder);
        Console.WriteLine();

        Console.WriteLine("3. Заказ с экспресс доставкой:");
        var expressOrder = new Order(customer);
        items.ForEach(item => expressOrder.AddItem(item, 1));
        var expressDecorator = new ExpressDeliveryDecorator(expressOrder);
        expressDecorator.DisplayEnhancedOrder(); 
        orderRepo.Save(expressOrder);
        Console.WriteLine();
        
        Console.WriteLine("4. Маленький заказ с экспресс-доставкой:");
        var smallOrder = new Order(new Customer("Тест", "+79990000000"));
        var coffeeOnly = GetMenuItemSafe(menuRepo, "5", "Кофе");
        if (coffeeOnly != null)
        {
            smallOrder.AddItem(coffeeOnly, 1);
            var smallExpressDecorator = new ExpressDeliveryDecorator(smallOrder);
            smallExpressDecorator.DisplayEnhancedOrder();
            orderRepo.Save(smallOrder);
        }
        
        Console.WriteLine();
        
        Console.WriteLine("ИТОГОВОЕ СРАВНЕНИЕ СТОИМОСТЕЙ:");
        Console.WriteLine($"• Базовый заказ: {baseOrder.Total:C}");
        Console.WriteLine($"• С особыми пожеланиями: {specialOrder.Total:C}");
        Console.WriteLine($"• С экспресс-доставкой: {expressOrder.Total:C}");
        
        if (smallOrder.Items.Any())
        {
            Console.WriteLine($"• Маленький заказ: {smallOrder.Total:C}");
        }
    }

    static void DisplayOrderDetailed(Order order)
    {
        Console.WriteLine($"Заказ {order.Id} | Тип: {order.DeliveryType}");
        Console.WriteLine("Состав заказа:");
        foreach (var item in order.Items)
        {
            var priceInfo = item.Price == 0 ? "БЕСПЛАТНО" : $"{item.Price:C}";
            Console.WriteLine($"  {item.MenuItem.Name} x{item.Quantity} - {priceInfo}");
        }
        Console.WriteLine($"Итого: {order.Total:C}");
    }

    
    static void DemonstrateCustomerOrders(IOrderRepository orderRepo)
    {
        Console.WriteLine("6. ПОИСК ЗАКАЗОВ КЛИЕНТА");
        Console.WriteLine("------------------------");
    
        var customerPhone = "+79991234567";
        var customerOrders = orderRepo.GetByCustomer(customerPhone);
    
        if (customerOrders.Any())
        {
            Console.WriteLine($"Найдено заказов для клиента {customerPhone}: {customerOrders.Count}");
            foreach (var order in customerOrders)
            {
                Console.WriteLine($"  Заказ {order.Id} | {order.CreatedAt:HH:mm} | {order.Total:C} | {order.Status}");
            }
        }
        else
        {
            Console.WriteLine($"Заказы для клиента {customerPhone} не найдены");
        }
        Console.WriteLine();
    }
    private static MenuItem? GetMenuItemSafe(IMenuRepository menuRepo, string id, string itemName)
    {
        var item = menuRepo.GetById(id);
        if (item == null)
        {
            Console.WriteLine($"Товар '{itemName}' (ID: {id}) не найден в меню");
        }
        return item;
    }
}