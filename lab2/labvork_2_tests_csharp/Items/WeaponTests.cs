using labvork_2_csharp.Core.Items.Concrete;
using labvork_2_csharp.Core.Items;

namespace labvork_2_tests_csharp
{
    public class WeaponTests
    {
        [Fact]
        public void Weapon_Improve_IncreasesLevel()
        {
            var weapon = new Weapon("test_sword", "Стальной Меч", 10, WeaponType.Sword);

            weapon.Improve();

            Assert.True(weapon.AbilityToImprove());
        }

        [Fact]
        public void Weapon_Improve_MaxLevel_ThrowsException()
        {
            var weapon = new Weapon("test_sword", "Стальной Меч", 10, WeaponType.Sword);
            
            for (int i = 0; i < 5; i++)
            {
                weapon.Improve();
            }

            Assert.False(weapon.AbilityToImprove());
            Assert.Throws<InvalidOperationException>(() => weapon.Improve());
        }

        [Fact]
        public void Weapon_Use_ExecutesSuccessfully()
        {
            var weapon = new Weapon("test_sword", "Стальной Меч", 10, WeaponType.Sword);

            var exception = Record.Exception(() => weapon.Use());
            Assert.Null(exception);
        }
    }
}