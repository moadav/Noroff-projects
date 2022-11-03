using HeroTest.Fakes;
using RPG_game_console.Exceptions;
using RPG_game_console.Heros;
using RPG_game_console.Items;
using static RPG_game_console.Enums.ArmorEnum;
using static RPG_game_console.Enums.SlotsEnum;
using static RPG_game_console.Enums.WeaponEnum;

namespace HeroTest
{
    public class ItemTesting
    {
        [Fact]
        public void WhenWeaponCreatedCorrect_Damage()
        {
            int weaponDamage = 12;
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, weaponDamage, "Mighty Staff", 1, Slot.Weapon);

            int actual = weaponDamage;
            int expected = weapon.WeaponDamage;


            Assert.Equal(actual, expected);

        }

        [Fact]
        public void WhenWeaponCreatedCorrect_Name()
        {
            string name = "Mighty staff";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, name, 1, Slot.Weapon);

            string actual = name;
            string expected = weapon.Name;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void WhenWeaponCreatedCorrect_RequiredLevel()
        {
          
            int requiredLevel = 2;

            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty Staff", requiredLevel, Slot.Weapon);

            int actual = requiredLevel;
            int expected = weapon.RequiredLevel;

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void WhenWeaponCreatedCorrect_WeaponType()
        {
            WeaponType axes = WeaponType.Axes;
            Weapon weapon = ItemFake.WeaponItem(axes, 12, "Mighty Staff", 1, Slot.Weapon);

            WeaponType actual = axes;
            WeaponType expected = weapon.WeaponType;

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void WhenWeaponCreatedCorrect_SlotPlace()
        {

            Slot slot = Slot.Weapon;

            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty Staff", 1, slot);

            Slot actual = slot;
            Slot expected = weapon.Slot;

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void WhenArmorCreatedCorrect_Attributes()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);

            HeroAttribute actual = armorAttribute;
            HeroAttribute expected = armor.ArmorAttribute;

            Assert.Equal(actual, expected);
        }



        [Fact]
        public void WhenArmorCreatedCorrect_Level()
        {
            int level = 2;
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", level, Slot.Head);

            int expected = armor.RequiredLevel;
            int actual = level;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void WhenArmorCreatedCorrect_Name()
        { 
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string wepName = "bobo";

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, wepName, 1, Slot.Head);
            
            string expected = armor.Name;
            string actual = wepName;
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void WhenArmorCreatedCorrect_Slot()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            
            Slot expected = armor.Slot;
            Slot actual = Slot.Head;

            Assert.Equal(actual, expected);
        }


        [Fact]
        public void WhenArmorCreatedCorrect_ArmorType()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            
            ArmorType expected = armor.ArmorType;
            ArmorType actual = ArmorType.Cloth;
            
            Assert.Equal(actual, expected);
        }


        [Fact]
        public void Hero_ShouldEquipArmorIf_AllRequirementsMet()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            bool expected = blackbeard.Equipments.HeroEquipment[armor.Slot] != null;

            Assert.True(expected);
        }

        [Fact]
        public void Item_ShouldThrowInvalidArmorExceptionIf_LevelNotMet()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 11, Slot.Head);


            Assert.Throws<InvalidArmorException>(() => blackbeard.Equip(armor));


        }

        [Fact]
        public void Item_ShouldThrowInvalidArmorExceptionIf_InvalidType()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);


            Assert.Throws<InvalidArmorException>(() => blackbeard.Equip(armor));


        }


        [Fact]
        public void Hero_ShouldEquipWeaponIf_AllRequirementsMet()
        {

            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty Staff", 1, Slot.Weapon);
            string heroName = "MOH";
            Mage blackbeard = new(heroName);
            blackbeard.Equip(weapon);

            bool expected = blackbeard.Equipments.HeroEquipment[weapon.Slot] != null;

            Assert.True(expected);
        }


        [Fact]
        public void Item_ShouldThrowInvalidWeaponExceptionIf_LevelNotMet()
        {
            string heroName = "MOH";

            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty Staff", 21, Slot.Weapon);
            Mage blackbeard = new(heroName);

            Assert.Throws<InvalidWeaponException>(() => blackbeard.Equip(weapon));
        }

        [Fact]
        public void Item_ShouldThrowInvalidWeaponExceptionIf_InvalidType()
        {
            string heroName = "MOH";


            Weapon weapon = ItemFake.WeaponItem(WeaponType.Axes, 12, "Mighty Staff", 1, Slot.Weapon);
            Mage blackbeard = new(heroName);

            Assert.Throws<InvalidWeaponException>(() => blackbeard.Equip(weapon));
        }

       

    }
}
