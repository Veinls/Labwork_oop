using labvork_2_csharp.Core.Items.Interfaces;

namespace labvork_2_csharp.Core.Items.Builders
{
    public abstract class BaseEquipmentBuilder<T, TBuilder> : BaseItemBuilder<T, TBuilder>
        where TBuilder : BaseEquipmentBuilder<T, TBuilder>
        where T : IItem
    {
        protected int _power;

        public TBuilder SetPower(int power)
        {
            if (power < 0)
            {
                throw new ArgumentException("Сила не может быть отрицательной");
            }

            _power = power;
            return (TBuilder)this;
        }

        protected override void Validate()
        {
            base.Validate();

            if (_power <= 0)
            {
                throw new InvalidOperationException("Сила не установлена");
            }
        }
    }
}