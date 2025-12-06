namespace labvork_2_csharp.Core.Items.Builders
{
    public static class ItemBuilderFactory
    {
        public static WeaponBuilder CreateWeapon() => new WeaponBuilder();
        public static PotionBuilder CreatePotion() => new PotionBuilder();
        public static ItemTemplate CreateItem(string name)
            => new ItemTemplate(name);
        
        public class ItemTemplate
        {
            private readonly string _name;
            private string? _id;
            
            public ItemTemplate(string name) 
                => _name = name;
            
            public ItemTemplate WithId(string id)
            {
                _id = id;
                return this;
            }

            public WeaponBuilder AsSword(int damage)
                => BuildWeapon(b => b.SetDamage(damage).SetType(WeaponType.Sword));

            public WeaponBuilder AsAxe(int damage) 
                => BuildWeapon(b => b.SetDamage(damage).SetType(WeaponType.Axe));

            public PotionBuilder AsHealthPotion(int healing)
                => BuildPotion(b => b.SetHealing(healing).SetType(PotionType.Health));

            public PotionBuilder AsManaPotion(int healing)
                => BuildPotion(b => b.SetHealing(healing).SetType(PotionType.Mana));

            private WeaponBuilder BuildWeapon(Action<WeaponBuilder> configure)
            {
                var builder = new WeaponBuilder().SetName(_name);
                configure(builder);
                return _id != null ? builder.SetId(_id) : builder;
            }

            private PotionBuilder BuildPotion(Action<PotionBuilder> configure)
            {
                var builder = new PotionBuilder().SetName(_name);
                configure(builder);
                return _id != null ? builder.SetId(_id) : builder;
            }
        }

        
    }
}