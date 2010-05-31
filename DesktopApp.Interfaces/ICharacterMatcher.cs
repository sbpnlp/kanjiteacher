using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// The ICharacterMatcher interface defines the interface to the Character matching
    /// class. The Character matching class calculates a matching score between two strokes.
    /// </summary>
    public interface ICharacterMatcher
    {
        /// <summary>
        /// Calculates a matching score between a character and a radical sequence
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="radicalList">The radical list.</param>
        /// <returns>Matching score as a double value.</returns>
        double Match(ICharacter character, List<IRadical> radicalList);
    }
}
