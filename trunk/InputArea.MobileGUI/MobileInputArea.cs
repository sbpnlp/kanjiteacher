using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Kanji.InputArea.MobileGUI
{
    public partial class MobileInputArea : Form
    {
        private MouseEventListener mouseListener;

        public MobileInputArea()
        {
            InitializeComponent();
        }

        private void MobileInputArea_Load(object sender, EventArgs e)
        {
            mouseListener = new MouseEventListener(this);
        }

        private void MobileInputArea_Paint(object sender, PaintEventArgs e)
        {
            mouseListener.ResetDrawing();
        }

        private void MobileInputArea_MouseMove(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mouseListener.ResetDrawing();
        }

        private void MobileInputArea_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListener.MouseUp(e);
        }
    }
}
