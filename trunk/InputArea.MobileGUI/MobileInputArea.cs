using System;
using System.Windows.Forms;

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
            mouseListener.UpdateDrawing();
        }

        private void MobileInputArea_MouseMove(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void MobileInputArea_MouseDown(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void MobileInputArea_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListener.MouseUp(e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mouseListener.ResetDrawing();
        }

    }
}
