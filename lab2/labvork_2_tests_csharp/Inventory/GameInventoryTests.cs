using labvork_2_csharp.Core.Inventory.Concrete;
using labvork_2_csharp.Core.Items.Concrete;
using labvork_2_csharp.Core.Items;

namespace labvork_2_tests_csharp
{
    public class GameInventoryTests
    {
        [Fact]
        public void GameInventory_AddItem_SuccessfullyAdds()
        {
            var inventory = new GameInventory(5);
            var weapon = new Weapon("test_sword", "Стальной меч", 10, WeaponType.Sword);

            inventory.AddItem(weapon);

            Assert.Equal(1, inventory.Count);
            Assert.True(inventory.ContainsItem("test_sword"));
        }

        [Fact]
        public void GameInventory_AddItem_WhenFull_DoesNotAdd()
        {
            var inventory = new GameInventory(1);
            var weapon1 = new Weapon("sword1", "Sword 1", 10, WeaponType.Sword);
            var weapon2 = new Weapon("sword2", "Sword 2", 15, WeaponType.Sword);
            
            inventory.AddItem(weapon1);
            inventory.AddItem(weapon2);

            Assert.Equal(1, inventory.Count);
            Assert.False(inventory.ContainsItem("sword2"));
        }

        [Fact]
        public void GameInventory_RemoveItem_RemovesSuccessfully()
        {
            var inventory = new GameInventory(5);
            var weapon = new Weapon("test_sword", "Стальной меч", 10, WeaponType.Sword);
            inventory.AddItem(weapon);

            var result = inventory.RemoveItem("test_sword");

            Assert.True(result);
            Assert.Equal(0, inventory.Count);
        }

        [Fact]
        public void GameInventory_FindItem_ReturnsCorrectItem()
        {
            var inventory = new GameInventory(5);
            var weapon = new Weapon("test_sword", "Стальной меч", 10, WeaponType.Sword);
            inventory.AddItem(weapon);
            
            var foundItem = inventory.FindItem("test_sword");

            Assert.NotNull(foundItem);
            Assert.Equal("test_sword", foundItem.Id);
            Assert.Equal("Стальной меч", foundItem.Name);
        }
    }
}