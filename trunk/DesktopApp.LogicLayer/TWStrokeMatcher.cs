using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public class TWStrokeMatcher : IStrokeMatcher
    {
        #region IStrokeMatcher Members

        public double Match(IStroke stroke1, IStroke stroke2)
        {
            double r = 0;
            if ((stroke1 is Stroke) && (stroke2 is Stroke))
            {
                TimeWarping tw = new TimeWarping(stroke1 as Stroke, stroke2 as Stroke);

            }
            return r;
        }

        #endregion
    }
}
