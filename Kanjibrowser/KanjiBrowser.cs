using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using Kanji.DesktopApp.LogicLayer;

namespace Kanji.Kanjibrowser
{
    class MirrorArea : WinFormInputArea
    {
        PointListSaveObserver plso = null;

        public MirrorArea(PointListSaveObserver observer) : base() 
        {
            Text = "MirrorArea";
            plso = observer;
        }

        protected override void InputArea_Load(object sender, EventArgs e)
        {
            mouseListener = new MirrorEventListener(this);
            if (mouseListener is MirrorEventListener)
                plso.MouseListener = mouseListener as MirrorEventListener;
            else throw new Exception("Get your types right");
            
            BoundingBox bb = new BoundingBox(mouseListener
        }
    }
}
