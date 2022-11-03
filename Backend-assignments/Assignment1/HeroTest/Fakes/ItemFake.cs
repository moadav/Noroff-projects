using RPG_game_console.Heros;
using RPG_game_console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.ArmorEnum;
using static RPG_game_console.Enums.SlotsEnum;
using static RPG_game_console.Enums.WeaponEnum;

namespace HeroTest.Fakes
{
    public static class ItemFake
    {

        public static Weapon WeaponItem(WeaponType weaponType, int damage, string name, int requiredLevel, Slot slot)
        {
            return new(weaponType, damage, name, requiredLevel, slot);
        }

        public static Armor ArmorItem(ArmorType armorType, HeroAttribute heroAttribute, string name, int requiredLevel, Slot slot)
        {
            return new(armorType, heroAttribute, name, requiredLevel, slot);
        }
    }
}
