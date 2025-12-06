using labvork_2_csharp.Core.Items.Interfaces;
using labvork_2_csharp.Core.Items.Concrete;

namespace labvork_2_csharp.Core.Enhancement.Strategies
{
    internal class PotionEnhancementStrategy : IEnhancementStrategy
    {
        public bool CanEnhance(IItem item)
        {
            return false;
        }

        public void Enhance(IItem item)
        {
            throw new InvalidOperationException("Зелья нельзя улучшать");
        }

        public string GetEnhancementInfo(IItem item)
        {
            return item is Potion ? "Зелья нельзя улучшать" : "Этот предмет не является зельем";
        }
    }
}