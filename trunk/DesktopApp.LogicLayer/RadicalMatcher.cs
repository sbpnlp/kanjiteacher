using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// The Radicalmatcher class calculates a matching score between radical and a stroke 
    /// sequence.
    /// </summary>
    public class RadicalMatcher : IRadicalMatcher
    {
        #region Fields / Properties
        #endregion

        #region Constructors
        #endregion 

        #region IRadicalMatcher Members

        /// <summary>
        /// Calculates a matching score between a radical and a stroke sequence
        /// </summary>
        /// <param name="radical">The radical.</param>
        /// <param name="stroke">The stroke.</param>
        /// <returns>Matching score as a double value.</returns>
        public double Match(IRadical radical, List<IStroke> stroke)
        {
            double r = 0;
            if (radical is Radical) 
            {
                Radical theRadical = radical as Radical;

                foreach (Stroke s in theRadical.StrokeList)
                {
                }
                
            }
            throw new NotImplementedException();
//            return r;
        }

        #endregion
    }
}
