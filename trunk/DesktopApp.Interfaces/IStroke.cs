using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IStroke
    {
        /// <summary>
        /// Gets all points.
        /// </summary>
        /// <returns>Returns a list of all points in the stroke.</returns>
        IPoint[] GetAllPoints();
    }
}
