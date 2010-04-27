using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Xml;

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

        public XmlNode ToXmlNode(XmlDocument doc, XmlElement attachTo)
        {
            XmlElement elem = doc.CreateElement("timestamp");
            elem.SetAttribute("xml:id", "testID" + BeginPoint.Time.Ticks.ToString());
            attachTo.AppendChild(elem);

            elem = doc.CreateElement("trace");
            elem.SetAttribute("id", "testID" + BeginPoint.Time.Ticks.ToString() + "trace");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < AllPoints.Count; i++)
            {
                string traceString = AllPoints[i].ToTraceString(BeginPoint.Time.Ticks);
                sb.Append(traceString + ",\r\n");
            }
            elem.InnerText = sb.ToString().TrimEnd().TrimEnd(',');

            attachTo.AppendChild(elem);
            
            return attachTo;
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
