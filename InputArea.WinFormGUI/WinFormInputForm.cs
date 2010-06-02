using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kanji.InputArea.WinFormGUI
{
    public partial class WinFormInputForm : Kanji.InputArea.WinFormGUI.AbstractWinFormInputArea
    {
        public WinFormInputForm() : base()
        {
            InitializeComponent();
        }

        private void InputArea_MouseMove(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void InputArea_MouseDown(object sender, MouseEventArgs e)
        {
            mouseListener.MouseMove(e);
        }

        private void InputArea_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListener.MouseUp(e);
        }
    }
}
