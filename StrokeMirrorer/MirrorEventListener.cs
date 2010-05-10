using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.InputArea.WinFormGUI;
using System.Windows.Forms;
using Kanji.DesktopApp.LogicLayer;

namespace Kanji.StrokeMirrorer
{
    internal class MirrorEventListener : MouseEventListener
    {
        public MirrorEventListener(Control control) : base(control) { }
        protected BoundingBox BBox = null;
        List<List<Point>> originalPointlist = new List<List<Point>>();

        internal void LoadPoints(List<int> xcoords, List<int> ycoords, List<DateTime> times)
        {
            List<MouseEventArgs> eventargsList = new List<MouseEventArgs>(xcoords.Count);
            List<Point> pointList = new List<Point>(xcoords.Count);


            for (int i = 0; i < xcoords.Count; i++)
            {
                //eventargsList.Add(new MouseEventArgs(MouseButtons.Left, 0, xcoords[i], ycoords[i], 0));
                pointList.Add(new Point(xcoords[i], ycoords[i]));
            }

            originalPointlist.Add(pointList);

            BBox = new BoundingBox(originalPointlist);

            double padding = 0.1;
            double stretchFactor = (double)(1-padding) * Form.ClientRectangle.Width / BBox.Width;
            BBox.Stretch(stretchFactor);
            foreach (Vector2 v in BBox.VectorsFromAnchor)
            {
                eventargsList.Add(
                    new MouseEventArgs(
                        MouseButtons.Left, 
                        0, 
                        (int)Math.Floor(v.X), 
                        (int)Math.Floor(v.Y), 
                        0));
            }

            AllActivePoints = new List<List<MouseEventArgs>>();
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
