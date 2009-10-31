using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public class MouseInputEventArgs : EventArgs, IMouseInputEventArgs
    {
        public List<Point> ActivePoints { get; set; }
        public List<Point> PassivePoints { get; set; }

        public MouseInputEventArgs(List<Point> activePoints, 
            List<Point> passivePoints)
        {
            ActivePoints = activePoints;
            PassivePoints = passivePoints;
        }
    }
}
