using RPG_game_console.Heros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game_console.Exceptions
{
    public class InvalidWeaponException : Exception
    {
        public InvalidWeaponException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidWeaponException" /> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="heroName">Name of the hero.</param>
        public InvalidWeaponException(string? message, string heroName) : base(message + heroName)
        {
        }

    }
}
