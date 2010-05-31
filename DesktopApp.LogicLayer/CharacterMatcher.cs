using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// The CharacterMatcher class calculates a matching score between a character and a 
    /// number of radicals.
    /// </summary>
    public class CharacterMatcher : ICharacterMatcher
    {
        #region Fields / Properties
        #endregion

        #region Constructors
        #endregion

        #region ICharacterMatcher Members

        /// <summary>
        /// Calculates a matching score between a character and a radical sequence
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="radicalList">The radical list.</param>
        /// <returns>Matching score as a double value.</returns>
        public double Match(ICharacter character, List<IRadical> radicalList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
