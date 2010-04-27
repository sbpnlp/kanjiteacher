using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public delegate void StrokeEventHandler(object sender, MouseInputEventArgs e);

    public class Stroke : IStroke
    {
        public Point BeginPoint { get { return AllPoints[0]; } }
        public Point EndPoint { get { return AllPoints[AllPoints.Count - 1]; } }
        public List<Point> IntermediatePoints { get; set; }
        public List<Point> AllPoints { get; set; }

        public Stroke() 
        {
            AllPoints = new List<Point>();
            IntermediatePoints = new List<Point>();
        }

        public Stroke(List<Point> allPoints)
        {
            AllPoints = allPoints;
            SearchIntermediate();
        }

        /// <summary>
        /// Searches intermediate significant points, judged from the angle
        /// of their vector to the general direction of the stroke
        /// </summary>
        private void SearchIntermediate()
        {
            Vector2 vStrokeDirection = new Vector2(BeginPoint, EndPoint);
            //if angle between points is really different,
            //create a new intermediate point.

            IntermediatePoints = new List<Point>();
            IntermediatePoints.Add(BeginPoint);

            if (AllPoints.Count > 5)
            {
                for (int i = 5; i < AllPoints.Count; i++)
                {
                    Vector2 v = new Vector2(AllPoints[i - 5], AllPoints[i]);
                    if (v.Angle(vStrokeDirection) > Math.PI / 4)
                    {
                        IntermediatePoints.Add(AllPoints[i]);
                    }
                }
            }
            IntermediatePoints.Add(EndPoint);
            //the code above does this now
            //var result =
            //    from point in allpoints
            //    where point.positionvector().angle(vstrokedirection) > math.pi / 4
            //    select point;

            //intermediatepoints = new list<point>(result);
        }

        #region IStroke Members

        public IPoint[] GetAllPoints()
        {
            return AllPoints.ToArray();
        }

        #endregion
    }
}
