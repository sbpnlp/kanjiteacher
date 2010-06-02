using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Net;
using Kanji.InputArea.WinFormGUI;

namespace Kanji.StrokeMirrorer
{
    public class PointLoadObserver : IObserver
    {
        public MirrorEventListener MouseListener { get { return mouselistener; } set { mouselistener = value; } }
        MirrorEventListener mouselistener = null;

        #region IObserver Members

        public void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            mouselistener.LoadPoints(xcoords, ycoords, times);
            mouselistener.UpdateDrawing();
        }

        public void setIP(IPAddress ip)
        {
            System.Windows.Forms.MessageBox.Show("IP: " + ip.ToString());
        }
        #endregion
    }
}
