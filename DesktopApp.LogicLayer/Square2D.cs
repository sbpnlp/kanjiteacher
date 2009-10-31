using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    public class Square2D : Rectangle2D, ISquare2D
    {
        #region Fields
        /// <summary>
        /// Gets or sets the Height and the Width.
        /// </summary>
        /// <value>The height.</value>
        public new double Height { get { return _width; } set { _width = _height = value; } }

        /// <summary>
        /// Gets or sets the Height and the Width.
        /// </summary>
        /// <value>The width.</value>
        public new double Width { get { return _width; } set { _width = _height = value; } }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new empty instance of the <see cref="Square2D"/> class.
        /// </summary>
        public Square2D()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Square2D"/> class.
        /// </summary>
        /// <param name="handle">The upper left corner of the square.</param>
        /// <param name="width">The width of the square.</param>
        public Square2D(Point handle, double width) : base(handle, width, width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Square2D"/> class.
        /// </summary>
        /// <param name="positionVectorToHandlePoint">The upper left corner of the square, represented as a  vector.</param>
        /// <param name="width">The width of the square.</param>
        public Square2D(Vector2 positionVectorToHandlePoint, double width) : base(positionVectorToHandlePoint, width, width)
        {
        }
        #endregion

    }
}
