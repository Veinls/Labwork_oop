namespace labvork_2_csharp.Core.Items.Interfaces
{
    public interface IItem
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        int StackAmount { get; }
        
        void Use();
        string GetInfo();
        void Improve();
        bool AbilityToImprove();
    }
}