using System;
using System.Windows.Forms;

namespace Kanji.InputArea.WinFormGUI
{
    public partial class AbstractWinFormInputArea : Form
    {
        protected MouseEventListener mouseListener;

        public AbstractWinFormInputArea()
        {
            InitializeComponent();
            mouseListener = new MouseEventListener(this.pnlDrawingArea);
        }

        protected virtual void InputArea_Load(object sender, EventArgs e){}

        private void InputArea_Paint(object sender, PaintEventArgs e)
        {
            mouseListener.UpdateDrawing();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            mouseListener.ResetDrawing();
        }
    }
}
