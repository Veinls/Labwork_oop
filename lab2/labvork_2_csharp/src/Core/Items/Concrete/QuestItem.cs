using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Items.Concrete
{
    public class QuestItem : IItem
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _questId;
        private readonly bool _isQuestRewardItem;
        private readonly int _stackAmount;
        
        public string Id => _id;
        public string Name => _name;
        public string Description { get; private set; }
        public int StackAmount => _stackAmount;

        public QuestItem(string id, string name, string questId, bool isQuestRewardItem = true, int stackAmount = 1)
        {
            _id = !string.IsNullOrWhiteSpace(id) 
                ? id 
                : throw new ArgumentException("ID не может быть пустым");
            
            _name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentException("Название не может быть пустым");
            
            _questId = !string.IsNullOrWhiteSpace(questId)
                ? questId
                : throw new ArgumentException("ID квеста не может быть пустым");
            
            _isQuestRewardItem = isQuestRewardItem;

            _stackAmount = stackAmount > 0
                ? stackAmount
                : throw new ArgumentException("Стак не может быть отрицательным значением");
            
            Description = $"Предмет в качестве награды за квест: {_name}({_questId})";
        }

        public void Use()
        {
            if (_isQuestRewardItem)
            {
                Console.WriteLine($"Ваша награда за квест: {_name}({_questId}). Готов к использованию");
            }
            else
            {
                Console.WriteLine($"Квестовый предмет использован: {_name}({_questId})");
            }
        }

        public string GetInfo()
        {
            string QuestRewardItemInfo = _isQuestRewardItem ? "Готов к использованию" : "Использован";
            return $"{_name}.{QuestRewardItemInfo}: Квест: {_questId}, В стаке: {_stackAmount}";
        }

        public void Improve()
        {
            throw new InvalidOperationException($"Квестовый предмет не поддается улучшению {_name}({_questId})");
        }

        // Будет использоваться в QuestItemBuilder
        // public void SetDescription(string description)
        // {
        //     Description = description ?? throw new ArgumentNullException(nameof(description));
        // }
        
        public bool AbilityToImprove()
            => false;
    }
}