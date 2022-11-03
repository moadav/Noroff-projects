using RPG_game_console.Equipments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.ArmorEnum;
using static RPG_game_console.Enums.WeaponEnum;

namespace RPG_game_console.Heros
{
    public abstract class Hero
    {

        /// <summary>Gets or sets the name.</summary>
        /// <value>The Hero name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the level.</summary>
        /// <value>The hero level.</value>
        public int Level { get; set; } = 1;


        /// <summary>Gets or sets the level attribute.</summary>
        /// <value>The level attribute.</value>
        public HeroAttribute LevelAttribute { get; set; } = new();

        /// <summary>Gets or sets the equipments.</summary>
        /// <value>The equipments of hero .</value>
        public Equipment Equipments { get; set; }

        public Hero(string name)
        {
            Name = name;
            Equipments = new();
            ValidArmorTypes = new();
            ValidWeaponTypes = new();
        }

        public List<WeaponType> ValidWeaponTypes { get; set; } 
        public List<ArmorType> ValidArmorTypes { get; set; } 


        /// <summary>Function to handle the hero Level up.</summary>
        public abstract void LevelUp();

    }
}
