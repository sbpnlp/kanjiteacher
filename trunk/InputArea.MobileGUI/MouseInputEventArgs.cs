using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Kanji.InputArea.MobileGUI
{
    public class MouseInputEventArgs : EventArgs
    {
        public List<List<Point>> ActivePoints { get; set; }
        public List<List<DateTime>> ActiveTimes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseInputEventArgs"/> class.
        /// </summary>
        /// <param name="activePoints">The active points.</param>
        /// <param name="activeTimes">The active times.</param>
        public MouseInputEventArgs(List<List<Point>> activePoints, List<List<DateTime>> activeTimes)
        {
            ActivePoints = activePoints;
            ActiveTimes = activeTimes;
        }
    }
}
