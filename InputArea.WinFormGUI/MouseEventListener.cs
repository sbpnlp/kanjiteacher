using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Kanji.DesktopApp;
using LL = Kanji.DesktopApp.LogicLayer;
using Drawing = System.Drawing;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.InputArea.WinFormGUI
{
    public delegate bool StrokeEventHandler(object sender, List<List<MouseEventArgs>> activePoints, List<List<DateTime>> activeTimes);
    public delegate bool OnlyActiveStrokeEventHandler(object sender, List<MouseEventArgs> activePoints, List<DateTime> activeTimes);

    internal class MouseEventListener : IControlled
    {
        #region Fields
        private WinClientCommunication controller = null;

        /// <summary>
        /// Event that occurs when [stroke finished].
        /// </summary>
        public event OnlyActiveStrokeEventHandler OnlyActiveStrokeFinished;

        private Control Form { set; get; }
        private List<MouseEventArgs> ActivePoints = new List<MouseEventArgs>();
        private List<DateTime> ActiveTimes = new List<DateTime>();
        private List<List<MouseEventArgs>> AllActivePoints = new List<List<MouseEventArgs>>();
        private List<List<DateTime>> AllActiveTimes = new List<List<DateTime>>();
        private Drawing.Graphics gfx = null;
        #endregion

        #region Constructors
        internal MouseEventListener(Control control)
        {
            Form = control;
            gfx = Form.CreateGraphics();
            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
            controller = new WinClientCommunication(this, "127.0.0.1"); //xxx todo: this is not correct. the view is desktopApp.Winformgui, not inputarea
            OnlyActiveStrokeFinished = controller.SendPointList;
        }
        #endregion

        #region Private methods
        protected virtual void OnStrokeFinished()
        {
            if (OnlyActiveStrokeFinished != null)
            {
                //send each point list only once for now...
                //maybe exchange with 
                //sending allactivepoints (after calling ArchiveActivePoints !)
                OnlyActiveStrokeFinished(this, ActivePoints, ActiveTimes);
            }

            //this deletes the current point lists
            ArchiveActivePoints();
        }

        /// <summary>
        /// Archives the active points into lists of lists.
        /// The method also archives the active times.
        /// </summary>
        private void ArchiveActivePoints()
        {
            AllActivePoints.Add(ActivePoints);
            AllActiveTimes.Add(ActiveTimes);
            ActivePoints = new List<MouseEventArgs>();
            ActiveTimes = new List<DateTime>();
        }

        /// <summary>
        /// Updates the drawing.
        /// </summary>
        /// <param name="colour">The colour.</param>
        private void UpdateDrawing(Drawing.Color colour)
        {
            DrawCross();

            foreach (List<MouseEventArgs> lp in AllActivePoints)
            {
                DrawPointList(colour, lp);
            }

            DrawPointList(colour, ActivePoints);
        }

        /// <summary>
        /// Draws the point list.
        /// </summary>
        /// <param name="colour">The colour.</param>
        /// <param name="ptList">The point list.</param>
        private void DrawPointList(Drawing.Color colour, List<MouseEventArgs> ptList)
        {
            Drawing.Pen pen = new Drawing.Pen(colour, 8);
            int j = 0;

            for (int i = 1; i < ptList.Count; i++)
            {
                DrawLine(pen, ptList[j], ptList[i]);
                j = i;
            }
        }

        /// <summary>
        /// Draws the cross on the input area.
        /// </summary>
        /// <param name="colour">The colour of the cross.</param>
        /// <param name="pt">The point where the cross should be drawn.</param>
        private void DrawCross(Drawing.Color colour, Drawing.Point pt)
        {
            // Create a pen that has the same background color as the form
            Drawing.Pen pen = new Drawing.Pen(colour);

            // Draw a horizontal line that will erase the previous line created
            DrawLine(pen, pt.X, 0, pt.X, Form.ClientRectangle.Height);

            // Draw a vertical line that will erase the previous line created
            DrawLine(pen, 0, pt.Y, Form.ClientRectangle.Width, pt.Y);

        }

        /// <summary>
        /// Draws a line connecting the two points specified by the coordinate pairs.
        /// </summary>
        /// <param name="pen"><see cref="System.Drawing.Pen"/> that determines the color, width, and style of the line.</param>
        /// <param name="X1">The x-coordinate of the first point.</param>
        /// <param name="Y1">The y-coordinate of the first point.</param>
        /// <param name="X2">The x-coordinate of the second point.</param>
        /// <param name="Y2">The y-coordinate of the second point.</param>
        /// <exception cref="System.ArgumentNullException">Pen is null.</exception>
        private void DrawLine(Drawing.Pen pen, int X1, int Y1, int X2, int Y2)
        {
            gfx.DrawLine(pen, X1, Y1, X2, Y2);
        }

        /// <summary>
        /// Draws a line connecting the two points specified by <see cref="System.Windows.Forms.MouseEventArgs"/>.
        /// </summary>
        /// <param name="pen"><see cref="System.Drawing.Pen"/> that determines the color, width, and style of the line.</param>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <exception cref="System.ArgumentNullException">Pen is null.</exception>
        private void DrawLine(Drawing.Pen pen, MouseEventArgs p1, MouseEventArgs p2)
        {
            gfx.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
        }

        private void DrawCross()
        {
            DrawCross(Drawing.Color.LightGray,
                new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Catches the mouse up event, stores the points and raises the OnStrokeFinished event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        internal void MouseUp(MouseEventArgs e)
        {
            //send point list here
            OnStrokeFinished();
            UpdateDrawing();
        }

        /// <summary>
        /// Catches the MouseMove event
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        internal void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ActiveTimes.Add(DateTime.Now);
                ActivePoints.Add(e);
                DrawPointList(Drawing.Color.Black, ActivePoints);
            }
        }

        /// <summary>
        /// Resets the drawing to a blank screen.
        /// </summary>
        internal void ResetDrawing()
        {
            gfx.FillRectangle(new Drawing.SolidBrush(Drawing.Color.White), Form.ClientRectangle);
            DrawCross();
//            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
            AllActivePoints = new List<List<MouseEventArgs>>();
            AllActiveTimes = new List<List<DateTime>>();
            ActiveTimes = new List<DateTime>();
            ActivePoints = new List<MouseEventArgs>();
        }

        /// <summary>
        /// Updates the drawing. This is the method that a UI 
        /// should call for getting updated.
        /// </summary>
        internal void UpdateDrawing()
        {
            UpdateDrawing(Drawing.Color.Black);
        }
        #endregion

        //#region IControlled Members

        //public void ReceiveCharacters(List<ICharacter> cModels)
        //{
        //    foreach (ICharacter c in cModels)
        //    {
        //        ResetDrawing();

        //        foreach (LL.Radical r in ((LL.Character) c).RadicalList)
        //        {
        //            foreach (LL.Stroke s in r.StrokeList)
        //            {
        //                AllActivePoints.Add(s.IntermediatePoints);
        //                //foreach (Point p in s.IntermediatePoints)
        //                //{
        //                //    // draw this point as a vector
        //                //}
        //            }
        //        }
        //    }
        //    UpdateDrawing();
        //}

        ///// <summary>
        ///// What is the use of this method?
        ///// Why am I actually returning anything to the drawing area?
        ///// No use. Just for seeing the strokes? No.
        ///// They should come up in the DesktopApp.InputArea,
        ///// not here, in the InputArea...
        ///// </summary>
        ///// <param name="stroke"></param>
        ///// <param name="bb"></param>
        //public void ReceivePointList(IStroke stroke, IBoundingBox bb)
        //{
        //    AllStrokes.Add((LL.Stroke)stroke);

        //    UpdateDrawing();
        //}

        //#endregion
    }
}
