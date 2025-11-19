using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Items.Concrete
{
    public class Potion : IItem
    {
        private readonly string _id;
        private readonly string _name;
        private readonly PotionType _type;
        private readonly int _healing;
        private readonly int _stackAmount;
        
        public string Id => _id;
        public string Name => _name;
        public string Description { get; private set; }
        public int StackAmount => _stackAmount;

        public Potion(string id, string name, int baseHealing, PotionType type = PotionType.Health, int stackAmount = 5)
        {
            _id = !string.IsNullOrWhiteSpace(id) 
                ? id 
                : throw new ArgumentException("ID не может быть пустым");
            
            _name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentException("Название не может быть пустым");
            
            _healing = baseHealing > 0
                ? baseHealing
                : throw new ArgumentException("Сила исцеления не может быть отрицательным значением");
            
            _type = type;

            _stackAmount = stackAmount > 0
                ? stackAmount
                : throw new ArgumentException("Стак не может быть отрицательным значением");
            
            Description = $"Сила исцеления {_name} – {_type}";
        }

        public void Use()
        {
            Console.WriteLine($"Исцеление активировано  {_name}({_type}) с силой исцеления {_healing}");
        }

        public string GetInfo()
        {
            return $"{_name}({_type}): Сила исцеления: {_healing}, Уровень:  {_stackAmount}";
        }

        public void Improve()
        {
            throw new InvalidOperationException($"Зелье не поддается улучшению {_name}({_type})");
        }
        
        public void SetDescription(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public bool AbilityToImprove()
            => false;
    }
}