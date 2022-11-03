using HeroTest.Fakes;
using RPG_game_console.Heros;
using RPG_game_console.Items;
using System.Reflection.Emit;
using static RPG_game_console.Enums.ArmorEnum;
using static RPG_game_console.Enums.SlotsEnum;
using static RPG_game_console.Enums.WeaponEnum;

namespace HeroTest
{
    public class HeroTesting
    {
        [Fact]
        public void Hero_MageShouldHaveCorrect_Attributes()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 8;
            string name = "MOH";

            Mage blackbeard = new(name);

            HeroAttribute expected = new HeroAttribute(strength, dexterity, intelligence);

            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hero_MageShouldHaveCorrect_Name()
        {
            string name = "MOH";

            Mage blackbeard = new(name);

            string expected = name;
            string actual = blackbeard.Name;
  

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Hero_MageShouldHaveCorrect_Level()
        {
            string name = "MOH";
            int level = 1;

            Mage blackbeard = new(name);


            int expected = level;
            int actual = blackbeard.Level;


            Assert.Equal(expected, actual);
        }
      


        [Fact]
        public void Hero_WarriorShouldHaveCorrect_Attributes()
        {
            int strength = 2;
            int dexterity = 5;
            int intelligence = 1;
            string name = "MOH";

            Warrior blackbeard = new(name);

            HeroAttribute expected = new(strength, dexterity, intelligence);

            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hero_WarriorShouldHaveCorrect_Name()
        {
            string name = "MOH";

            Warrior blackbeard = new(name);

            string expected = name;
            string actual = blackbeard.Name;


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Hero_WarriorShouldHaveCorrect_Level()
        {
            string name = "MOH";
            int level = 1;

            Warrior blackbeard = new(name);

            int expected = level;
            int actual = blackbeard.Level;


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hero_RogueShouldHaveCorrect_Attributes()
        {
            int strength = 2;
            int dexterity = 6;
            int intelligence = 1;
            string name = "MOH";

            Rogue blackbeard = new(name);

            HeroAttribute expected = new HeroAttribute(strength, dexterity, intelligence);

            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hero_RogueShouldHaveCorrect_Name()
        {
            string name = "MOH";

            Rogue blackbeard = new(name);

            string expected = name;
            string actual = blackbeard.Name;


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Hero_RogueShouldHaveCorrect_Level()
        {
            string name = "MOH";
            int level = 1;

            Rogue blackbeard = new(name);

            int expected = level;
            int actual = blackbeard.Level;


            Assert.Equal(expected, actual);
        }



        [Fact]
        public void Hero_RangerShouldHaveCorrect_Attributes()
        {
            int strength = 1;
            int dexterity = 7;
            int intelligence = 1;
            string name = "MOH";

            Ranger blackbeard = new(name);

            HeroAttribute expected = new(strength, dexterity, intelligence);

            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hero_RangerShouldHaveCorrect_Name()
        {
            string name = "MOH";

            Ranger blackbeard = new(name);

            string expected = name;
            string actual = blackbeard.Name;


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Hero_RangerShouldHaveCorrect_Level()
        {
            string name = "MOH";
            int level = 1;

            Ranger blackbeard = new(name);

            int expected = level;
            int actual = blackbeard.Level;


            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Hero_MageShouldIncreaseAttributesCorrectlyWhen_LevelUp()
        {
            int newLevelStrength = 2;
            int newLevelDexterity = 2;
            int newLevelIntelligence = 13;
            string name = "MOH";

            Mage blackbeard = new(name);
            blackbeard.LevelUp();
            
            HeroAttribute expected = new(newLevelStrength, newLevelDexterity, newLevelIntelligence);
            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);
  
        }

        [Fact]
        public void Hero_RangerShouldIncreaseAttributesCorrectlyWhen_LevelUp()
        {
            int newLevelStrength = 2;
            int newLevelDexterity = 12;
            int newLevelIntelligence = 2;
            string name = "MOH";

            Ranger blackbeard = new(name);
            blackbeard.LevelUp();
           
            HeroAttribute expected = new(newLevelStrength, newLevelDexterity, newLevelIntelligence);
            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);

        }

       


        [Fact]
        public void Hero_RogueShouldIncreaseAttributesCorrectlyWhen_LevelUp()
        {
            int newLevelStrength = 3;
            int newLevelDexterity = 10;
            int newLevelIntelligence = 2;
            string name = "MOH";

            Rogue blackbeard = new(name);
            blackbeard.LevelUp();
           
            HeroAttribute expected = new(newLevelStrength, newLevelDexterity, newLevelIntelligence);
            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldIncreaseAttributesCorrectlyWhen_LevelUp()
        {
            int newLevelStrength = 5;
            int newLevelDexterity = 7;
            int newLevelIntelligence = 2;
            string name = "MOH";

            Warrior blackbeard = new(name);
            blackbeard.LevelUp();
           
            HeroAttribute expected = new(newLevelStrength, newLevelDexterity, newLevelIntelligence);
            HeroAttribute actual = blackbeard.LevelAttribute;

            Assert.Equal(expected, actual);

        }


        [Fact]
        public void Hero_MageShouldDisplayCorrectTotalAttributes_WithOneEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            int expected =  heroAttribute.GetHashCode() + armorAttribute.GetHashCode();
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldDisplayCorrectTotalAttributes_WithOneEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Warrior blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode();
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }
        [Fact]
        public void Hero_RogueShouldDisplayCorrectTotalAttributes_WithOneEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Rogue blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode();
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }
        [Fact]
        public void Hero_RangerShouldDisplayCorrectTotalAttributes_WithOneEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Ranger blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode();
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_MageShouldDisplayCorrectTotalAttributes_WithTwoEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Cloth, armorAttributeBody, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldDisplayCorrectTotalAttributes_WithTwoEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Warrior blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeBody, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldDisplayCorrectTotalAttributes_WithTwoEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Rogue blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeBody, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerShouldDisplayCorrectTotalAttributes_WithTwoEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Ranger blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeBody, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_MageShouldDisplayCorrectTotalAttributes_WithThreeEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);


            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);

            HeroAttribute armorAttributeLegs = new(strength, dexterity, intelligence);
            Armor armorLegs = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Legs);
            blackbeard.Equip(armorLegs);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode() + armorAttributeLegs.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }
        [Fact]
        public void Hero_WarriorShouldDisplayCorrectTotalAttributes_WithThreeEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Warrior blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);


            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);

            HeroAttribute armorAttributeLegs = new(strength, dexterity, intelligence);
            Armor armorLegs = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Legs);
            blackbeard.Equip(armorLegs);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode() + armorAttributeLegs.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldDisplayCorrectTotalAttributes_WithThreeEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Rogue blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);


            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);

            HeroAttribute armorAttributeLegs = new(strength, dexterity, intelligence);
            Armor armorLegs = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Legs);
            blackbeard.Equip(armorLegs);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode() + armorAttributeLegs.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerShouldDisplayCorrectTotalAttributes_WithThreeEquipment()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Ranger blackbeard = new(heroName);

            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;

            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);


            HeroAttribute armorAttributeBody = new(strength, dexterity, intelligence);
            Armor armorBody = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Body);
            blackbeard.Equip(armorBody);

            HeroAttribute armorAttributeLegs = new(strength, dexterity, intelligence);
            Armor armorLegs = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Legs);
            blackbeard.Equip(armorLegs);


            int expected = heroAttribute.GetHashCode() + armorAttribute.GetHashCode() + armorAttributeBody.GetHashCode() + armorAttributeLegs.GetHashCode();

            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }


        [Fact]
        public void Hero_MageShouldDisplayCorrectTotalAttributesAfterReplacing_HeadArmor()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Mage blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            
            strength = 1;
            dexterity = 24;
            intelligence = 1;
            HeroAttribute armorAttributeNew = new(strength, dexterity, intelligence);
            Armor armor2 = ItemFake.ArmorItem(ArmorType.Cloth, armorAttributeNew, "Mighty armor XXL", 1, Slot.Head);
            blackbeard.Equip(armor2);

            int expected = heroAttribute.GetHashCode() + armorAttributeNew.GetHashCode();
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldDisplayCorrectTotalAttributesAfterReplacing_HeadArmor()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Warrior blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            strength = 1;
            dexterity = 24;
            intelligence = 1;
            HeroAttribute armorAttributeNew = new(strength, dexterity, intelligence);
            Armor armor2 = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeNew, "Mighty armor XXL", 1, Slot.Head);
            blackbeard.Equip(armor2);

            int expected = heroAttribute.GetHashCode() + armorAttributeNew.GetHashCode();
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldDisplayCorrectTotalAttributesAfterReplacing_HeadArmor()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Rogue blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            strength = 1;
            dexterity = 24;
            intelligence = 1;
            HeroAttribute armorAttributeNew = new(strength, dexterity, intelligence);
            Armor armor2 = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeNew, "Mighty armor XXL", 1, Slot.Head);
            blackbeard.Equip(armor2);

            int expected = heroAttribute.GetHashCode() + armorAttributeNew.GetHashCode();
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_ShouldDisplayCorrectTotalAttributesAfterReplacing_HeadArmor()
        {
            int strength = 1;
            int dexterity = 1;
            int intelligence = 1;
            string heroName = "MOH";

            Ranger blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            HeroAttribute heroAttribute = blackbeard.LevelAttribute;
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);

            strength = 1;
            dexterity = 24;
            intelligence = 1;
            HeroAttribute armorAttributeNew = new(strength, dexterity, intelligence);
            Armor armor2 = ItemFake.ArmorItem(ArmorType.Mail, armorAttributeNew, "Mighty armor XXL", 1, Slot.Head);
            blackbeard.Equip(armor2);

            int expected = heroAttribute.GetHashCode() + armorAttributeNew.GetHashCode();
            int actual = blackbeard.TotalAttributes();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_MageShouldBeCalculatedCorrectlyWith_NoWeapons()
        {
            int damage = 1;
            string heroName = "MOH";
            Mage blackbeard = new(heroName);
           
            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldBeCalculatedCorrectlyWith_NoWeapons()
        {
            int damage = 1;
            string heroName = "MOH";
            Warrior blackbeard = new(heroName);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldBeCalculatedCorrectlyWith_NoWeapons()
        {
            int damage = 1;
            string heroName = "MOH";
            Rogue blackbeard = new(heroName);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerShouldBeCalculatedCorrectlyWith_NoWeapons()
        {
            int damage = 1;
            string heroName = "MOH";
            Ranger blackbeard = new(heroName);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_MageShouldBeCalculatedCorrectlyWith_weapon()
        {
            int damage = 12;
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty staff", 1, Slot.Weapon);
            Mage blackbeard = new(heroName);
            blackbeard.Equip(weapon);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldBeCalculatedCorrectlyWith_weapon()
        {
            int damage = 12;
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Axes, 12, "Mighty staff", 1, Slot.Weapon);
            Warrior blackbeard = new(heroName);
            blackbeard.Equip(weapon);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldBeCalculatedCorrectlyWith_weapon()
        {
            int damage = 12;
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Daggers, 12, "Mighty staff", 1, Slot.Weapon);
            Rogue blackbeard = new(heroName);
            blackbeard.Equip(weapon);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerShouldBeCalculatedCorrectlyWith_weapon()
        {
            int damage = 12;
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Bows, 12, "Mighty staff", 1, Slot.Weapon);
            Ranger blackbeard = new(heroName);
            blackbeard.Equip(weapon);

            int expected = damage;
            int actual = blackbeard.Damage();


            Assert.Equal(expected, actual);

        }



        [Fact]
        public void Hero_MageShouldBeCalculatedCorrectlyWith_ReplacedWeapon()
        {
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty staff", 1, Slot.Weapon);
            Mage blackbeard = new(heroName);
            blackbeard.Equip(weapon);
            Weapon weaponBetter = ItemFake.WeaponItem(WeaponType.Staffs, 22, "Mighty staff XX2", 1, Slot.Weapon);
            blackbeard.Equip(weaponBetter);      
           
            int actual = blackbeard.Damage();
            int expected = weaponBetter.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorShouldBeCalculatedCorrectlyWith_ReplacedWeapon()
        {
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Axes, 12, "Mighty staff", 1, Slot.Weapon);
            Warrior blackbeard = new(heroName);
            blackbeard.Equip(weapon);
            Weapon weaponBetter = ItemFake.WeaponItem(WeaponType.Axes, 22, "Mighty staff XX2", 1, Slot.Weapon);
            blackbeard.Equip(weaponBetter);

            int actual = blackbeard.Damage();
            int expected = weaponBetter.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueShouldBeCalculatedCorrectlyWith_ReplacedWeapon()
        {
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Daggers, 12, "Mighty staff", 1, Slot.Weapon);
            Rogue blackbeard = new(heroName);
            blackbeard.Equip(weapon);
            Weapon weaponBetter = ItemFake.WeaponItem(WeaponType.Daggers, 22, "Mighty staff XX2", 1, Slot.Weapon);
            blackbeard.Equip(weaponBetter);

            int actual = blackbeard.Damage();
            int expected = weaponBetter.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerShouldBeCalculatedCorrectlyWith_ReplacedWeapon()
        {
            string heroName = "MOH";
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Bows, 12, "Mighty staff", 1, Slot.Weapon);
            Ranger blackbeard = new(heroName);
            blackbeard.Equip(weapon);
            Weapon weaponBetter = ItemFake.WeaponItem(WeaponType.Bows, 22, "Mighty staff XX2", 1, Slot.Weapon);
            blackbeard.Equip(weaponBetter);

            int actual = blackbeard.Damage();
            int expected = weaponBetter.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_MageAttribute_ShouldBeCalculatedCorrectlyWithWeaponAndArmor()
        {

            int strength = 4;
            int dexterity = 4;
            int intelligence = 4;
            string heroName = "MOH";
            Mage blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Cloth, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Staffs, 12, "Mighty Staff", 1, Slot.Weapon);
            blackbeard.Equip(weapon);

            int actual = blackbeard.Damage();
            int expected = weapon.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_WarriorAttribute_ShouldBeCalculatedCorrectlyWithWeaponAndArmor()
        {

            int strength = 4;
            int dexterity = 4;
            int intelligence = 4;
            string heroName = "MOH";
            Warrior blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Axes, 12, "Mighty Staff", 1, Slot.Weapon);
            blackbeard.Equip(weapon);

            int actual = blackbeard.Damage();
            int expected = weapon.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RogueAttribute_ShouldBeCalculatedCorrectlyWithWeaponAndArmor()
        {

            int strength = 4;
            int dexterity = 4;
            int intelligence = 4;
            string heroName = "MOH";
            Rogue blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Daggers, 12, "Mighty Staff", 1, Slot.Weapon);
            blackbeard.Equip(weapon);

            int actual = blackbeard.Damage();
            int expected = weapon.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Hero_RangerAttribute_ShouldBeCalculatedCorrectlyWithWeaponAndArmor()
        {

            int strength = 4;
            int dexterity = 4;
            int intelligence = 4;
            string heroName = "MOH";
            Ranger blackbeard = new(heroName);
            HeroAttribute armorAttribute = new(strength, dexterity, intelligence);
            Armor armor = ItemFake.ArmorItem(ArmorType.Mail, armorAttribute, "Mighty armor", 1, Slot.Head);
            blackbeard.Equip(armor);
            Weapon weapon = ItemFake.WeaponItem(WeaponType.Bows, 12, "Mighty Staff", 1, Slot.Weapon);
            blackbeard.Equip(weapon);

            int actual = blackbeard.Damage();
            int expected = weapon.WeaponDamage * (1 + blackbeard.LevelAttribute.Intelligence / 100);

            Assert.Equal(expected, actual);

        }




    }
}