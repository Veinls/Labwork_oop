using labvork_2_csharp.Core.Items.Concrete;

namespace labvork_2_csharp.Core.Items.Builders
{
    public class PotionBuilder : BaseItemBuilder<Potion, PotionBuilder>
    {
        private int _healing;
        private PotionType _type = PotionType.Health;
        private int _stackAmount = 5;

        public PotionBuilder SetHealing(int healing)
        {
            if (healing <= 0)
                throw new ArgumentException("Сила исцеления должна быть положительной");
            
            _healing = healing;
            return this;
        }

        public PotionBuilder SetType(PotionType type)
        {
            _type = type;
            return this;
        }

        public PotionBuilder SetStackAmount(int stackAmount)
        {
            if (stackAmount <= 0)
                throw new ArgumentException("Размер стака должен быть положительным");
            
            _stackAmount = stackAmount;
            return this;
        }

        protected override void Validate()
        {
            base.Validate();
            
            if (_healing <= 0)
                throw new InvalidOperationException("Сила исцеления не установлена");
        }

        public override Potion Build()
        {
            Validate();
            var potion = new Potion(_id!, _name!, _healing, _type, _stackAmount);
            if (!string.IsNullOrEmpty(_description)) 
                potion.SetDescription(_description);
            return potion;
        }
        
        
    }
}