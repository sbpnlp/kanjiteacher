using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Kanji.InputArea.Controller;
using Kanji.DesktopApp;
using LL = Kanji.DesktopApp.LogicLayer;
using Drawing = System.Drawing;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.InputArea.WinFormGUI
{

    next thing to do: consume webserive just like InputArea.MobileGui
    added KanjiServiceReference - so maybe the file KanjiService.cs generated
    by svcutil.exe is not needed at all - let's see.

    internal class MouseEventListener : IControlled
    {
        #region Fields
        private InputController controller = new InputController();

        /// <summary>
        /// Event that occurs when [stroke finished].
        /// </summary>
        public event LL.StrokeEventHandler StrokeFinished;

        private Control Form { set; get; }
        //what's this for? don't find any reference:
        //private GraphicsPath CurrentPath { set; get; }
        private List<LL.Point> ActivePoints = new List<LL.Point>();
        private List<LL.Point> PassivePoints = new List<LL.Point>();
        private List<List<LL.Point>> AllActivePoints = new List<List<LL.Point>>();
        private List<List<LL.Point>> AllPassivePoints = new List<List<LL.Point>>();
        private List<LL.Stroke> AllStrokes = new List<LL.Stroke>();
        #endregion

        #region Constructors
        internal MouseEventListener(Control control)
        {
            Form = control;
            controller.View = this; //xxx todo: this is not correct. the view is desktopApp.Winformgui, not inputarea
            StrokeFinished = controller.ReceivePointList;
            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
        }
        #endregion

        #region Private methods
        protected virtual void OnStrokeFinished(LL.MouseInputEventArgs e)
        {
            if (StrokeFinished != null)
            {
                StrokeFinished(this, e);
            }
        }

        private LL.MouseInputEventArgs AssembleInputEvents()
        {
            return new LL.MouseInputEventArgs(ActivePoints, PassivePoints);
        }

        /// <summary>
        /// Archives the passive points into lists of lists.
        /// The method also archives the passive times.
        /// </summary>
        private void ArchivePassivePoints()
        {
            AllPassivePoints.Add(PassivePoints);
            PassivePoints = new List<LL.Point>();
        }

        /// <summary>
        /// Archives the active points into lists of lists.
        /// The method also archives the active times.
        /// </summary>
        private void ArchiveActivePoints()
        {
            AllActivePoints.Add(ActivePoints);
            ActivePoints = new List<LL.Point>();
        }

        /// <summary>
        /// Updates the drawing.
        /// </summary>
        /// <param name="colour">The colour.</param>
        private void UpdateDrawing(Drawing.Color colour)
        {
            DrawCross();
            using (Drawing.Graphics gfx = Form.CreateGraphics())
            {
                Drawing.Pen pen = new Drawing.Pen(colour, 3);
                int j = 0;

                foreach (List<LL.Point> lp in AllActivePoints)
                {
                    j = 0;
                    for (int i = 0; i < lp.Count; i++)
                    {
                        gfx.DrawLine(pen, lp[j].SysDrawPointF, lp[i].SysDrawPointF);
                        j = i;
                    }
                }

                j = 0;
                for (int i = 0; i < ActivePoints.Count; i++)
                {
                    gfx.DrawLine(pen, ActivePoints[j].SysDrawPointF, ActivePoints[i].SysDrawPointF);
                    j = i;
                }

                //foreach (LL.Stroke s in AllStrokes)
                //{
                //    j = 0;
                //    for (int i = 0; i < s.IntermediatePoints.Count; i++)
                //    {
                //        gfx.DrawLine(pen, s.IntermediatePoints[j].SysDrawPointF, s.IntermediatePoints[i].SysDrawPointF);
                //        j = i;
                //    }
                //}
            }
        }

        /// <summary>
        /// Draws the cross on the input area.
        /// </summary>
        /// <param name="colour">The colour of the cross.</param>
        /// <param name="pt">The point where the cross should be drawn.</param>
        private void DrawCross(Drawing.Color colour, Drawing.Point pt)
        {
            // Create new graphics object
            Drawing.Graphics gfx = Form.CreateGraphics();

            // Create a pen that has the same background color as the form
            Drawing.Pen pen = new Drawing.Pen(colour);

            // Draw a horizontal line that will erase the previous line created
            gfx.DrawLine(pen, new Drawing.Point(pt.X, 0), new Drawing.Point(pt.X, Form.ClientRectangle.Height));

            // Draw a vertical line that will erase the previous line created
            gfx.DrawLine(pen, new Drawing.Point(0, pt.Y), new Drawing.Point(Form.ClientRectangle.Width, pt.Y));
            gfx.DrawPath(pen, new GraphicsPath());

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
            OnStrokeFinished(AssembleInputEvents());

            //this deletes the current point lists
            ArchiveActivePoints();
            ArchivePassivePoints();
        }

        internal void MouseMove(MouseEventArgs e)
        {
            DrawCross();
            if (e.Button == MouseButtons.Left)
            {
                ActivePoints.Add(new LL.Point(e.X, e.Y, DateTime.Now));
            }
            else
            {
                PassivePoints.Add(new LL.Point(e.X, e.Y, DateTime.Now));
            }
            UpdateDrawing(Drawing.Color.Black);
        }

        private void DrawCross()
        {
            DrawCross(Drawing.Color.LightGray,
                new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
        }

        /// <summary>
        /// Resets the drawing to a blank screen.
        /// </summary>
        internal void ResetDrawing()
        {
            Drawing.Graphics gfx = Form.CreateGraphics();
            // gfx.FillRectangle(Drawing.Brushes.White, Form.ClientRectangle);
            //see if the following works equally well than the above:
            gfx.FillRectangle(new Drawing.SolidBrush(Drawing.Color.White), Form.ClientRectangle);

            gfx.Dispose();
            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
            AllActivePoints = new List<List<LL.Point>>();
            AllPassivePoints = new List<List<LL.Point>>();
            ActivePoints = new List<LL.Point>();
            PassivePoints = new List<LL.Point>();
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

        #region IControlled Members

        public void ReceiveCharacters(List<ICharacter> cModels)
        {
            foreach (ICharacter c in cModels)
            {
                ResetDrawing();

                foreach (LL.Radical r in ((LL.Character) c).RadicalList)
                {
                    foreach (LL.Stroke s in r.StrokeList)
                    {
                        AllActivePoints.Add(s.IntermediatePoints);
                        //foreach (Point p in s.IntermediatePoints)
                        //{
                        //    // draw this point as a vector
                        //}
                    }
                }
            }
            UpdateDrawing();
        }

        /// <summary>
        /// What is the use of this method?
        /// Why am I actually returning anything to the drawing area?
        /// No use. Just for seeing the strokes? No.
        /// They should come up in the DesktopApp.WinFormGUI,
        /// not here, in the InputArea...
        /// </summary>
        /// <param name="stroke"></param>
        /// <param name="bb"></param>
        public void ReceivePointList(IStroke stroke, IBoundingBox bb)
        {
            AllStrokes.Add((LL.Stroke)stroke);

            UpdateDrawing();
        }

        #endregion
    }
}
