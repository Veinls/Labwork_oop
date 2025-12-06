using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Inventory.Interfaces
{
    public interface IInventoryState
    {
        bool CanAddItem { get; }
        bool CanRemoveItem { get; }
        string StateInfo { get; }
        
        bool CanAdd(IItem item, int currentCount, int maxCapacity);
        bool CanRemove();
    }
}