using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Represents a rectangle in a 2D cartesian coordinate system.
    /// </summary>
    public class Rectangle2D : IArea2D, IEquatable<Rectangle2D>, IRectangle2D
    {
        #region Fields

        /// <summary>
        /// The width of the rectangle
        /// </summary>
        protected double _width = 0;

        /// <summary>
        /// The height of the rectangle
        /// </summary>
        protected double _height = 0;

        /// <summary>
        /// Gets or sets the Width
        /// </summary>
        /// <value>The width.</value>
        public double Width { get { return _width; } set { _width = value; } }

        /// <summary>
        /// Gets or sets the Height
        /// </summary>
        /// <value>The height.</value>
        public double Height { get { return _height; } set { _height = value; } }

        /// <summary>
        /// Gets or sets the upper left corner of the rectangle.
        /// </summary>
        /// <value>The handle.</value>
        public Point Anchor { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance of the <see cref="Rectangle2D"/> class.
        /// </summary>
        public Rectangle2D()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class.
        /// </summary>
        /// <param name="handle">The upper left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Rectangle2D(Point handle, double width, double height)
        {
            Anchor = handle;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class.
        /// </summary>
        /// <param name="positionVectorToHandlePoint">The upper left corner of the rectangle, represented as a  vector.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Rectangle2D(Vector2 positionVectorToHandlePoint, double width, double height)
        {
            Anchor = new Point(positionVectorToHandlePoint.X, positionVectorToHandlePoint.Y);
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle2D"/> class.
        /// </summary>
        /// <param name="upperLeft">The upper left point of the rectangle.</param>
        /// <param name="lowerRight">The lower right point of the rectangle.</param>
        public Rectangle2D(Point upperLeft, Point lowerRight)
        {
            Anchor = upperLeft;
            Width = lowerRight.X - upperLeft.X;
            Height = upperLeft.Y - lowerRight.Y;
        }

        #endregion

        #region IArea2D Members

        /// <summary>
        /// Provides information about the intersection of this rectangle with another area.
        /// </summary>
        /// <param name="area">The other area</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the area doesn't touch the rectangle anywhere.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if there is an overlapping area between the two.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the area lies completely inside this rectangle and doesn't intersect.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.BEnclosesA"/> if this rectangle lies completely inside the area and doesn't intersect.
        /// Additionally to Overlap and AEnclosesB/BEnclosesA: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the other area is a rectangle, the handle points are equal and length of the sides is equal.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the outer edge of this rectangle touches the outer edge of the area.
        /// </returns>
        public AreaCode Intersect(IArea2D area)
        {
            if (area is Circle2D) return Area.Intersect(this, area as Circle2D);
            if (area is Square2D) return Area.Intersect(this, area as Square2D);
            if (area is Rectangle2D) return Area.Intersect(this, area as Rectangle2D);
            else return AreaCode.Unknown;
        }

        /// <summary>
        /// Provides information about if the stroke intersects the rectangle.
        /// </summary>
        /// <param name="stroke">The stroke.</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the stroke lies outside the rectangle.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if the stroke lies inside, intersects or is a tangent of the rectangle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the stroke lies completely inside the rectangle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Intersect"/> if the stroke intersects the rectangle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the stroke is a tangent of the rectangle.
        /// </returns>
        public AreaCode Intersect(IStroke stroke)
        {
            return Area.Intersect(this, stroke);
        }

        /// <summary>
        /// Provides information about if the point lies in the rectangle.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the point lies outside the rectangle.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if the point lies inside or on the rectangle's edge.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the point lies inside and not on the edge.
        /// Additionally to Overlap and AEnclosesB: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the point equals the handle point of the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the point lies on the edge of the rectangle.
        /// </returns>
        public AreaCode Intersect(IPoint point)
        {
            return Area.Intersect(this, (Point)point);
        }

        /// <summary>
        /// Calculates the 2D geometrical size of the area.
        /// </summary>
        /// <returns>The size of this area.</returns>
        public double GeometricalSize()
        {
            return Width * Height;
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
            if (obj is Rectangle2D)
                return Equals((Rectangle2D)obj);
            else
                return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{3}:\n\tAnchor point: {0}\n\tWidth:{1}\n\tHeight{2}", 
                Anchor, Width, Height, this.GetType().ToString());
        }
        #endregion

        #region IEquatable<Rectangle2D> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Rectangle2D other)
        {
            // the equal method of Point also asks for the time stamp to 
            // be equal, therefore, using the EqualPosition method
            return other.Anchor.EqualPosition(Anchor) &&
                (Width == other.Width) &&
                (Height == other.Height);
        }

        #endregion
    }
}
