using labvork_2_csharp.Core.Items.Concrete;
using labvork_2_csharp.Core.Items;

namespace labvork_2_tests_csharp
{
    public class PotionTests
    {
        [Fact]
        public void Potion_Improve_ThrowsException()
        {
            var potion = new Potion("test_potion", "Зелье здоровья", 50, PotionType.Health);

            Assert.Throws<InvalidOperationException>(() => potion.Improve());
        }

        [Fact]
        public void Potion_AbilityToImprove_ReturnsFalse()
        {
            var potion = new Potion("test_potion", "Зелье здоровья", 50, PotionType.Health);

            Assert.False(potion.AbilityToImprove());
        }
    }
}