using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using KSvc = Kanji.KanjiService;
using System.Threading;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.Controller
{
    /// <summary>
    /// Controls the desktop application. Controller of a MVC scheme
    /// </summary>
    public class Controller : IObserver
    {
        /// <summary>
        /// Provides information whether web service meta data should be shown or not.
        /// </summary>
        bool showmetadata = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
        {
            Thread t = new Thread(StartWebservice);
            t.Start();
        }

        /// <summary>
        /// Starts the webservice for reception of point coordinates.
        /// The controller is handed over to the webservice as an observer/visitor
        /// </summary>
        private void StartWebservice()
        {
            KSvc.Service serv = new KSvc.Service();


            showmetadata = true;
            //showmetadata = false;
            serv.ShowMetaData = showmetadata;

            serv.Run((object)this);
        }

        #region IObserver Members

        public void ReveivePoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            throw new Exception("received points in controller!");
        }

        #endregion

        #region IObserver Members


        public bool ShowMetaData()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
