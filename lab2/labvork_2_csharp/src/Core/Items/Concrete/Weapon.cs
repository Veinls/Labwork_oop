using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Items.Concrete
{
    public class Weapon : IItem
    {
        private readonly string _id;
        private readonly string _name;
        private readonly WeaponType _type;
        private int _baseDamage;
        private int _improvementLevel;
        
        public string Id => _id;
        public string Name => _name;
        public string Description { get; private set; }
        public int StackAmount => 1;

        public Weapon(string id, string name, int baseDamage, WeaponType type = WeaponType.Sword)
        {
            _id = !string.IsNullOrWhiteSpace(id) 
                ? id 
                : throw new ArgumentException("ID не может быть пустым");
            
            _name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentException("Название не может быть пустым");
            
            _baseDamage = baseDamage > 0
                ? baseDamage
                : throw new ArgumentException("Базовый урон не может быть отрицательным значением");
            
            _type = type;

            Description = $"Оружие {_name} – {_type}";
        }

        public void Use()
        {
            Console.WriteLine($"Производится атака соперника {_name}({_type}) с уроном {CalculateTotalDamage()}");
        }

        public string GetInfo()
        {
            return $"{_name}({_type}): Урон:  {CalculateTotalDamage()}, Уровень:  {_improvementLevel}";
        }
        
        private int CalculateTotalDamage() 
            => (int)(_baseDamage * Math.Pow(1.15, _improvementLevel)); 
        

        public void Improve()
        {
            if (!AbilityToImprove())
            {
                throw new InvalidOperationException($"Достигнут максимальный уровень оружия {_name}({_type})");
            }
            
            _improvementLevel++;
            Console.WriteLine($"Оружие {_name}({_type}) улучшено до уровня {_improvementLevel}. Урон: {CalculateTotalDamage()}");
        }
        
        public void SetDescription(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
        
        public bool AbilityToImprove() 
            => _improvementLevel < 5;
        
    }
}