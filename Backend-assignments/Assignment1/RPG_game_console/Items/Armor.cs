using RPG_game_console.Enums;
using RPG_game_console.Heros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.ArmorEnum;

namespace RPG_game_console.Items
{
    public class Armor: Item
    {
        public Armor(ArmorType armorType, HeroAttribute armorAttribute, string name, int requiredLevel, SlotsEnum.Slot slot) : base(name, requiredLevel, slot)
        {
            ArmorType = armorType;
            ArmorAttribute = armorAttribute;
        }

        /// <summary>Gets or sets the armor attribute.</summary>
        /// <value>The armor attribute.</value>
        public HeroAttribute ArmorAttribute { get; set; }

        /// <summary>Gets or sets the type of the armor.</summary>
        /// <value>The type of the armor.</value>
        public ArmorType ArmorType { get; set; }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, RequiredLevel, Slot, ArmorAttribute, ArmorType);
        }
    }
}
