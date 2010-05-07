using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using System.Windows.Forms;

namespace Kanji.Kanjibrowser
{
    internal class MirrorEventListener : MouseEventListener
    {
        public MirrorEventListener(Control control) : base(control) { }

        internal void LoadPoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            List<MouseEventArgs> eventargsList = new List<MouseEventArgs>(xcoords.Count);
            for (int i = 0; i < xcoords.Count; i++)
            {
                eventargsList.Add(new MouseEventArgs(MouseButtons.Left, 0, xcoords[i], ycoords[i], 0));
            }

            AllActivePoints.Add(eventargsList);
        }
    }
}
