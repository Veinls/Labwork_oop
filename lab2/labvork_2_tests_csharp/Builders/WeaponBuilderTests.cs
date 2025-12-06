using labvork_2_csharp.Core.Items.Builders;
using labvork_2_csharp.Core.Items;

namespace labvork_2_tests_csharp
{
    public class WeaponBuilderTests
    {
        [Fact]
        public void WeaponBuilder_Build_CreatesValidWeapon()
        {
            var builder = new WeaponBuilder()
                .SetId("test_sword")
                .SetName("Test Sword")
                .SetDamage(15)
                .SetType(WeaponType.Sword);

            var weapon = builder.Build();

            Assert.NotNull(weapon);
            Assert.Equal("test_sword", weapon.Id);
            Assert.Equal("Test Sword", weapon.Name);
        }

        [Fact]
        public void WeaponBuilder_Build_WithoutRequiredFields_ThrowsException()
        {
            var builder = new WeaponBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
    }
}