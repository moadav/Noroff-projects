using RPG_game_console.Heros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game_console.Exceptions
{
    public class InvalidArmorException : Exception
    {
        public InvalidArmorException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidArmorException" /> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="heroName">Name of the hero.</param>
        public InvalidArmorException(string? message, string heroName) : base(message + heroName)
        {
        }

    }
}
