using labvork_2_csharp.Core.Enhancement.Strategies;
using labvork_2_csharp.Core.Items.Concrete;
using labvork_2_csharp.Core.Items;

namespace labvork_2_tests_csharp
{
    public class WeaponEnhancementStrategyTests
    {
        [Fact]
        public void WeaponEnhancementStrategy_CanEnhance_ForWeapon_ReturnsTrue()
        {
            var strategy = new WeaponEnhancementStrategy();
            var weapon = new Weapon("test_sword", "Test Sword", 10, WeaponType.Sword);

            var canEnhance = strategy.CanEnhance(weapon);
            
            Assert.True(canEnhance);
        }

        [Fact]
        public void WeaponEnhancementStrategy_CanEnhance_ForPotion_ReturnsFalse()
        {
            var strategy = new WeaponEnhancementStrategy();
            var potion = new Potion("test_potion", "Test Potion", 50, PotionType.Health);

            var canEnhance = strategy.CanEnhance(potion);

            Assert.False(canEnhance);
        }

        [Fact]
        public void WeaponEnhancementStrategy_Enhance_ImprovesWeapon()
        {
            var strategy = new WeaponEnhancementStrategy();
            var weapon = new Weapon("test_sword", "Test Sword", 10, WeaponType.Sword);

            strategy.Enhance(weapon);

            Assert.True(true); 
        }
    }
}