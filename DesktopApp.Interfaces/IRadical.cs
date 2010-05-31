using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IRadical
    {
        /// <summary>
        /// Calculates the matching score between two a sequence of IStrokes
        /// and a Radical
        /// </summary>
        /// <param name="strokeSequence">The stroke sequence.</param>
        /// <param name="radicalmatcher">An instance of a radicalmatcher class.</param>
        /// <returns>double: matching value</returns>
        double MatchingScore(List<IStroke> strokeSequence, IRadicalMatcher radicalmatcher);

        /// <summary>
        /// Creates an Hash of the IRadical point sequence.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute 
        /// the hash including the time information of the points.</param>
        /// <returns>A byte array with the hash.</returns>
        byte[] Hash(bool withTime);  
    }
}
