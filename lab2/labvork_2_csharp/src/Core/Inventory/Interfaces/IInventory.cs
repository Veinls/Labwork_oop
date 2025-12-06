using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Inventory.Interfaces
{
    public interface IInventory
    {
        void AddItem(IItem item);
        bool RemoveItem(string itemId);
        bool RemoveItem(IItem item);
        int RemoveItems(string itemName, int count = 1);
        bool RemoveItemAt(int index);
        
        IItem? FindItem(string itemId);
        IEnumerable<IItem> FindItemsByName(string name);
        IItem? GetItemAt(int index);
        
        bool ContainsItem(string itemId);
        
        void DisplayInventory();
        int Count { get; }
        bool IsFull { get; }
        int MaxCapacity { get; }
        IReadOnlyList<IItem> Items { get; }
        
        string CurrentStateInfo { get; }
        bool CanAddItems { get; }
        bool CanRemoveItems { get; }
    }
}