using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public class BoundingBox : Square2D, IBoundingBox
    {
        private double paddingFactor = 1.1;
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox "/> class.
        /// </summary>
        /// <param name="handle">The upper left corner of the square.</param>
        /// <param name="width">The width of the square.</param>
        public BoundingBox(Point handle, double width) : base(handle, width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="positionVectorToHandlePoint">The upper left corner of the bounding box, 
        /// represented as a  vector.</param>
        /// <param name="width">The width of the bounding box</param>
        public BoundingBox(Vector2 positionVectorToHandlePoint, double width)
            : base(positionVectorToHandlePoint, width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="pointlist">The pointlist.</param>
        public BoundingBox(List<Point> pointlist) : base()
        {
            Point uppermost, lowermost, leftmost, rightmost;
            if (pointlist.Count >= 1)
                uppermost = lowermost = leftmost = rightmost = pointlist[0];
            else throw new ArithmeticException("Bounding Box needs at least one point");

            // find uppermost, lowermost, leftmoest and rightmost point of pointlis
            foreach (Point p in pointlist)
            {
                if (p.X > rightmost.X) rightmost = p;
                if (p.X < leftmost.X) leftmost = p;
                if (p.Y > uppermost.Y) uppermost = p;
                if (p.Y < lowermost.Y) lowermost = p;
            }

            Rectangle2D r = new Rectangle2D(
                new Point(leftmost.X, uppermost.Y),
                new Point(rightmost.X, lowermost.Y));

            if (r.Height > r.Width)
            {
                // increase width to height
                r.Anchor.X -= ((r.Height - r.Width) / 2);
                r.Width = r.Height;
            }
            else if (r.Width > r.Height)
            {
                // increase height to width
                r.Anchor.Y -= ((r.Width - r.Height) / 2);
                r.Height = r.Width;
            }

            // apply values of rectangle to this bounding box
            Anchor = r.Anchor;
            Width = r.Width;

            // additional padding to increase box size
            double move = (Width * paddingFactor - Width) / 2;
            Anchor.X -= Math.Floor(move);
            Anchor.Y -= Math.Floor(move);
            Width = Math.Floor(Width * paddingFactor);
        }
    }
}
