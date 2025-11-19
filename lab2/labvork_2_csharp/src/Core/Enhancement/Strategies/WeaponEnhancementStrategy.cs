using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Items.Concrete;

namespace labvork_2_csharp.Core.Enhancement.Strategies
{
    public  class WeaponEnhancementStrategy : IEnhancementStrategy
    {
        public bool CanEnhance(IItem item)
        {
            return item is Weapon weapon && weapon.AbilityToImprove();
        }

        public void Enhance(IItem item)
        {
            if (!CanEnhance(item))
                throw new InvalidOperationException("Невозможно улучшить это оружие");

            var weapon = item as Weapon;
            weapon?.Improve();
        }

        public string GetEnhancementInfo(IItem item)
        {
            if (item is Weapon weapon)
            {
                var canImprove = weapon.AbilityToImprove();
                return $"Оружие {weapon.Name}: {(canImprove ? "можно улучшить" : "максимальный уровень")}";
            }
            return $"{item.Name} – этот предмет не является оружием";
        }
    }
}