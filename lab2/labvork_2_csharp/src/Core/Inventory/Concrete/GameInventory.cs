using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Inventory.Interfaces;
using labvork_2_csharp.Core.Inventory.States;

namespace labvork_2_csharp.Core.Inventory.Concrete
{
    public class GameInventory : IInventory
    {
        private readonly List<IItem> _items;
        private readonly InventoryOperations _operations;
        private IInventoryState _state;
        private readonly int _maxCapacity;

        public int Count => _items.Count;
        public bool IsFull => Count >= _maxCapacity;
        public int MaxCapacity => _maxCapacity;
        public IReadOnlyList<IItem> Items => _items.AsReadOnly();
        public string CurrentStateInfo => _state.StateInfo;
        public bool CanAddItems => _state.CanAddItem;
        public bool CanRemoveItems => _state.CanRemoveItem;

        public GameInventory(int maxCapacity = 20)
        {
            if (maxCapacity <= 0)
                throw new ArgumentException("Максимальная емкость должна быть положительной", nameof(maxCapacity));
            
            _items = new List<IItem>();
            _operations = new InventoryOperations(_items);
            _maxCapacity = maxCapacity;
            _state = new NormalState();
        }

        public void AddItem(IItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_state.CanAdd(item, _items.Count, _maxCapacity))
            {
                _items.Add(item);
                Console.WriteLine($"{item.Name} добавлен в инвентарь");
                UpdateState();
            }
            else
            {
                Console.WriteLine($"Невозможно добавить {item.Name} в текущем состоянии: {_state.StateInfo}");
            }
        }

        public bool RemoveItem(string itemId)
        {
            if (!_state.CanRemove()) 
            {
                Console.WriteLine("Невозможно удалить предмет в текущем состоянии");
                return false;
            }
            
            var result = _operations.RemoveById(itemId);
            if (result) UpdateState();
            return result;
        }

        public bool RemoveItem(IItem item)
        {
            if (!_state.CanRemove()) 
            {
                Console.WriteLine("Невозможно удалить предмет в текущем состоянии");
                return false;
            }
            
            var result = _operations.RemoveByItem(item);
            if (result) UpdateState();
            return result;
        }

        public int RemoveItems(string itemName, int count = 1)
        {
            if (!_state.CanRemove()) 
            {
                Console.WriteLine("Невозможно удалить предметы в текущем состоянии");
                return 0;
            }
            
            var result = _operations.RemoveByName(itemName, count);
            if (result > 0) UpdateState();
            return result;
        }

        public bool RemoveItemAt(int index)
        {
            if (!_state.CanRemove()) 
            {
                Console.WriteLine("Невозможно удалить предмет в текущем состоянии");
                return false;
            }
            
            if (index < 0 || index >= _items.Count)
                return false;
                
            var result = _operations.RemoveByIndex(index);
            if (result) UpdateState();
            return result;
        }

        public IItem? FindItem(string itemId) => _operations.FindById(itemId);
        
        public IEnumerable<IItem> FindItemsByName(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return Enumerable.Empty<IItem>();
                
            return _items.Where(item => item.Name == name).ToList();
        }
        
        public IItem? GetItemAt(int index)
        {
            if (index < 0 || index >= _items.Count)
                return null;
                
            return _items[index];
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"\n=== ИНВЕНТАРЬ ({Count}/{_maxCapacity}) ===");
            Console.WriteLine($"Состояние: {_state.StateInfo}");
            
            if (Count == 0)
            {
                Console.WriteLine("Инвентарь пуст");
                return;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"[{i}] {_items[i].GetInfo()}");
            }
        }
        
        public bool ContainsItem(string itemId) => _operations.FindById(itemId) != null;
        
        private void UpdateState()
        {
            var newState = Count >= _maxCapacity 
                ? (IInventoryState)new OverloadedState() 
                : new NormalState();
        
            if (newState.GetType() != _state.GetType())
            {
                _state = newState;
                Console.WriteLine($"Состояние изменено: {_state.StateInfo}");
            }
        }
    }
}