using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokerDice.Services.Enums;

namespace PokerDice.Services.Helpers
{
    /// <summary>
    /// Extensions
    /// </summary>
    public class Extensions
    {
        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public string GetEnumDescription(PokerDiceEnum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0 ? enumValue.ToString() : ((DescriptionAttribute)attributes[0]).Description;
        }

        /// <summary>
        /// Gets the enum description in sequence.
        /// </summary>
        /// <returns></returns>
        public string GetEnumDescriptionInSequence()
        {
            var result = new StringBuilder();
            foreach (PokerDiceEnum eachEnum in Enum.GetValues(typeof(PokerDiceEnum)))
            {
                result.Append(GetEnumDescription(eachEnum));
            }

            return result.ToString();
        }

        /// <summary>
        /// Determines whether [has two pairs] [the specified roll].
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <returns>
        ///   <c>true</c> if [has two pairs] [the specified roll]; otherwise, <c>false</c>.
        /// </returns>
        public  bool HasTwoPairs(string roll)
        {
            return (Regex.Match(roll, @"([AKQJ9])\1").Length == 4 || Regex.Match(roll, @"([1010])\1").Length == 8);
        }
    }
}
