using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.IO;

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
            Console.WriteLine("This is the Dummy Observer.");
//            Console.WriteLine(string.Format("Received a list of points at {0}", times[times.Count-1].ToLongTimeString()));
            using (StreamWriter sw = new StreamWriter("out.txt", true))
            {
                //xxx HACK the last element of ActiveTimes is not a time but the stroke number
                sw.WriteLine(string.Format("Received a list of points [{2}] at {0}.\n\tLatency since drawing of last point (mouseup event): {1}",
                    DateTime.Now.ToLongTimeString(),
                    new DateTime(DateTime.Now.Ticks - times[times.Count - 2].Ticks).ToLongTimeString(),
                    times[times.Count - 1].Ticks));

                sw.Write(string.Format("\tStroke[{0}]: ", times[times.Count - 1].Ticks));
                for (int i = 0; i < xcoords.Count; i++)
                {
                    sw.Write(string.Format("<{0},{1}> ", xcoords[i], ycoords[i]));
                }
                sw.WriteLine();


                Console.WriteLine(string.Format("Received a list of points [{2}] at {0}.\n\tLatency since drawing of last point (mouseup event): {1}",
                    DateTime.Now.ToLongTimeString(),
                    new DateTime(DateTime.Now.Ticks - times[times.Count - 2].Ticks).ToLongTimeString(),
                    times[times.Count - 1].Ticks));
            }
        }

        #endregion
    }
}
