using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kanji.InputArea.WinFormGUI;
using KSvc = Kanji.KanjiService;
using System.Threading;

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
            PointLoadObserver plso = new PointLoadObserver();
            ThreadStart tStart = delegate { serv.Run(plso); };
            Thread t = new Thread(tStart);
            t.Start();

            //starting mirror GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MirrorArea ma = new MirrorArea(plso);
            //if (
                ma.ShowDialog();// == DialogResult.Cancel)
            //{
                ma.DialogResult = DialogResult.OK;
                ma.Hide();
                ma.Close();
                ma.Dispose();
            //}
            //else
            //{
            //    MessageBox.Show("Test");
            //}
//            Application.Run(new MirrorArea(plso));
        }
    }
}
