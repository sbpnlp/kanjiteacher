using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Kanji.DesktopApp;
using LL = Kanji.DesktopApp.LogicLayer;
using Drawing = System.Drawing;

namespace Kanji.InputArea.MobileGUI
{
    public delegate bool StrokeEventHandler(object sender, List<List<MouseEventArgs>> activePoints, List<List<DateTime>> activeTimes);
    public delegate bool OnlyActiveStrokeEventHandler(object sender, List<MouseEventArgs> activePoints, List<DateTime> activeTimes);

    internal class MouseEventListener : IMobileView
    {
        #region Fields

        private MobileClientCommunication controller = null;

        /// <summary>
        /// Event that occurs when [stroke finished].
        /// </summary>
        public event StrokeEventHandler StrokeFinished;
        public event OnlyActiveStrokeEventHandler OnlyActiveStrokeFinished;
        

        private Control Form { set; get; }
        private List<MouseEventArgs> ActivePoints = new List<MouseEventArgs>();
        private List<DateTime> ActiveTimes = new List<DateTime>();
        private List<List<MouseEventArgs>> AllActivePoints = new List<List<MouseEventArgs>>();
        private List<List<DateTime>> AllActiveTimes = new List<List<DateTime>>();
        private Drawing.Graphics gfx;
        #endregion

        #region Constructors
        internal MouseEventListener(Control control)
        {
            Form = control;
            gfx = Form.CreateGraphics();
            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
            controller = new MobileClientCommunication(this);
            controller.IP = Microsoft.VisualBasic.Interaction.InputBox("Please enter IP address of service", "IP address", "192.168.178.41", 50, 50);
            OnlyActiveStrokeFinished = controller.SendPointList;

        }
        #endregion

        #region Private methods
        protected virtual void OnStrokeFinished()
        {

            if (OnlyActiveStrokeFinished != null)
            {
                MessageBox.Show("sending the point list of active points");
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
                Drawing.Pen pen = new Drawing.Pen(colour, 8);
                int j = 0;

                foreach (List<MouseEventArgs> lp in AllActivePoints)
                {
                    j = 0;
                    for (int i = 0; i < lp.Count; i++)
                    {
                        DrawLine(pen, lp[j], lp[i]);
                        j = i;
                    }
                }

                j = 0;
                for (int i = 1; i < ActivePoints.Count; i++)
                {
                    DrawLine(pen, ActivePoints[j], ActivePoints[i]);
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

        private void DrawLine(Drawing.Pen pen, int X1, int Y1, int X2, int Y2)
        {
            gfx.DrawLine(pen, X1, Y1, X2, Y2);
        }

        private void DrawLine(Drawing.Pen pen, MouseEventArgs p1, MouseEventArgs p2)
        {
            gfx.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
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
            UpdateDrawing(Drawing.Color.Black);
        }

        internal void MouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ActiveTimes.Add(DateTime.Now);
                ActivePoints.Add(e);
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
            gfx.FillRectangle(new Drawing.SolidBrush(Drawing.Color.White), Form.ClientRectangle);
            DrawCross(Drawing.Color.LightGray, new Drawing.Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
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
    }
}
