using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using KSvc = Kanji.KanjiService;
using System.Threading;

namespace Kanji.StrokeMirrorer
{
    public class MirrorArea : AbstractWinFormInputArea
    {
        protected PointLoadObserver PointLoader = null;
        private Thread ServiceThread = null;

        public MirrorArea() : base() 
        {
            Text = "MirrorArea";
            PointLoader = new PointLoadObserver();

            mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            PointLoader.MouseListener = mouseListener as MirrorEventListener;

            KSvc.Service serv = new KSvc.Service();

            //don't show metadata
            serv.ShowMetaData = false;

            ThreadStart tStart = delegate { serv.Run(PointLoader); };
            ServiceThread = new Thread(tStart);
            ServiceThread.Start();
        }
        public MirrorArea(PointLoadObserver observer) : base() 
        {
            Text = "MirrorArea";
            PointLoader = observer;

            mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            PointLoader.MouseListener = mouseListener as MirrorEventListener;
        }

        protected override void InputArea_Load(object sender, EventArgs e)
        {
            //mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            //if (mouseListener is MirrorEventListener)
            //    PointLoader.MouseListener = mouseListener as MirrorEventListener;
            //else throw new Exception("Get your types right");
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(ServiceThread != null)
                ServiceThread.Abort();
            base.Dispose(disposing);
        }
    }
}
