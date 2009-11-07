using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Represents a circle in a 2D cartesian coordinate system.
    /// </summary>
    public class Circle2D : IArea2D, IEquatable<Circle2D>
    {
        #region Fields
        /// <summary>
        /// Gets or sets the length of the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets the centre point of the circle.
        /// </summary>
        /// <value>The centre.</value>
        public Point Centre { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="centre">The centre point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public Circle2D(Point centre, double radius)
        {
            Radius = radius;
            Centre = centre;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="positionVectorToCentrePoint">The position vector to centre point.</param>
        /// <param name="radius">The radius.</param>
        public Circle2D(Vector2 positionVectorToCentrePoint, double radius)
        {
            Radius = radius;
            Centre = new Point(positionVectorToCentrePoint.X, positionVectorToCentrePoint.Y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="centre">The centre.</param>
        /// <param name="edge">A point that lies on the edge of the circle.</param>
        public Circle2D(Point centre, Point edge)
        {
            Radius = centre.Distance(edge);
            Centre = centre;
        }

        #endregion

        #region IArea2D Members

        /// <summary>
        /// Provides information about the intersection of this circle with another area.
        /// </summary>
        /// <param name="area">area</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the area doesn't touch the circle anywhere.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if there is an overlapping area between the two.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the area lies completely inside this circle and doesn't intersect.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.BEnclosesA"/> if this circle lies completely inside the area and doesn't intersect.
        /// Additionally to Overlap and AEnclosesB/BEnclosesA: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the other area is a circle, the centre points are equal and the radius is equal.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the outer edge of this circle touches the outer edge of the area.
        /// </returns>
        public AreaCode Intersect(IArea2D area)
        {
            if (area is Circle2D) return Area.Intersect(this, area as Circle2D);
            if (area is Square2D) return Area.Intersect(this, area as Square2D);
            if (area is Rectangle2D) return Area.Intersect(this, area as Rectangle2D);
            else return AreaCode.Unknown;
        }

        /// <summary>
        /// Provides information about if the stroke intersects the circle.
        /// </summary>
        /// <param name="stroke">The stroke.</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the stroke lies outside the circle.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if the stroke lies inside, intersects or is a tangent of the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the stroke lies completely inside the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Intersect"/> if the stroke intersects the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the stroke is a tangent of the circle.
        /// </returns>
        public AreaCode Intersect(IStroke stroke)
        {
            return Area.Intersect(this, stroke);
        }

        /// <summary>
        /// Provides information about if the point lies in the circle.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the point lies outside the circle.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if the point lies inside or on the circle edge.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the point lies inside and not on the edge.
        /// Additionally to Overlap and AEnclosesB: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the point equals the centre point of the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the point lies on the edge of the circle.
        /// </returns>
        public AreaCode Intersect(IPoint point)
        {
            return Area.Intersect(this, (Point) point);
        }

        /// <summary>
        /// Calculates the 2D geometrical size of the area.
        /// </summary>
        /// <returns>The size of this area.</returns>
        public double GeometricalSize()
        {
            return Math.PI * Radius * Radius;
        }

        #endregion

        #region Overridden Object methods
        /// <summary>
        /// Compares this Kanji.DesktopApp.LogicLayer.Circle2D object with another one.
        /// The result specifies whether the values of the Kanji.DesktopApp.LogicLayer.Circle2D.Centre
        /// and Kanji.DesktopApp.LogicLayer.Circle2D.Radius properties of the two 
        /// Kanji.DesktopApp.LogicLayer.Circle2D objects are equal.
        /// </summary>
        /// <param name="obj">The Kanji.DesktopApp.LogicLayer.Circle2D to compare.</param>
        /// <returns>True if the values of the Kanji.DesktopApp.LogicLayer.Circle2D.Radius properties 
        /// and the Kanji.DesktopApp.LogicLayer.Circle2D.Centre properties of this and obj are equal; 
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Circle2D) 
                return Equals((Circle2D)obj);
            else
                return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region IEquatable<Circle2D> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Circle2D other)
        {
            // the equal method of Point also asks for the time stamp to 
            // be equal, therefore, using the EqualPosition method
            return other.Centre.EqualPosition(Centre) &&
                other.Radius.Equals(Radius);
        }

        #endregion
    }
}
