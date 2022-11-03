using RPG_game_console.Equipments;
using RPG_game_console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.SlotsEnum;

namespace RPG_game_console.Heros
{
    public interface IHero
    {

        /// <summary>Level up method for hero</summary>
        void LevelUp();

        /// <summary>Equips the specified armor equipment.</summary>
        /// <param name="armorEquipment">The armor equipment.</param>
        void Equip(Armor armorEquipment);

        /// <summary>Equips the specified weapon equipment.</summary>
        /// <param name="weaponEquipment">The weapon equipment.</param>
        void Equip(Weapon weaponEquipment);

        /// <summary>The total amount of hero damage</summary>
        /// <returns>returns the hero damage</returns>
        public int Damage();

        /// <summary>Displays the hero current values</summary>
        public void Display();

        /// <summary>The attribute total</summary>
        /// <returns>returns the total attributes of the hero</returns>
        public int TotalAttributes();




    }
}
