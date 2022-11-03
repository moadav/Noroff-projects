using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game_console.Heros
{
    public class HeroAttribute
    {

        /// <summary>Gets or sets the strength.</summary>
        /// <value>The strength value.</value>
        public int Strength { get; set; }

        /// <summary>Gets or sets the dexterity.</summary>
        /// <value>The dexterity value.</value>
        public int Dexterity { get; set; }
        /// <summary>Gets or sets the intelligene.</summary>
        /// <value>The intelligence value.</value>
        public int Intelligence { get; set; }
        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
        }
        public HeroAttribute()
        {

        }


        /// <summary>Increases strength, dexterity, intelligence by specified amount</summary>
        /// <param name="strength">The strength value.</param>
        /// <param name="dexterity">The dexterity value.</param>
        /// <param name="intelligence">The intelligence value.</param>
        public void Increase(int strength, int dexterity, int intelligence)
        {
            Strength += strength;
            Dexterity += dexterity;
            Intelligence += intelligence;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <span class="keyword">
        ///     <span class="languageSpecificText">
        ///       <span class="cs">true</span>
        ///       <span class="vb">True</span>
        ///       <span class="cpp">true</span>
        ///     </span>
        ///   </span>
        ///   <span class="nu">
        ///     <span class="keyword">true</span> (<span class="keyword">True</span> in Visual Basic)</span> if the specified object  is equal to the current object; otherwise, <span class="keyword"><span class="languageSpecificText"><span class="cs">false</span><span class="vb">False</span><span class="cpp">false</span></span></span><span class="nu"><span class="keyword">false</span> (<span class="keyword">False</span> in Visual Basic)</span>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is HeroAttribute attribute &&
                   Strength == attribute.Strength &&
                   Dexterity == attribute.Dexterity &&
                   Intelligence == attribute.Intelligence;
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Strength, Dexterity, Intelligence);
        }
    }
}
