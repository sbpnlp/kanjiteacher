using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Kanji.InputArea.Controller
{
    public class MouseInputEventArgs : EventArgs
    {
        private List<List<Point>> ActivePoints { get; set; }
        private List<List<DateTime>> ActiveTimes { get; set; }
        private List<List<Point>> PassivePoints { get; set; }
        private List<List<DateTime>> PassiveTimes { get; set; }

        public MouseInputEventArgs(List<List<Point>> activePoints, List<List<DateTime>> activeTimes,
            List<List<Point>> passivePoints, List<List<DateTime>> passiveTimes)
        {
            ActivePoints = activePoints;
            ActiveTimes = activeTimes;
            PassivePoints = passivePoints;
            PassiveTimes = passiveTimes;
        }
    }
}
