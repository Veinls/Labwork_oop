using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Inventory.Interfaces;

namespace labvork_2_csharp.Core.Inventory.States
{
    internal class NormalState : IInventoryState
    {
        public bool CanAddItem => true;
        public bool CanRemoveItem => true;
        public string StateInfo => "Нормальное состояние - можно добавлять и удалять предметы";

        public bool CanAdd(IItem item, int currentCount, int maxCapacity)
        {
            return currentCount < maxCapacity;
        }

        public bool CanRemove()
        {
            return true;
        }
    }
}