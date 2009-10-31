using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public static class Area
    {
        /// <summary>
        /// Provides information about the intersection of two circles.
        /// </summary>
        /// <param name="circle1">circle 1</param>
        /// <param name="circle2">circle 2</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the circles don't touch anywhere.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if there is an overlapping area.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if circle2 lies completely inside circle1 and doesn't intersect.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.BEnclosesA"/> if circle1 lies completely inside circle2 and doesn't intersect.
        /// Additionally to Overlap and AEnclosesB/BEnclosesA: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the centre points are equal and the radius is equal.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the outer edge of circle1 touches the outer edge of circle2.
        /// </returns>
        public static AreaCode Intersect(Circle2D circle1, Circle2D circle2)
        {
            AreaCode retval = AreaCode.Unknown;

            // the circles are identical
            if (circle1.Equals(circle2))
            {
                retval =
                    AreaCode.AEnclosesB |
                    AreaCode.BEnclosesA |
                    AreaCode.Identical |
                    AreaCode.Intersect;
            }
            // the circles are not identical
            else
            {
                double distance = circle1.Centre.Distance(circle2.Centre);

                // the distance between the centres equals the sum of the radii
                // so the circles are tangents of each other
                if (distance == circle1.Radius + circle2.Radius)
                    retval = AreaCode.Tangents;
                // the distance between the centres is greater than the sum of the radii
                // therefore the circles must be disjoint
                else if (distance > circle1.Radius + circle2.Radius)
                    retval = AreaCode.Disjoint;

                // the distance between the centres is lower than the sum of the radii
                // therefore they must overlap in some way
                else if (distance < circle1.Radius + circle2.Radius)
                {
                    retval = AreaCode.Overlap;

                    // the sum of the distance and radius B is lower than radius A
                    // therefore: A encloses B
                    if (distance + circle2.Radius < circle1.Radius)
                        retval = retval | AreaCode.AEnclosesB;

                    // the sum of the distance and radius A is lower than radius B
                    // therefore: B encloses A
                    else if (distance + circle1.Radius < circle2.Radius)
                        retval = retval | AreaCode.BEnclosesA;
                    // no other possibility holds, the circles intersect
                    else 
                        retval = retval | AreaCode.Intersect;
                }
            }
            return retval;
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
        public static AreaCode Intersect(Circle2D circle2D, IStroke stroke)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides information about if the point lies in the circle.
        /// </summary>
        /// <param name="circle2D">The circle.</param>
        /// <param name="point">The point.</param>
        /// <returns>
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Unknown"/> if status is unknown.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Disjoint"/> if the point lies outside the circle.
        /// <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Overlap"/> if the point lies inside or on the circle edge.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.AEnclosesB"/> if the point lies inside and not on the edge.
        /// Additionally to Overlap and AEnclosesB: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Identical"/> if the point equals the centre point of the circle.
        /// Additionally to Overlap: <see cref="Kanji.DesktopApp.Interfaces.AreaCode.Tangents"/> if the point lies on the edge of the circle.
        /// </returns>
        public static AreaCode Intersect(Circle2D circle2D, Point point)
        {
            AreaCode retval = AreaCode.Unknown;
            if (circle2D.Centre.Distance((Point)point) < circle2D.Radius)
                retval = AreaCode.Overlap | AreaCode.AEnclosesB;
            else if (circle2D.Centre.Equals(point))
                retval = AreaCode.Overlap | AreaCode.AEnclosesB | AreaCode.Identical;
            else if (circle2D.Centre.Distance((Point)point) == circle2D.Radius)
                retval = AreaCode.Overlap | AreaCode.Tangents;
            else retval = AreaCode.Disjoint;

            return retval;
        }

        public static AreaCode Intersect(Circle2D circle2D, Square2D square2D)
        {
            return Area.Intersect(circle2D, square2D as Rectangle2D);
        }

        public static AreaCode Intersect(Circle2D circle2D, Rectangle2D rectangle2D)
        {
            throw new NotImplementedException();
        }

        internal static AreaCode Intersect(Rectangle2D rectangle2D, Circle2D circle2D)
        {
            throw new NotImplementedException();
        }

        internal static AreaCode Intersect(Rectangle2D rectangle2D, Square2D square2D)
        {
            throw new NotImplementedException();
        }

        internal static AreaCode Intersect(Rectangle2D rectangle2D, Rectangle2D rectangle2D_2)
        {
            throw new NotImplementedException();
        }

        internal static AreaCode Intersect(Rectangle2D rectangle2D, IStroke stroke)
        {
            throw new NotImplementedException();
        }

        internal static AreaCode Intersect(Rectangle2D rectangle2D, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
