﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Kanji.DesktopApp.WinFormGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DesktopAppMainForm());
            //Application.Run(new ViewModeForm());
            //Application.Run(new FollowModeForm());
            //Application.Run(new ExerciseMode1Form());
        }
    }
}
