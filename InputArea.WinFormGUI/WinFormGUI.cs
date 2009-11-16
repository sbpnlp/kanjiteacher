using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kanji.InputArea.WinFormGUI
{
    public partial class WinFormGUI : Form
    {
        private MouseEventListener mouseListener;

        public WinFormGUI()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mouseListener = new MouseEventListener(this);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mouseListener.UpdateDrawing();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {            
            mouseListener.MouseMove(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mouseListener.ResetDrawing();
        }

        private void WinFormGUI_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListener.MouseUp(e);
        }
    }
}
