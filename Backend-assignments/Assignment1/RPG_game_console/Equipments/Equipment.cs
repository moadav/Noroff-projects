using RPG_game_console.Items;
using static RPG_game_console.Enums.SlotsEnum;

namespace RPG_game_console.Equipments
{
    public class Equipment
    {

        /// <summary>Gets or sets the hero equipment.</summary>
        /// <value>The hero equipment.</value>
        public Dictionary<Slot, Item?> HeroEquipment { get; set; }


        public Equipment()
        {
            HeroEquipment = new()
            {
                { Slot.Weapon, null },
                { Slot.Legs, null },
                { Slot.Body, null },
                { Slot.Head, null }
            };
        }
    }
}
