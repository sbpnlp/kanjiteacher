using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using Kanji.DesktopApp.LogicLayer;

namespace Kanji.Kanjibrowser
{
    class MirrorArea : AbstractWinFormInputArea
    {
        protected PointLoadObserver plso = null;

        public MirrorArea(PointLoadObserver observer)
            : base()
        {
            Text = "MirrorArea";
            plso = observer;
        }

        protected override void InputArea_Load(object sender, EventArgs e)
        {
            mouseListener = new MirrorEventListener(this.pnlDrawingArea);
            if (mouseListener is MirrorEventListener)
                plso.MouseListener = mouseListener as MirrorEventListener;
            else throw new Exception("Get your types right");
        }
    }
}
