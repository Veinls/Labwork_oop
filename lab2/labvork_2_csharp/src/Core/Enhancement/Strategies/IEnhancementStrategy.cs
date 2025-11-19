using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Enhancement.Strategies
{
    public interface IEnhancementStrategy
    {
        bool CanEnhance(IItem item);
        void Enhance(IItem item);
        string GetEnhancementInfo(IItem item);
    }
}