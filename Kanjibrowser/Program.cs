using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kanji.InputArea.WinFormGUI;
using KSvc = Kanji.KanjiService;
using System.Threading;
using Kanji.DesktopApp.Interfaces;
using Kanji.DesktopApp.LogicLayer;
using Kanji.StrokeMirrorer;
using System.IO;

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

            //get characters
            List<Character> characterdatabase =            
                UPXReader.ParseUPXFile(
                    File.Open("C:\\Diplom\\kanjiteacher\\data\\exampleFormat.upx", FileMode.Open));


            //initialise viewing area
            MirrorArea ma = new MirrorArea(plso);
            
            //go through all the strokes in the database
            //fill viewing area
            foreach (Character c in characterdatabase)
            {
                foreach (Stroke dbStroke in c.StrokeList)
                {
                    plso.ReveivePoints(dbStroke.AllPoints);
                }
            }

            //show viewing area
            ma.ShowDialog();
            ma.Hide();
            ma.Close();
            ma.Dispose();
            t.Abort();

//            Application.Run(new MirrorArea(plso));
        }
    }
}
