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

        /// <summary>
        /// Creates an Md5hash of the IStroke point sequence.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute 
        /// the hash including the time information of the points.</param>
        /// <returns>A byte array with the hash.</returns>
        byte[] MD5hash(bool withTime);  
    }
}
