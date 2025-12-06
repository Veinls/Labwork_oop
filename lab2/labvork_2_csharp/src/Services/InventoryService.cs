using labvork_2_csharp.Core.Inventory.Interfaces;
using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Items.Concrete;
using labvork_2_csharp.Core.Enhancement.Strategies;

namespace labvork_2_csharp.Services
{
    public class InventoryService
    {
        private readonly IInventory _inventory;
        private readonly Dictionary<Type, IEnhancementStrategy> _enhancementStrategies;

        public InventoryService(IInventory inventory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            
            _enhancementStrategies = new Dictionary<Type, IEnhancementStrategy>
            {
                { typeof(Weapon), new WeaponEnhancementStrategy() },
                { typeof(Armor), new WeaponEnhancementStrategy() },
                { typeof(Potion), new PotionEnhancementStrategy() }, 
                { typeof(QuestItem), new PotionEnhancementStrategy() }
            };
        }

        public bool EnhanceItem(string itemId)
        {
            var item = _inventory.FindItem(itemId);
            if (item == null)
            {
                Console.WriteLine($"Предмет с ID {itemId} не найден");
                return false;
            }

            var itemType = item.GetType();
            if (_enhancementStrategies.ContainsKey(itemType))
            {
                var strategy = _enhancementStrategies[itemType];
                if (strategy.CanEnhance(item))
                {
                    try
                    {
                        strategy.Enhance(item);
                        Console.WriteLine($"{item.Name} успешно улучшен");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при улучшении {item.Name}: {ex.Message}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"{item.Name} нельзя улучшить дальше");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"{item.Name} не поддерживает улучшение");
                return false;
            }
        }

        public void DisplayEnhancementInfo(string itemId)
        {
            var item = _inventory.FindItem(itemId);
            if (item == null)
            {
                Console.WriteLine($"Предмет с ID {itemId} не найден");
                return;
            }

            var itemType = item.GetType();
            if (_enhancementStrategies.ContainsKey(itemType))
            {
                var strategy = _enhancementStrategies[itemType];
                Console.WriteLine($"{strategy.GetEnhancementInfo(item)}");
            }
            else
            {
                Console.WriteLine($"Этот предмет не поддерживает улучшение");
            }
        }

        public bool UseItem(string itemId)
        {
            var item = _inventory.FindItem(itemId);
            if (item == null)
            {
                Console.WriteLine($"Предмет с ID {itemId} не найден");
                return false;
            }

            try
            {
                item.Use();
                Console.WriteLine($"{item.Name} использован");

                if (item is Potion)
                {
                    _inventory.RemoveItem(itemId);
                    Console.WriteLine($"Расходный предмет {item.Name} удален после использования");
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при использовании {item.Name}: {ex.Message}");
                return false;
            }
        }

        public int EnhanceAllEligibleItems()
        {
            var enhancedCount = 0;
            Console.WriteLine($"Массовое улучшение предметов...");

            foreach (var item in _inventory.Items)
            {
                try
                {
                    if (EnhanceItem(item.Id))
                    {
                        enhancedCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при улучшении {item.Name}: {ex.Message}");
                }
            }

            Console.WriteLine($"Всего улучшено предметов: {enhancedCount}");
            return enhancedCount;
        }

        public void DisplayItemsByType<T>() where T : IItem
        {
            var itemsOfType = _inventory.Items.OfType<T>();
            var typeName = typeof(T).Name;
            
            Console.WriteLine($"ПРЕДМЕТЫ ТИПА {typeName.ToUpper()}");
            
            if (!itemsOfType.Any())
            {
                Console.WriteLine("Предметы не найдены");
                return;
            }

            foreach (var item in itemsOfType)
            {
                Console.WriteLine($"- {item.GetInfo()}");
            }
        }

        public int ClearItemsByName(string itemName)
        {
            var countBefore = _inventory.Count;
            var removedCount = _inventory.RemoveItems(itemName, int.MaxValue);
            
            if (removedCount > 0)
            {
                Console.WriteLine($"Удалено {removedCount} предметов с именем '{itemName}'");
            }
            else
            {
                Console.WriteLine($"Предметы с именем '{itemName}' не найдены");
            }
            
            return removedCount;
        }

        public void DisplayInventoryStatus()
        {
            _inventory.DisplayInventory();
            Console.WriteLine($"Можно добавлять: {_inventory.CanAddItems}");
            Console.WriteLine($"Можно удалять: {_inventory.CanRemoveItems}");
        }

        public void DisplayAllEnhanceableItems()
        {
            Console.WriteLine("ПРЕДМЕТЫ ДЛЯ УЛУЧШЕНИЯ:");
            var enhanceableItems = _inventory.Items.Where(item => 
            {
                var itemType = item.GetType();
                return _enhancementStrategies.ContainsKey(itemType) && 
                       _enhancementStrategies[itemType].CanEnhance(item);
            });

            if (!enhanceableItems.Any())
            {
                Console.WriteLine("Нет предметов для улучшения");
                return;
            }

            foreach (var item in enhanceableItems)
            {
                DisplayEnhancementInfo(item.Id);
            }
        }
    }
}