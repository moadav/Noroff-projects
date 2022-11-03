using RPG_game_console.Enums;
using static RPG_game_console.Enums.SlotsEnum;

namespace RPG_game_console.Items
{
    public abstract class Item
    {

        /// <summary>Gets or sets the item name.</summary>
        /// <value>The item name.</value>
        public string Name { get; set; } = "";

        /// <summary>Gets or sets the tem required level.</summary>
        /// <value>The item required level.</value>
        public int RequiredLevel { get; set; }

        /// <summary>Gets or sets the item slot.</summary>
        /// <value>The item slot.</value>
        public Slot Slot { get; set; }

        protected Item(string name, int requiredLevel, Slot slot)
        {
            Name = name;
            RequiredLevel = requiredLevel;
            Slot = slot;
        }
    }
}
