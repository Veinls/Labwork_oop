using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Inventory
{
    internal class InventoryOperations
    {
        private readonly List<IItem> _items;

        internal InventoryOperations(List<IItem> items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        internal IItem? FindById(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) return null;
            return _items.Find(item => item.Id == itemId);
        }

        private IItem? FindByIndex(int index)
        {
            return index >= 0 && index < _items.Count ? _items[index] : null;
        }

        internal bool RemoveById(string itemId)
        {
            var itemToRemove = FindById(itemId);
            if (itemToRemove == null) return false;
            
            _items.Remove(itemToRemove);
            Console.WriteLine($"Удален предмет по ID: {itemToRemove.Name}");
            return true;
        }

        internal bool RemoveByItem(IItem item)
        {
            if (item == null) 
                throw new ArgumentNullException(nameof(item));
            
            if (_items.Remove(item))
            {
                Console.WriteLine($"Удален предмет по объекту: {item.Name}");
                return true;
            }
            return false;
        }

        internal int RemoveByName(string itemName, int count = 1)
        {
            if (string.IsNullOrEmpty(itemName) || count <= 0) 
                return 0;

            var removedCount = 0;
            for (int i = _items.Count - 1; i >= 0 && removedCount < count; i--)
            {
                if (_items[i].Name == itemName)
                {
                    var removedItem = _items[i];
                    _items.RemoveAt(i);
                    Console.WriteLine($"Удален предмет по имени: {removedItem.Name}");
                    removedCount++;
                }
            }
            
            return removedCount;
        }

        internal bool RemoveByIndex(int index)
        {
            var item = FindByIndex(index);
            if (item == null) return false;
    
            _items.RemoveAt(index);
            Console.WriteLine($"Удален предмет по индексу {index}: {item.Name}");
            return true;
        }
        
    }
}