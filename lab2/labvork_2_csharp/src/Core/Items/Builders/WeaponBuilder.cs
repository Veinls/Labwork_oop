using labvork_2_csharp.Core.Items.Concrete;

namespace labvork_2_csharp.Core.Items.Builders
{
    public class WeaponBuilder : BaseEquipmentBuilder<Weapon, WeaponBuilder>
    {
        private WeaponType _type = WeaponType.Sword;

        public WeaponBuilder SetType(WeaponType type)
        {
            _type = type;
            return this;
        }

        public WeaponBuilder SetDamage(int damage)
        {
            SetPower(damage);
            return this;
        }

        public override Weapon Build()
        {
            Validate();
            var weapon = new Weapon(_id!, _name!, _power, _type);
            if (!string.IsNullOrEmpty(_description)) 
                weapon.SetDescription(_description); 
            return weapon;
        }
    }
}