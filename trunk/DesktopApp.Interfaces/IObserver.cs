using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// Interface for the Observer in an Observer design scheme.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Reveives the points from the observed class.
        /// Whenever the observed class calls the observers ReceivePoints method,
        /// the observer is notified.
        /// </summary>
        /// <param name="xcoords">The xcoords.</param>
        /// <param name="ycoords">The ycoords.</param>
        /// <param name="times">The times.</param>
        void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times);

        void setIP(IPAddress ip);
    }
}
