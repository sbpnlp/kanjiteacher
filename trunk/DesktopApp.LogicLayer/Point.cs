using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drawing = System.Drawing;
using Kanji.DesktopApp.Interfaces;
using System.Xml;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Kanji.DesktopApp.LogicLayer
{
    public class Point : IEquatable<Point>, IPoint
    {
        #region Fields
        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>.
        /// </summary>
        /// <value>The x-coordinate of this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>.</value>
        public double X { get; set; }
        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>.
        /// </summary>
        /// <value>The y-coordinate of this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>.</value>
        public double Y { get; set; }
        /// <summary>
        /// Returns a position vector from the point coordinates.
        /// </summary>
        /// <value>The <see cref="Kanji.DesktopApp.LogicLayer.Vector2"/>.</value>
        public Vector2 Vector { get { return new Vector2(X, Y); } }
        /// <summary>
        /// Gets or sets the time at which the point sample was taken.
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { get; set; }
        /// <summary>
        /// Gets a <see cref="System.Drawing.Point"/> from this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>
        /// </summary>
        /// <value>The System.Drawing.Point.</value>
        public Drawing.Point SysDrawPoint { get { return new Drawing.Point((int)Math.Floor(X), (int)Math.Floor(Y)); } }
        /// <summary>
        /// Gets a <see cref="System.Drawing.PointF"/> from this <see cref="Kanji.DesktopApp.LogicLayer.Point"/>
        /// </summary>
        /// <value>The System.Drawing.PointF.</value>
        public Drawing.PointF SysDrawPointF { get { return new Drawing.PointF((float) X, (float) Y); } }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="y">The y-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="y">The y-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        public Point(int x, int y)
        {
            X = (double)x;
            Y = (double)y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="pt">An instance of System.Drawing.Point.</param>
        public Point(Drawing.Point pt)
        {
            X = (double)pt.X;
            Y = (double)pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="pt">An instance of System.Drawing.PointF.</param>
        public Point(Drawing.PointF pt)
        {
            X = (double)pt.X;
            Y = (double)pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="y">The y-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="time">The time at which the point sample was taken.</param>
        public Point(double x, double y, DateTime time)
        {
            X = x;
            Y = y;
            Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="y">The y-coordinate of this Kanji.DesktopApp.LogicLayer.Point.</param>
        /// <param name="time">The time at which the point sample was taken.</param>
        public Point(int x, int y, DateTime time)
        {
            X = (double)x;
            Y = (double)y;
            Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="pt">An instance of System.Drawing.Point.</param>
        /// <param name="time">The time at which the point sample was taken.</param>
        public Point(Drawing.Point pt, DateTime time)
        {
            X = (double)pt.X;
            Y = (double)pt.Y;
            Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kanji.DesktopApp.LogicLayer.Point"/> class.
        /// </summary>
        /// <param name="pt">An instance of System.Drawing.PointF.</param>
        /// <param name="time">The time at which the point sample was taken.</param>
        public Point(Drawing.PointF pt, DateTime time)
        {
            X = (double)pt.X;
            Y = (double)pt.Y;
            Time = time;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Gets a value indicating whether this instance of Kanji.DesktopApp.LogicLayer.Point is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        /// <returns>true if both Kanji.DesktopApp.LogicLayer.Point.X and 
        /// Kanji.DesktopApp.LogicLayer.Point.Y are 0; otherwise, false.</returns>
        public bool IsEmpty { get { return this.Equals(Point.Empty); } }

        /// <summary>
        /// Creates a position vector for a given Point
        /// </summary>
        /// <param name="p">The point.</param>
        /// <returns>Position vector for a point</returns>
        public Vector2 PositionVector(Point p)
        {
            return new Vector2(p.X, p.Y);
        }

        /// <summary>
        /// Computes the distance from this point to the specified point p2.
        /// </summary>
        /// <param name="p2">Point p2.</param>
        /// <returns>The distance from this point to the specified point p2.</returns>
        public double Distance(Point p2)
        {
            double a = Math.Pow((X - p2.X), 2);
            double b = Math.Pow((Y - p2.Y), 2);
            return Math.Sqrt(a + b);
        }

        /// <summary>
        /// Compares this Kanji.DesktopApp.LogicLayer.Point object with another one.
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Point.X 
        /// and Kanji.DesktopApp.LogicLayer.Point.Y properties of the two 
        /// Kanji.DesktopApp.LogicLayer.Point objects are equal.
        /// </summary>
        /// <param name="obje">The Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <returns>The result of the operator. true if the values of the 
        /// Kanji.DesktopApp.LogicLayer.Point.X properties and the Kanji.DesktopApp.LogicLayer.Point.Y 
        /// properties of left and right are equal; otherwise, false.</returns>
        public bool EqualPosition(Point other)
        {
            return ((other.X == X) && (other.Y == Y));
        }

        public string ToTraceString(long timestamp)
        {
            return string.Format("{0} {1} {2}", X, Y, Time.Ticks - timestamp);
        }

        /// <summary>
        /// Creates a byte array from the points coordinates
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> 
        /// include the timestamp information of the points.</param>
        /// <returns>A byte array of the point coordinates.</returns>
        public byte[] ToByteArray(bool withTime)
        {
            int len = 2;
            byte[] temp0, temp1, temp2;
            temp0 = BitConverter.GetBytes(X);
            temp1 = BitConverter.GetBytes(Y);
            if (withTime)
                temp2 = BitConverter.GetBytes(Time.Ticks);
            else temp2 = new byte[0];
            len = temp0.Length + temp1.Length + temp2.Length;
            byte[] r = new byte[len];
            temp0.CopyTo(r, 0);
            temp1.CopyTo(r, temp0.Length);
            temp2.CopyTo(r, temp0.Length + temp1.Length); //this may be of 0 length: no copy operation
            return r;
        }

        #endregion

        #region IPoint Members

        /// <summary>
        /// Creates an XML string representation of the IPoint
        /// </summary>
        /// <returns>An XML string</returns>
        public string ToXmlString()
        {
            return String.Format("<point x=\"{0}\" y=\"{1}\" time=\"{2}\" />", X, Y, Time.Ticks);
        }

        /// <summary>
        /// Creates an Md5hash of the IPoint coordinates.
        /// </summary>
        /// <param name="withTime">if set to <c>true</c> compute
        /// the hash including the time information.</param>
        /// <returns>A byte array with the hash.</returns>
        public byte[] Hash(bool withTime)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(ToByteArray(withTime));
        }
        #endregion

        #region IEquatable<Point> Members

        /// <summary>
        /// Compares this Kanji.DesktopApp.LogicLayer.Point object with another one.
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Point.X 
        /// Kanji.DesktopApp.LogicLayer.Point.Y and Kanji.DesktopApp.LogicLayer.Point.Time properties 
        /// of the two Kanji.DesktopApp.LogicLayer.Point objects are equal.
        /// </summary>
        /// <param name="other">The Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <returns>The result of the operator. true if the values of the 
        /// Kanji.DesktopApp.LogicLayer.Point.X properties, the 
        /// Kanji.DesktopApp.LogicLayer.Point.Y properties
        /// and the Kanji.DesktopApp.LogicLayer.Point.Time properties are equal; otherwise, false.</returns>
        public bool Equals(Point other)
        {
            return ((other.Time == this.Time) &&
                    (other.X == this.X) &&
                    (other.Y == this.Y));
        }

        #endregion

        #region Overridden Object methods
        /// <summary>
        /// Compares this Kanji.DesktopApp.LogicLayer.Point object with another one.
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Point.X 
        /// Kanji.DesktopApp.LogicLayer.Point.Y and Kanji.DesktopApp.LogicLayer.Point.Time properties 
        /// of the two Kanji.DesktopApp.LogicLayer.Point objects are equal.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>The result of the operator. true if the values of the 
        /// Kanji.DesktopApp.LogicLayer.Point.X properties, the 
        /// Kanji.DesktopApp.LogicLayer.Point.Y properties
        /// and the Kanji.DesktopApp.LogicLayer.Point.Time properties are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Point)
                return Equals((Point)obj);
            else
                return false;
        }

        /// <summary>
        /// Returns a hash code for this instance of Kanji.DesktopApp.LogicLayer.Point.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            Drawing.PointF pt = new Drawing.PointF((float)X, (float)Y);
            return pt.GetHashCode() + Time.GetHashCode();
        }

        /// <summary>
        /// Returns a human-readable <see cref="System.String"/> that represents this instance
        /// of Kanji.DesktopApp.LogicLayer.Point.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this Kanji.DesktopApp.LogicLayer.Point.
        /// </returns>
        public override string ToString()
        {
            return String.Format("Point: X = {0}, Y = {1}, Time = {2}:{3}", X, Y, Time.ToString(), Time.Millisecond);
        }
        #endregion

        #region Static methods and operators
        /// <summary>
        /// Static variable that represents an empty point.
        /// </summary>
        private static Point empty = new Point(Double.MinValue, Double.MaxValue);

        /// <summary>
        /// Implements the operator !=. Compares two Kanji.DesktopApp.LogicLayer.Point objects. 
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Point.X 
        /// or Kanji.DesktopApp.LogicLayer.Point.Y properties of the two 
        /// Kanji.DesktopApp.LogicLayer.Point objects are unequal.
        /// </summary>
        /// <param name="left">The left Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <param name="right">The right Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <returns>The result of the operator. true if the values of either the 
        /// Kanji.DesktopApp.LogicLayer.Point.X properties or the Kanji.DesktopApp.LogicLayer.Point.Y 
        /// properties of left and right differ; otherwise, false.</returns>
        public static bool operator !=(Point left, Point right) { return !left.Equals(right); }

        /// <summary>
        /// Implements the operator ==. Compares two Kanji.DesktopApp.LogicLayer.Point objects. 
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Point.X 
        /// and Kanji.DesktopApp.LogicLayer.Point.Y properties of the two 
        /// Kanji.DesktopApp.LogicLayer.Point objects are equal.
        /// </summary>
        /// <param name="left">The left Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <param name="right">The right Kanji.DesktopApp.LogicLayer.Point to compare.</param>
        /// <returns>The result of the operator. true if the values of the 
        /// Kanji.DesktopApp.LogicLayer.Point.X properties and the Kanji.DesktopApp.LogicLayer.Point.Y 
        /// properties of left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Point left, Point right) { return left.Equals(right); }

        // Summary:
        //     Represents a Kanji.DesktopApp.LogicLayer.Point that has Kanji.DesktopApp.LogicLayer.Point.X and Kanji.DesktopApp.LogicLayer.Point.Y
        //     values set to zero.
        public static Point Empty { get { return empty; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="XmlString">The XML string as it is returned by the ToXmlString method.</param>
        public static Point FromXmlString(string xmlString)
        {
            //TODO Check out how XML reading really works
            //XmlTextReader xmlr = new XmlTextReader(xmlString, XmlNodeType.Element, new XmlParserContext(null, null, "", XmlSpace.Default));
            Regex r = new Regex("<point x=\"(.*)\" y=\"(.*)\" time=\"(.*)\" />");
            Match rMatch = r.Match(xmlString);
            //Console.WriteLine(rMatch.Length);
            //if (rMatch.Success) Console.WriteLine("Success: ");
            //else Console.Write("No success: ");
            //Console.Write(rMatch.Groups.Count);
            //Console.WriteLine(" groups");
            //int i = 0;
            //while (i < rMatch.Groups.Count) Console.WriteLine(rMatch.Groups[i++]);
            if (rMatch.Success)
            {
                double X = Double.Parse(rMatch.Groups[1].ToString());
                double Y = Double.Parse(rMatch.Groups[2].ToString());
                DateTime date = new DateTime(Int64.Parse(rMatch.Groups[3].ToString()));

                return new Point(X, Y, date);
            }
            else return null;
        }

        public static bool IsNullOrEmpty(Point p)
        {
            if (p != null)
            {
                return p.IsEmpty;
            }
            else return true; 
        }
        #endregion
    }
}
