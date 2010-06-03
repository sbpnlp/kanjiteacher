using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Net;
using Kanji.InputArea.WinFormGUI;
using Kanji.DesktopApp.LogicLayer;

namespace Kanji.StrokeMirrorer
{
    public class PointLoadObserver : IObserver
    {
        public MirrorEventListener MouseListener { get { return mouselistener; } set { mouselistener = value; } }
        MirrorEventListener mouselistener = null;

        /// <summary>
        /// Reveives the points.
        /// </summary>
        /// <param name="pointList">The point list.</param>
        public void ReveivePoints(List<Point> pointList)
        {
            mouselistener.LoadPoints(pointList);
            mouselistener.UpdateDrawing();
        }

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
            mouselistener.LoadPoints(xcoords, ycoords, times);
            mouselistener.UpdateDrawing();
        }

        /// <summary>
        /// Sets the IP.
        /// </summary>
        /// <param name="ip">The ip.</param>
        public void setIP(IPAddress ip)
        {
            //System.Windows.Forms.MessageBox.Show("IP: " + ip.ToString());
        }

        /// <summary>
        /// Signals that a Reset has been requested.
        /// </summary>
        public void ResetSignal()
        {
            mouselistener.ResetDrawing();
        }
        #endregion
    }
}
