using RPG_game_console.Equipments;
using RPG_game_console.Exceptions;
using RPG_game_console.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RPG_game_console.Enums.ArmorEnum;
using static RPG_game_console.Enums.SlotsEnum;
using static RPG_game_console.Enums.WeaponEnum;

namespace RPG_game_console.Heros
{
    public class Warrior : Hero, IHero
    {
        public Warrior(string name) : base(name)
        {
            LevelAttribute = new(2, 5, 1);
            ValidArmorTypes = new() { ArmorType.Mail, ArmorType.Plate };
            ValidWeaponTypes = new() { WeaponType.Axes, WeaponType.Hammers, WeaponType.Swords };
        }

        /// <summary>The total amount of hero damage</summary>
        /// <returns>returns the hero damage</returns>
        public int Damage()
        {
            if (Equipments.HeroEquipment[Slot.Weapon] != null)
            {
                Weapon weapon = (Weapon)Equipments.HeroEquipment[Slot.Weapon];
                return weapon.WeaponDamage * (1 + LevelAttribute.Strength / 100);
            }
            else
                return 1;
        }

        /// <summary>Displays the hero current values</summary>
        public void Display()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine("The hero name: " + Name);
            stringBuilder.AppendLine("The hero class: " + "Mage");
            stringBuilder.AppendLine("The hero level: " + Level);
            stringBuilder.AppendLine("The hero total strength: " + LevelAttribute.Strength);
            stringBuilder.AppendLine("The hero total dexterity: " + LevelAttribute.Dexterity);
            stringBuilder.AppendLine("The hero total intelligence: " + LevelAttribute.Intelligence);
            stringBuilder.AppendLine("The hero total damage: " + Damage());
            Console.WriteLine(stringBuilder);
        }

        /// <summary>Equips the specified armor equipment.</summary>
        /// <param name="armorEquipment">The armor equipment.</param>
        /// <exception cref="RPG_game_console.Exceptions.InvalidArmorException">This equipment is too high level to equip for
        /// or
        /// Invalid armor equipped, please equip Mail, Plate for hero</exception>
        public void Equip(Armor armorEquipment)
        {
            bool isValid = false;
            if (armorEquipment.RequiredLevel > Level)
                throw new InvalidArmorException("This equipment is too high level to equip for ", Name);

            foreach (ArmorType armorType in ValidArmorTypes!)
            {
                if (armorEquipment.ArmorType == armorType)
                {
                    isValid = true;
                    Equipments.HeroEquipment[armorEquipment.Slot] = armorEquipment;
                }

            }
            if (!isValid)
                throw new InvalidArmorException("Invalid armor equipped, please equip Mail, Plate for hero ", Name);


        }

        /// <summary>Equips the specified weapon equipment.</summary>
        /// <param name="weaponEquipment">The weapon equipment.</param>
        /// <exception cref="RPG_game_console.Exceptions.InvalidWeaponException">
        /// This equipment is too high level to equip for
        /// or
        /// Invalid weapon equipped, please equip Axes, Swords and Hammers for hero
        /// </exception>
        public void Equip(Weapon weaponEquipment)
        {
            bool isValid = false;

            if (weaponEquipment.RequiredLevel > Level)
                throw new InvalidWeaponException("This equipment is too high level to equip for ", Name);

            foreach (WeaponType WeaponType in ValidWeaponTypes!)
            {
                if (weaponEquipment.WeaponType == WeaponType)
                {
                    isValid = true;
                    Equipments.HeroEquipment[Slot.Weapon] = weaponEquipment;
                }

            }
            if (!isValid)
                throw new InvalidWeaponException("Invalid weapon equipped, please equip Axes, Swords and Hammers for hero ", Name);

        }


        /// <summary>Function to handle the hero Level up.</summary>
        public override void LevelUp()
        {
            Level += 1;
            LevelAttribute.Increase(3, 2, 1);
        }

        /// <summary>The attribute total</summary>
        /// <returns>returns the total attributes of the hero</returns>
        public int TotalAttributes()
        {
            int total = LevelAttribute.GetHashCode();


            foreach (Armor entry in Equipments.HeroEquipment.Values)
            {
                if (entry?.Slot == Slot.Head || entry?.Slot == Slot.Body || entry?.Slot == Slot.Legs)
                {
                    total += entry.ArmorAttribute.GetHashCode();
                }
            }
            return total;
        }
    }
}
