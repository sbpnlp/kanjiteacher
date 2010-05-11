using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public class BoundingBox : Square2D, IBoundingBox
    {
        #region Private fields
        /// <summary>
        /// The padding factor describes how much whitespace is allowed around the
        /// bounding box
        /// </summary>
        private double _paddingFactor = 1;
        private List<Point> _pointList = new List<Point>();
        private List<Vector2> _vectorsFromAnchor = new List<Vector2>();
        #endregion

        #region Public fields
        public List<Point> PointList { get { return _pointList; } set { _pointList = value; Initialisation(); } }
        public List<Vector2> VectorsFromAnchor { get { return _vectorsFromAnchor; } }
        public double PaddingFactor { get { return _paddingFactor; } set { _paddingFactor = value; } }
        #endregion

        #region Constructors
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
            /* Remember that the coordinate system begins in the
             * upper left corner of the screen.
             * Therefore some calculations seem backwards,
             * like moving the anchor point around
             */

            _pointList = pointlist;
            Initialisation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="pointlist">The pointlist.</param>
        public BoundingBox(List<List<Point>> pointlist)
            : base()
        {
            /* Remember that the coordinate system begins in the
             * upper left corner of the screen.
             * Therefore some calculations seem backwards,
             * like moving the anchor point around
             */
            foreach (List<Point> pList in pointlist)
                _pointList.AddRange(pList);
            Initialisation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="pointlist">The pointlist.</param>
        public BoundingBox(List<Point> pointlist, double padding)
            : base()
        {
            /* Remember that the coordinate system begins in the
             * upper left corner of the screen.
             * Therefore some calculations seem backwards,
             * like moving the anchor point around
             */

            _paddingFactor = 1 - padding;
            _pointList = pointlist;
            Initialisation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> class.
        /// </summary>
        /// <param name="pointlist">The pointlist.</param>
        public BoundingBox(List<List<Point>> pointlist, double padding)
            : base()
        {
            /* Remember that the coordinate system begins in the
             * upper left corner of the screen.
             * Therefore some calculations seem backwards,
             * like moving the anchor point around
             */
            _paddingFactor = 1 - padding;
            foreach (List<Point> pList in pointlist)
                _pointList.AddRange(pList);
            Initialisation();
        }
        #endregion

        #region Private methods
        private void Initialisation()
        {
            Point uppermost, lowermost, leftmost, rightmost;
            if (_pointList.Count >= 1)
                uppermost = lowermost = leftmost = rightmost = _pointList[0];
            else throw new ArithmeticException("Bounding Box needs at least one point");

            // find uppermost, lowermost, leftmoest and rightmost point of pointlis
            foreach (Point p in _pointList)
            {
                if (p.X > rightmost.X) rightmost = p;
                if (p.X < leftmost.X) leftmost = p;
                if (p.Y < uppermost.Y) uppermost = p; //this seems backwards:
                if (p.Y > lowermost.Y) lowermost = p; //due to upside down screen coordinates
            }

            //create bounding rectangle for some calculations
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
                r.Anchor.Y -= ((r.Width - r.Height) / 2); //backwards, because of screen coordinates
                r.Height = r.Width;
            }

            // apply values of rectangle to this bounding box
            Anchor = r.Anchor;
            Width = r.Width;

            // additional padding to increase box size
            double paddedWidth = Width * _paddingFactor;
            //calculate average between the two for movement of anchor point
            double move = (paddedWidth - Width) / 2;
            //set new width
            Width = paddedWidth;
            //move anchor point
            Anchor.X -= move;
            Anchor.Y -= move; //backwards, due to screen coordinates beginning in upper left corner

            //move pointlist into vector list, relative to anchor point
            _vectorsFromAnchor = new List<Vector2>(_pointList.Count);
            foreach (Point p in _pointList)
            {
                _vectorsFromAnchor.Add(new Vector2(Anchor, p));
            }
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Product of a BoundingBox and a scalar value. Resizes the BoundingBox
        /// and the values of the Points inside with the scalar value.
        /// </summary>
        /// <param name="s">Scalar value to be multiplied by</param>
        public new void Stretch(double s)
        {
            Width *= s;
            for (int i = 0; i < _vectorsFromAnchor.Count; i++)
            {
                _vectorsFromAnchor[i] = _vectorsFromAnchor[i] * s;
            }
        }
        #endregion
    }
}
