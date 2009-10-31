//Downloaded from
//Visual C# Kicks - http://www.vcskicks.com/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DragForm
{
    public partial class frmDrag : Form
    {
        public frmDrag()
        {
            InitializeComponent();
        }

        private void frmDrag_Paint(object sender, PaintEventArgs e)
        {
            //Draws a border to make the Form stand out
            //Just done for appearance, not necessary

            //Pen p = new Pen(Color.Gray, 3);
            //e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            //p.Dispose();
        }

        Point lastClick; //Holds where the Form was clicked

        private void frmDrag_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = new Point(e.X, e.Y); //We'll need this for when the Form starts to move
        }

        private void frmDrag_MouseMove(object sender, MouseEventArgs e)
        {
            //Point newLocation = new Point(e.X - lastE.X, e.Y - lastE.Y);
            if (e.Button == MouseButtons.Left) //Only when mouse is clicked
            {
                //Move the Form the same difference the mouse cursor moved;
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        //If the user clicks on the label we want the same kind of behavior
        //so we just call the Forms corresponding methods
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            frmDrag_MouseDown(sender, e);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            frmDrag_MouseMove(sender, e);
        }

        private void frmDrag_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}