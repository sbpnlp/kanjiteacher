using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Kanji.FirstGraphicsTest
{
    public partial class Form1 : Form
    {
        private Bitmap m_objDrawingSurface;
        private MouseEventListener mouseListener;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mouseListener = new MouseEventListener(this, 0, 0);

            //m_objDrawingSurface = new Bitmap(
            //    ClientRectangle.Width,
            //    ClientRectangle.Height,
            //    PixelFormat.Format24bppRgb);
            //InitializeSurface();
        }

        //private void InitializeSurface()
        //{
        //    Graphics objGraphics;
        //    Rectangle rectBounds;

        //    // Create a Graphics object that references the bitmap and clear it.
        //    objGraphics = Graphics.FromImage(m_objDrawingSurface);

        //    objGraphics.Clear(Color.White);

        //    //Create a rectangle the same size as the bitmap.
        //    rectBounds = new Rectangle(0, 0,
        //          m_objDrawingSurface.Width, m_objDrawingSurface.Height);
        //    //Reduce the rectangle slightly so the ellipse won't appear on the border.
        //    rectBounds.Inflate(-1, -1);

        //    // Draw an ellipse that fills the form.
        //    objGraphics.DrawEllipse(Pens.Orange, rectBounds);

        //    // Free up resources.
        //    objGraphics.Dispose();
        //}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //using (Graphics objGraphics = e.Graphics)
            //{
            //    // Draw the contents of the bitmap on the form.
            //    objGraphics.DrawImage(m_objDrawingSurface, 0, 0,
            //        m_objDrawingSurface.Width,
            //        m_objDrawingSurface.Height);
            //}
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //lblAbzisse.Text = String.Format("Die Abzisse ist bei {0} pixeln.",
            //    e.X.ToString());
            //lblYWert.Text = String.Format("Der Y-Wert beträgt {0} pixel.",
            //    e.Y.ToString());

            mouseListener.MouseMove(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseListener.MouseIsDown = true;
            mouseListener.MouseMove(e);
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            mouseListener.MouseIsDown = false;
            mouseListener.EndDrawing(e);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListener.MouseIsDown = false;
            mouseListener.EndDrawing(e);
        }
    }
}
