using labvork_2_csharp.Core.Items.Builders;

namespace labvork_2_tests_csharp
{
    public class ItemBuilderFactoryTests
    {
        [Fact]
        public void ItemBuilderFactory_CreateWeapon_ReturnsWeaponBuilder()
        {
            var builder = ItemBuilderFactory.CreateWeapon();

            Assert.NotNull(builder);
            Assert.IsType<WeaponBuilder>(builder);
        }

        [Fact]
        public void ItemBuilderFactory_CreateItem_WithFluentApi_CreatesWeapon()
        {
            var weapon = ItemBuilderFactory.CreateItem("Test Sword")
                .WithId("test_sword")
                .AsSword(15)
                .Build();

            Assert.NotNull(weapon);
            Assert.Equal("Test Sword", weapon.Name);
            Assert.Equal("test_sword", weapon.Id);
        }
    }
}