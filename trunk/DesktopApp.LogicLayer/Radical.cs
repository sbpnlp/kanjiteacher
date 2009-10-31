using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Represents a radical
    /// </summary>
    public class Radical : IRadical
    {
        public List<Stroke> StrokeList = new List<Stroke>();
        public List<List<Point>> ActivePoints;

        public Radical(List<List<Point>> activePoints)
        {
            ActivePoints = activePoints;

            for (int i = 0; i < ActivePoints.Count; i++)
            {
                Stroke s = new Stroke(ActivePoints[i]);
                StrokeList.Add(s);
            }
        }
    }
}
