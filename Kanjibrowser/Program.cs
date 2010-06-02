using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kanji.InputArea.WinFormGUI;
using KSvc = Kanji.KanjiService;
using System.Threading;
using Kanji.DesktopApp.Interfaces;
using Kanji.DesktopApp.LogicLayer;
using SM = Kanji.StrokeMirrorer;

namespace Kanji.Kanjibrowser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            KSvc.Service serv = new KSvc.Service();

            //don't show metadata
            serv.ShowMetaData = false;

            //starting service
            SM.PointLoadObserver plso = new SM.PointLoadObserver();
            ThreadStart tStart = delegate { serv.Run(plso); };
            Thread t = new Thread(tStart);
            t.Start();

            //starting mirror GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SM.MirrorArea ma = new SM.MirrorArea(plso);
            ma.ShowDialog();// == DialogResult.Cancel)
            ma.DialogResult = DialogResult.OK;
            ma.Hide();
            ma.Close();
            ma.Dispose();
            t.Abort();

//            Application.Run(new MirrorArea(plso));
        }
    }
}
