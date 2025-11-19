using labvork_2_csharp.Core.Inventory.Concrete;
using labvork_2_csharp.Core.Items.Builders;
using labvork_2_csharp.Core.Items;
using labvork_2_csharp.Services;
using labvork_2_csharp.Core.Items.Concrete;

namespace labvork_2_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("СИСТЕМА ИНВЕНТАРЯ");
            Console.WriteLine("==========================");

            var inventory = new GameInventory(8);
            var inventoryService = new InventoryService(inventory);
            
            Console.WriteLine("\nСОЗДАНИЕ ПРЕДМЕТОВ");
            Console.WriteLine("------------------");

            var steelSword = ItemBuilderFactory.CreateItem("Стальной меч")
                .WithId("меч_стальной")
                .AsSword(25)
                .Build();

            var battleAxe = ItemBuilderFactory.CreateItem("Боевой топор")
                .WithId("топор_боевой") 
                .AsAxe(30)
                .Build();

            var healthPotion = ItemBuilderFactory.CreateItem("Зелье здоровья")
                .WithId("зелье_здоровья")
                .AsHealthPotion(50)
                .Build();

            var manaPotion = ItemBuilderFactory.CreateItem("Зелье маны")
                .WithId("зелье_маны")
                .AsManaPotion(40)
                .Build();

            var leatherArmor = new Armor("доспех_кожаный", "Кожаный доспех", 15, ArmorType.Leather);
            
            Console.WriteLine("\nДОБАВЛЕНИЕ ПРЕДМЕТОВ В ИНВЕНТАРЬ");
            Console.WriteLine("--------------------------------");
            
            inventory.AddItem(leatherArmor);
            inventory.AddItem(steelSword);
            inventory.AddItem(battleAxe);
            inventory.AddItem(healthPotion);
            inventory.AddItem(manaPotion);

            Console.WriteLine("\nСОЗДАНИЕ УНИКАЛЬНЫХ ПРЕДМЕТОВ");
            Console.WriteLine("------------------------------");

            var legendarySword = ItemBuilderFactory.CreateWeapon()
                .SetId("меч_легендарный")
                .SetName("Легендарный меч драконоборца")
                .SetDamage(45)
                .SetType(WeaponType.Sword)
                .SetDescription("Мощное оружие, способное поразить даже древнего дракона") 
                .Build();

            var epicPotion = ItemBuilderFactory.CreatePotion()
                .SetId("эликсир_бессмертия")
                .SetName("Эликсир бессмертия")
                .SetHealing(100)
                .SetType(PotionType.Health)
                .SetStackAmount(1)
                .SetDescription("Редкое зелье, восстанавливающее все здоровье") 
                .Build();

            inventory.AddItem(legendarySword);
            inventory.AddItem(epicPotion);

            Console.WriteLine("\nТЕКУЩИЙ ИНВЕНТАРЬ");
            Console.WriteLine("-----------------");
            inventory.DisplayInventory();

            Console.WriteLine("\nУДАЛЕНИЕ ПРЕДМЕТОВ");
            Console.WriteLine("------------------");
            
            inventory.RemoveItem("меч_стальной");
            
            inventory.RemoveItems("Зелье здоровья", 1);

            Console.WriteLine("\nИНВЕНТАРЬ ПОСЛЕ УДАЛЕНИЯ");
            Console.WriteLine("-----------------------");
            inventory.DisplayInventory();

            Console.WriteLine("\nУЛУЧШЕНИЕ ПРЕДМЕТОВ");
            Console.WriteLine("-------------------");

            Console.WriteLine("Улучшение оружия:");
            inventoryService.EnhanceItem("топор_боевой");
            inventoryService.EnhanceItem("топор_боевой");

            Console.WriteLine("Улучшение брони:");
            inventoryService.EnhanceItem("доспех_кожаный");
            
            Console.WriteLine("\nИНФОРМАЦИЯ ОБ УЛУЧШЕНИЯХ");
            Console.WriteLine("-----------------------");
            inventoryService.DisplayEnhancementInfo("топор_боевой");
            inventoryService.DisplayEnhancementInfo("доспех_кожаный");

            Console.WriteLine("\nПОИСК И ИСПОЛЬЗОВАНИЕ ПРЕДМЕТОВ");
            Console.WriteLine("--------------------------------");
            
            Console.WriteLine("Поиск предметов по имени 'Зелье маны':");
            var foundItems = inventory.FindItemsByName("Зелье маны");
            foreach (var item in foundItems)
            {
                Console.WriteLine($"Найден: {item.GetInfo()}");
            }

            Console.WriteLine("Использование предметов:");
            inventoryService.UseItem("топор_боевой");
            inventoryService.UseItem("зелье_маны");

            Console.WriteLine("\nПЕРЕГРУЗКА ИНВЕНТАРЯ");
            Console.WriteLine("--------------------");
            
            for (int i = 1; i <= 4; i++)
            {
                var potion = ItemBuilderFactory.CreateItem($"Зелье здоровья")
                    .WithId($"зелье_здоровья_{i}")
                    .AsHealthPotion(20).Build();
                inventory.AddItem(potion);
            }

            Console.WriteLine("\nОСВОБОЖДЕНИЕ МЕСТА");
            Console.WriteLine("------------------");
            
            inventory.RemoveItems("Зелье здоровья");
            
            var newSword = ItemBuilderFactory.CreateItem("Новый меч")
                .WithId("меч_новый")
                .AsSword(20).Build();
            inventory.AddItem(newSword);

            Console.WriteLine("\nФИНАЛЬНЫЙ ИНВЕНТАРЬ");
            Console.WriteLine("-------------------");
            inventory.DisplayInventory();

            Console.WriteLine("\nДОПОЛНИТЕЛЬНЫЕ ВОЗМОЖНОСТИ");
            Console.WriteLine("---------------------------");
            
            Console.WriteLine("Массовое улучшение предметов:");
            inventoryService.EnhanceAllEligibleItems();

            Console.WriteLine("Очистка зелий:");
            inventoryService.ClearItemsByName("Зелье здоровья");

            Console.WriteLine("\nФИНАЛЬНЫЙ СТАТУС");
            Console.WriteLine("-----------------");
            inventoryService.DisplayInventoryStatus();

            Console.WriteLine("\nУСЕ");
            Console.WriteLine("===========================");
        }
    }
}