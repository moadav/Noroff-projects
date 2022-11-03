using RPG_game_console.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.WeaponEnum;

namespace RPG_game_console.Items
{

    public class Weapon : Item
    {


        /// <summary>Gets or sets the weapon damage.</summary>
        /// <value>The weapon damage.</value>
        public int WeaponDamage { get; set; }

        /// <summary>Gets or sets the type of the weapon.</summary>
        /// <value>The type of the weapon.</value>
        public WeaponType WeaponType { get; set; }
        public Weapon(WeaponType weaponType, int weaponDamage, string name, int requiredLevel, SlotsEnum.Slot slot) : base(name, requiredLevel, slot)
        {
            WeaponDamage = weaponDamage;
            WeaponType = weaponType;
        }
    }
}
