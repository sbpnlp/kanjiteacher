using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kanji.DesktopApp.WinFormGUI
{
    public partial class ExerciseMode1Form : Form
    {

        public ExerciseMode1Form()
        {
            InitializeComponent();
            DesktopApp.Controller.Controller controller = new Controller.Controller();
        }
    }
}
