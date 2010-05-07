﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using System.Windows.Forms;

namespace Kanji.StrokeMirrorer
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

        /// <summary>
        /// Catches the mouse up event, stores the points and raises the OnStrokeFinished event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        internal new void MouseUp(MouseEventArgs e) {/* don't do anything */}

        /// <summary>
        /// Catches the MouseMove event
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        internal new void MouseMove(MouseEventArgs e) {/* don't do anything */}

    }
}
