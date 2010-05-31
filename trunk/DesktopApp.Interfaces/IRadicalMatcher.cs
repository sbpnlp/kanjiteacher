using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// The IRadicalmatcher interface defines the interface to the Radical matching
    /// class. The Radical matching class calculates a matching score between two strokes.
    /// </summary>
    public interface IRadicalMatcher
    {
        /// <summary>
        /// Calculates a matching score between a radical and a stroke sequence
        /// </summary>
        /// <param name="radical">The radical.</param>
        /// <param name="stroke">The stroke.</param>
        /// <returns>Matching score as a double value.</returns>
        double Match(IRadical radical, List<IStroke> stroke);
    }
}
