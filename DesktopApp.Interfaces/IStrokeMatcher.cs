﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// The IStrokematcher interface defines the interface to the strokematching
    /// class. The strokematching class calculates a matching score between two strokes.
    /// </summary>
    public interface IStrokeMatcher
    {
        /// <summary>
        /// Calculates a matching score between stroke1 and stroke2.
        /// </summary>
        /// <param name="stroke1">The first stroke.</param>
        /// <param name="stroke2">The second stroke.</param>
        /// <returns>Matching score as a double value.</returns>
        double Match(IStroke stroke1, IStroke stroke2);
    }
}
