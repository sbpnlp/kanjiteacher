using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using Kanji.DesktopApp.LogicLayer;
using Kanji.StrokeMirrorer;

namespace Kanji.Kanjibrowser
{
    class BrowserArea : AbstractWinFormInputArea
    {
        protected PointLoadObserver plso = null;

        public BrowserArea(PointLoadObserver observer)
            : base()
        {
            Text = "BrowserArea";
            plso = observer;

            mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            plso.MouseListener = mouseListener as MirrorEventListener;
        }

        protected override void InputArea_Load(object sender, EventArgs e)
        {
            //mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            //if (mouseListener is MirrorEventListener)
            //    plso.MouseListener = mouseListener as MirrorEventListener;
            //else throw new Exception("Get your types right");
        }
    }
}
