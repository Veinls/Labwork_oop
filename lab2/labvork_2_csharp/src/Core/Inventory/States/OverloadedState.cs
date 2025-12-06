using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Inventory.Interfaces;

namespace labvork_2_csharp.Core.Inventory.States
{
    internal class OverloadedState : IInventoryState
    {
        public bool CanAddItem => false;
        public bool CanRemoveItem => true;
        public string StateInfo => "Перегруженное состояние - можно только удалять предметы";

        public bool CanAdd(IItem item, int currentCount, int maxCapacity)
        {
            return false;
        }

        public bool CanRemove()
        {
            return true;
        }
    }
}