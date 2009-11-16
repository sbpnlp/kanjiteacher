using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// Interface for the Observer in an Observer design scheme.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Reveives the points.
        /// </summary>
        /// <param name="xcoords">The xcoords.</param>
        /// <param name="ycoords">The ycoords.</param>
        /// <param name="times">The times.</param>
        void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times);
        /// <summary>
        /// Provides information if the metadata for the webservice should be shown or not.
        /// </summary>
        /// <returns>True if the metadata should be shown, otherwise false.</returns>
        bool ShowMetaData();
    }
}
