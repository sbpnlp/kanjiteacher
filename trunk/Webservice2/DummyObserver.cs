using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.Webservice2
{
    class DummyObserver : IObserver
    {
        #region IObserver Members

        /// <summary>
        /// Reveives the points from the observed class.
        /// Whenever the observed class calls the observers ReceivePoints method,
        /// the observer is notified.
        /// </summary>
        /// <param name="xcoords">The xcoords.</param>
        /// <param name="ycoords">The ycoords.</param>
        /// <param name="times">The times.</param>
        public void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            Console.WriteLine("This is the Dummy Observer");
            Console.WriteLine(string.Format("Received a list of points at {0}", times[0].ToLongTimeString()));
        }

        #endregion
    }
}
