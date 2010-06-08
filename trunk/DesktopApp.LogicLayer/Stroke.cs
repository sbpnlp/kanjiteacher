using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;
using System.Xml;
using System.Security.Cryptography;

namespace Kanji.DesktopApp.LogicLayer
{
    public delegate void StrokeEventHandler(object sender, MouseInputEventArgs e);

    public class Stroke : IStroke
    {
        #region Fields
        public Point BeginPoint { get { return AllPoints[0]; } }
        public Point EndPoint { get { return AllPoints[AllPoints.Count - 1]; } }
        public List<Point> IntermediatePoints { get; set; }
        public List<Point> AllPoints { get; set; }
        /// <summary>
        /// Gets or sets the ID. The ID is a human readable and
        /// human-given name for a stroke. If the stroke has just been
        /// input by a user the ID may be empty or contain only a stroke number.
        /// </summary>
        /// <value>The ID.</value>
        public string ID { get; set; }
        public string Value { get; set; }
        /// <summary>
        /// A hash value that represents the stroke with the time information
        /// </summary>
        byte[] _hashWithTime = null;
        /// <summary>
        /// A hash value that represents the stroke without the time information
        /// </summary>
        byte[] _hash = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Stroke"/> class.
        /// </summary>
        public Stroke() 
        {
            AllPoints = new List<Point>();
            IntermediatePoints = new List<Point>();
            ID = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stroke"/> class.
        /// </summary>
        /// <param name="allPoints">All points.</param>
        public Stroke(List<Point> allPoints)
        {
            AllPoints = allPoints;
            SearchIntermediate();
            ID = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stroke"/> class.
        /// </summary>
        /// <param name="trace">The an InkML trace that contains the point coordinates for this stroke.</param>
        public Stroke(string trace)
        {
            AllPoints = PointListFromInkMLTrace(trace);
            SearchIntermediate();
            ID = string.Empty;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Creates an XML trace node from the current stroke. Attaches a reference time stamp 
        /// and a trace format element as well.
        /// </summary>
        /// <param name="doc">The xml document.</param>
        /// <param name="attachTo">The xml element to which the trace should be attached to.</param>
        public void ToXmlNode(XmlDocument doc, XmlElement attachTo)
        {
            string localTimestampID = CreateTimeStampElement(doc, attachTo);
            CreateTraceFormatElement(doc, attachTo, localTimestampID);
            ToXmlNode(doc, attachTo, BeginPoint.Time);
        }

        /// <summary>
        /// Creates an XML trace node from the current stroke. Does not attach a reference time stamp element,
        /// but assumes it is already there.
        /// </summary>
        /// <param name="doc">The xml document.</param>
        /// <param name="attachTo">The xml element to which the trace should be attached to.</param>
        /// <param name="referenceTimestamp">The reference timestamp.</param>
        public void ToXmlNode(XmlDocument doc, XmlElement attachTo, DateTime referenceTimestamp)
        {
            XmlElement elem = doc.CreateElement("trace");
            elem.SetAttribute("id", ID);

            StringBuilder sb = new StringBuilder();
            string separator = ", ";
            for (int i = 0; i < AllPoints.Count; i++)
            {
                string traceString = AllPoints[i].ToTraceString(referenceTimestamp.Ticks);
                sb.Append(traceString + separator);
            }
            elem.InnerText = sb.ToString().TrimEnd(separator.ToCharArray());

            attachTo.AppendChild(elem);
        }
        #endregion

        #region Private Methods
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

        /// <summary>
        /// Creates the time stamp element.
        /// &lt;timestamp xml:id="#ID"&gt; #long number# &lt;/timestamp&gt;
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="attachTo">The attach to.</param>
        /// <returns></returns>
        private string CreateTimeStampElement(XmlDocument doc, XmlElement attachTo)
        {
            string timestampid = ID + "_ts";
            XmlElement elem = doc.CreateElement("timestamp");
            elem.SetAttribute("xml:id", timestampid);
            elem.InnerText = BeginPoint.Time.Ticks.ToString();
            attachTo.AppendChild(elem);
            return timestampid;
        }

        /// <summary>
        /// Creates the trace format element.
        /// &lt;traceFormat xml:id="KanjiStrokeTrace"&gt;
        /// &lt;channel name="X" type="decimal" /&gt;
        /// &lt;channel name="Y" type="decimal" /&gt;
        /// &lt;channel name="T" type="integer" units="ns" respectTo="#0001_ts" /&gt;
        /// &lt;/traceFormat&gt;
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="attachTo">The attach to.</param>
        /// <param name="timestampID">The timestamp ID.</param>
        private void CreateTraceFormatElement(XmlDocument doc, XmlElement attachTo, string timestampID)
        {
            XmlElement elem = doc.CreateElement("traceFormat");
            elem.SetAttribute("xml:id", ID + "_tf");

            XmlElement channel = doc.CreateElement("channel");
            channel.SetAttribute("name", "X");
            channel.SetAttribute("type", "decimal");
            elem.AppendChild(channel);

            channel = doc.CreateElement("channel");
            channel.SetAttribute("name", "Y");
            channel.SetAttribute("type", "decimal");
            elem.AppendChild(channel);

            channel = doc.CreateElement("channel");
            channel.SetAttribute("name", "T");
            channel.SetAttribute("type", "integer");
            channel.SetAttribute("units", "ns");
            channel.SetAttribute("respectTo", timestampID);
            elem.AppendChild(channel);

            attachTo.AppendChild(elem);
        }

        #endregion

        #region IStroke Members

        /// <summary>
        /// Gets all points.
        /// </summary>
        /// <returns>
        /// Returns a list of all points in the stroke.
        /// </returns>
        public List<IPoint> GetAllPoints()
        {
            return new List<IPoint>(AllPoints.ToArray());
        }

        /// <summary>
        /// Gets all points.
        /// </summary>
        /// <returns>
        /// Returns a list of all points in the stroke.
        /// </returns>
        public IPoint[] ToPointArray()
        {
            return AllPoints.ToArray();
        }

        /// <summary>
        /// Calculates the matching score between two IStrokes
        /// </summary>
        /// <param name="stroke">The second stroke.</param>
        /// <param name="strokematcher">An instance of a strokematcher class.</param>
        /// <returns>double: matching value</returns>
        public double MatchingScore(IStroke stroke, IStrokeMatcher strokematcher)
        {
            return strokematcher.Match(this, stroke);
        }

        /// <summary>
        /// Creates an Md5hash of the IStroke point sequence.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute
        /// the hash including the time information of the points.</param>
        /// <returns>A byte array with the hash.</returns>
        public byte[] Hash(bool withTime)
        {
            if (withTime)
            {
                if ((_hashWithTime != null) && (_hashWithTime.Length > 0)) 
                    { return _hashWithTime; }
                else 
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    return _hashWithTime = md5.ComputeHash(ToByteArray(withTime));
                }
            }
            else
                if ((_hash != null) && (_hash.Length > 0)) 
                    { return _hash; }
                else
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    return _hash = md5.ComputeHash(ToByteArray(withTime));
                } 
        }

        /// <summary>
        /// Creates an Md5hash of the IStroke point sequence, using only the
        /// point coordinate information, not the time information.
        /// </summary>
        public byte[] Hash() { return Hash(false); }

        /// <summary>
        /// Creates a byte array from the points coordinates
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> 
        /// include the timestamp information of the points.</param>
        /// <returns>A byte array of the point coordinates.</returns>
        public byte[] ToByteArray(bool withTime)
        {
            
            //we know that the Point class holds two double values
            //and a datetime so we're using the size
            //info in order to create a byte array of the stroke

            int pointNo = AllPoints.Count;
            int onePointLength = 
                withTime ? 2 * sizeof(double) + sizeof(long) : 2 * sizeof(double);
            
            byte[] r = new byte[pointNo * onePointLength];
            for (int i = 0; i < pointNo; i++ )
            {
                AllPoints[i].ToByteArray(withTime).CopyTo(r, i*onePointLength);
            }
            return r;
        }

        #endregion

        /// <summary>
        /// Creates a List&lt;Point&gt; from an InkML trace.
        /// </summary>
        /// <param name="trace">The trace.</param>
        /// <returns></returns>
        public static List<Point> PointListFromInkMLTrace(string trace)
        {
            List<Point> retval = new List<Point>();

            string[] traceElements = trace.Split(',');
            foreach (string point in traceElements)
            {
                string[] xyT = point.Trim().Split();

                retval.Add(
                    new Point(
                        double.Parse(xyT[0]), 
                        double.Parse(xyT[1]), 
                        new DateTime(
                            Int64.Parse(xyT[2]))));
            }

            return retval;
        }
    }
}
