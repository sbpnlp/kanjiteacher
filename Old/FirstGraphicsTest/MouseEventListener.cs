using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Kanji.FirstGraphicsTest
{
    internal class MouseEventListener
    {
        int XMouse { set; get; }
        int YMouse { set; get; }
        Control Form { set; get; }
        internal Boolean MouseIsDown { set; get; }

        internal MouseEventListener(Control control, int X, int Y)
        {
            //initial mouse coordinates
            XMouse = X;
            YMouse = Y;
            Form = control;
        }

        internal void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (MouseIsDown)
            {
                DrawCross(Form.BackColor, XMouse, YMouse);
                DrawCross(Color.Black, e.X, e.Y);
            }

            // Create new graphics object
            Graphics gfx = Form.CreateGraphics();
            // Create a pen that has the same background color as the form
            Pen erasePen = new Pen(Color.White);
            // Draw a line that will erase the previous line created
            gfx.DrawLine(erasePen, 0, 0, XMouse, YMouse);
            // Create a pen that will draw the blue line
            Pen linePen = new Pen(Color.Blue);
            // Store the X coordinate
            XMouse = e.X;
            // Store the Y coordinate
            YMouse = e.Y;
            // Actually draw the blue line
            gfx.DrawLine(linePen, 0, 0, XMouse, YMouse);
        }

        private void DrawCross(Color colour, int X, int Y)
        {
            // Create new graphics object
            Graphics gfx = Form.CreateGraphics();
            
            // Create a pen that has the same background color as the form
            Pen pen = new Pen(colour);

            // Draw a horizontal line that will erase the previous line created
            gfx.DrawLine(pen, X, 0, X, Form.ClientRectangle.Height);

            // Draw a vertical line that will erase the previous line created
            gfx.DrawLine(pen, 0, Y, Form.ClientRectangle.Width, Y);

        }


        private void DrawSomeLines(int X, int Y)
        {
            // Create new graphics object
            Graphics gfx = Form.CreateGraphics();

            //  Form.ClientRectangle.Width;
            //  Form.ClientRectangle.Height;


            // Create a pen that has the same background color as the form
            Pen erasePen = new Pen(Form.BackColor);

            // Draw a horizontal line that will erase the previous line created
            gfx.DrawLine(erasePen, YMouse, 0, Form.ClientRectangle.Width, YMouse);

            // Draw a vertical line that will erase the previous line created
            gfx.DrawLine(erasePen, XMouse, 0, 0, Form.ClientRectangle.Height);

            // Create a pen that will draw the blue line
            Pen linePen = new Pen(Color.Black);
            // Store the X coordinate
            XMouse = X;
            // Store the Y coordinate
            YMouse = Y;

            // Draw a horizontal line that will erase the previous line created
            gfx.DrawLine(linePen, YMouse, 0, Form.ClientRectangle.Width, YMouse);

            // Draw a vertical line that will erase the previous line created
            gfx.DrawLine(linePen, XMouse, 0, 0, Form.ClientRectangle.Height);

        }

        private void RemoveLastCross()
        {
            DrawCross(Form.BackColor, XMouse, YMouse);
        }

        internal void EndDrawing(EventArgs e)
        {
            RemoveLastCross();
        }
    }
}
