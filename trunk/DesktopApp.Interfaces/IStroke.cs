using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IStroke
    {
        /// <summary>
        /// Gets all points.
        /// </summary>
        /// <returns>Returns a list of all points in the stroke.</returns>
        IPoint[] ToPointArray();

        /// <summary>
        /// Gets all points.
        /// </summary>
        /// <returns>Returns a list of all points in the stroke.</returns>
        List<IPoint> GetAllPoints();

        /// <summary>
        /// Calculates the matching score between two IStrokes
        /// </summary>
        /// <param name="stroke">The second stroke.</param>
        /// <param name="strokematcher">An instance of a strokematcher class.</param>
        /// <returns>double: matching value</returns>
        double MatchingScore(IStroke stroke, IStrokeMatcher strokematcher);
    }
}
