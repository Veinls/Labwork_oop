
using labvork_2_csharp.Core.Items.Interfaces;



namespace labvork_2_csharp.Core.Items.Concrete
{
    public class Armor : IItem
    {
        private readonly string _id;
        private readonly string _name;
        private readonly ArmorType _type;
        private int _defense;
        private int _improvementLevel;
        
        public string Id => _id;
        public string Name => _name;
        public string Description { get; private set; }
        public int StackAmount => 1;

        public Armor(string id, string name, int baseDefense, ArmorType type = ArmorType.Leather)
        {
            _id = !string.IsNullOrWhiteSpace(id) 
                ? id 
                : throw new ArgumentException("ID не может быть пустым");
            
            _name = !string.IsNullOrWhiteSpace(name) 
                ? name 
                : throw new ArgumentException("Название не может быть пустым");
            
            _defense = baseDefense > 0
                ? baseDefense
                : throw new ArgumentException("Защита не может быть отрицательным значением");
            
            _type = type;

            Description = $"Броня {_name} – {_type}";
        }

        public void Use()
        {
            Console.WriteLine($"Броня активирована {_name}({_type}) с защитой {CalculateTotalDefense()}");
        }

        public string GetInfo()
        {
            return $"{_name}({_type}): Защита: {CalculateTotalDefense()}, Уровень:  {_improvementLevel}";
        }

        private int CalculateTotalDefense() 
            => (int)(_defense * Math.Pow(1.15, _improvementLevel)); 

        public void Improve()
        {
            if (!AbilityToImprove())
            {
                throw new InvalidOperationException($"Достигнут максимальный уровень брони {_name}({_type})");
            }
            
            _improvementLevel++;
            Console.WriteLine($"Броня {_name}({_type}) улучшена до уровня {_improvementLevel}. Защита: {CalculateTotalDefense()}");
        }
        
        public bool AbilityToImprove() 
            => _improvementLevel < 5;
        
        // Будет использоваться в ArmorBuilder
        // public void SetDescription(string description)
        // {
        //     Description = description ?? throw new ArgumentNullException(nameof(description));
        // }
    }
}