using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// A stroke matcher that uses dynamic time warping in
    /// order to calculate a matching score between two strokes.
    /// </summary>
    public class TWStrokeMatcher : IStrokeMatcher
    {
        #region IStrokeMatcher Members

        /// <summary>
        /// Calculates a matching score between stroke1 and stroke2.
        /// </summary>
        /// <param name="stroke1">The first stroke.</param>
        /// <param name="stroke2">The second stroke.</param>
        /// <returns>Matching score as a double value.</returns>
        public double Match(IStroke stroke1, IStroke stroke2)
        {
            double r = double.MaxValue;
            if ((stroke1 is Stroke) && (stroke2 is Stroke))
            {
                TimeWarping tw = new TimeWarping(stroke1 as Stroke, stroke2 as Stroke);
                tw.CalculateDistances((p1, p2) => p1.Distance(p2));
                tw.CalculateCumulativeDistance();
                r = tw.WarpingDistance;
            }
            return r;
        }

        #endregion
    }
}
