using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public interface IArea2D
    {
        /// <summary>
        /// Provides information if the two areas intersect.
        /// </summary>
        /// <param name="area">The other area.</param>
        /// <returns>An <see cref="Kanji.DesktopApp.Interfaces.AreaCode"/> depending on the status of the intersection.</returns>
        AreaCode Intersect(IArea2D area);

        /// <summary>
        /// Provides information if the area and the stroke intersect.
        /// </summary>
        /// <param name="stroke">The stroke</param>
        /// <returns>An <see cref="Kanji.DesktopApp.Interfaces.AreaCode"/> depending on the status of the intersection.</returns>
        AreaCode Intersect(IStroke stroke);

        /// <summary>
        /// Provides information if the point lies in the area.
        /// </summary>
        /// <param name="point">The point</param>
        /// <returns>An <see cref="Kanji.DesktopApp.Interfaces.AreaCode"/> depending on the status of the intersection.</returns>
        AreaCode Intersect(IPoint point);

        /// <summary>
        /// Calculates the 2D geometrical size of the area.
        /// </summary>
        /// <returns>The size of this area.</returns>
        double GeometricalSize();

        /// <summary>
        /// Product of an Area and a scalar value. Resizes the Area with the scalar value.
        /// </summary>
        /// <param name="s">Scalar value to be multiplied by </param>
        void Stretch(double s);
    }
}
