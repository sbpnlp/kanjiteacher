//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Drawing.Drawing2D;
//using System.Drawing;
//using Kanji.InputArea.MobileController;

//namespace Kanji.InputArea.MobileGUI
//{
//    //commented out because of definition in other file
//    //if needed, comment back in
//    public delegate void StrokeEventHandler(object sender, MouseInputEventArgs e);

//    internal class MouseEventListenerOldMobile
//    {
//        /// <summary>
//        /// Event that occurs when [stroke finished].
//        /// </summary>
//        public event StrokeEventHandler StrokeFinished;

//        private Control Form { set; get; }
//        //        private GraphicsPath CurrentPath { set; get; }
//        private List<Point> ActivePointList = new List<Point>();
//        private List<DateTime> ActiveTimeList = new List<DateTime>();
//        private List<Point> PassivePointList = new List<Point>();
//        private List<DateTime> PassiveTimeList = new List<DateTime>();
//        private List<List<Point>> AllActivePoints = new List<List<Point>>();
//        private List<List<DateTime>> AllActiveTimes = new List<List<DateTime>>();
//        private List<List<Point>> AllPassivePoints = new List<List<Point>>();
//        private List<List<DateTime>> AllPassiveTimes = new List<List<DateTime>>();
//        private Boolean PointListHasBeenSent { set; get; }


//        protected virtual void OnStrokeFinished(MouseInputEventArgs e)
//        {
//            if (StrokeFinished != null)
//            {
//                StrokeFinished(this, e);
//            }
//        }


//        internal MouseEventListenerOldMobile(Control control, int X, int Y)
//        {
//            Form = control;
//            ResetDrawing();
//        }

//        internal void MouseUp(MouseEventArgs e)
//        {
//            ArchiveActivePoints();

//            if (PointListHasBeenSent != true)
//            {
//                //send point list here
//                OnStrokeFinished(AssembleInputEvents());
//                PointListHasBeenSent = true;
//            }
//        }

//        private MouseInputEventArgs AssembleInputEvents()
//        {
//            return new MouseInputEventArgs(AllActivePoints, AllActiveTimes, AllPassivePoints, AllPassiveTimes);
//        }

//        internal void MouseMove(MouseEventArgs e)
//        {
//            DrawCross(Color.LightGray, new Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
//            if (e.Button == MouseButtons.Left)
//            {
//                UpdateActive(new Point(e.X, e.Y), DateTime.Now);
//                ArchivePassivePoints();
//                UpdateDrawing(Color.Black);
//            }
//            else
//            {
//                UpdatePassive(new Point(e.X, e.Y), DateTime.Now);
//                ArchiveActivePoints();
//                if (PointListHasBeenSent != true)
//                {
//                    //send point list here
//                    OnStrokeFinished(AssembleInputEvents());
//                }
//            }
//        }

//        private void ArchivePassivePoints()
//        {
//            AllPassivePoints.Add(PassivePointList);
//            PassivePointList = new List<Point>();
//            PointListHasBeenSent = false;
//        }

//        private void ArchiveActivePoints()
//        {
//            AllActivePoints.Add(ActivePointList);
//            ActivePointList = new List<Point>();
//        }

//        private void UpdateDrawing(Color colour)
//        {
//            using (Graphics gfx = Form.CreateGraphics())
//            {
//                Pen pen = new Pen(colour, 3);

//                //int j = 0;

//                gfx.DrawLines(pen, ActivePointList.ToArray());

//                //for (int i = 0; i < ActivePointList.Count; i++)
//                //{
//                //    gfx.DrawLine(pen, ActivePointList[j], ActivePointList[i]);
//                //    j = i;
//                //}
//            }
//        }

//        private void UpdateActive(Point point, DateTime dateTime)
//        {
//            ActivePointList.Add(point);
//            ActiveTimeList.Add(dateTime);
//        }

//        private void UpdatePassive(Point point, DateTime dateTime)
//        {
//            PassivePointList.Add(point);
//            PassiveTimeList.Add(dateTime);
//        }


//        private void DrawCross(Color colour, Point pt)
//        {
//            // Create new graphics object
//            Graphics gfx = Form.CreateGraphics();

//            // Create a pen that has the same background color as the form
//            Pen pen = new Pen(colour);

//            // Draw a horizontal line that will erase the previous line created
//            //            gfx.DrawLine(pen, new Point(pt.X, 0), new Point(pt.X, Form.ClientRectangle.Height));
//            gfx.DrawLine(pen, pt.X, 0, pt.X, Form.ClientRectangle.Height);
//            // Draw a vertical line that will erase the previous line created
//            //            gfx.DrawLine(pen, new Point(0, pt.Y), new Point(Form.ClientRectangle.Width, pt.Y));
//            gfx.DrawLine(pen, 0, pt.Y, Form.ClientRectangle.Width, pt.Y);
//            //gfx.DrawPath(pen, new GraphicsPath());

//        }

//        internal void ResetDrawing()
//        {
//            Graphics gfx = Form.CreateGraphics();
//            gfx.FillRectangle(new SolidBrush(Color.White), Form.ClientRectangle);
//            gfx.Dispose();
//            DrawCross(Color.LightGray, new Point(Form.ClientRectangle.Width / 2, Form.ClientRectangle.Height / 2));
//            AllActivePoints = new List<List<Point>>();
//            AllActiveTimes = new List<List<DateTime>>();
//            AllPassivePoints = new List<List<Point>>();
//            AllPassiveTimes = new List<List<DateTime>>();
//            ActivePointList = new List<Point>();
//            ActiveTimeList = new List<DateTime>();
//            PassivePointList = new List<Point>();
//            PassiveTimeList = new List<DateTime>();
//        }
//    }
//}
