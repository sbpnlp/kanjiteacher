using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Net;

namespace Kanji.StrokeMirrorer
{
    class PointListSaveObserver : IObserver
    {
        #region IObserver Members

        public void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            System.Windows.Forms.MessageBox.Show("punkte empfangen");
        }

        public void setIP(IPAddress ip)
        {
            System.Windows.Forms.MessageBox.Show("IP: " + ip.ToString());
        }
        #endregion
    }
}
