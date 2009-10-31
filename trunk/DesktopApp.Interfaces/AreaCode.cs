using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    /// <summary>
    /// Enum that specifies the different possibilities for areas
    /// to touch or not touch each other.
    /// </summary>
    public enum AreaCode
    {
        /// <summary>
        /// The areas are disjoint.
        /// </summary>
        Disjoint = 0x00000001,
        /// <summary>
        /// The areas intersect.
        /// </summary>
        Intersect = 0x00000002,
        /// <summary>
        /// Area A encloses area B
        /// </summary>
        AEnclosesB = 0x00000004,
        /// <summary>
        /// Area B encloses area A
        /// </summary>
        BEnclosesA = 0x00000008,
        /// <summary>
        /// Area A and B are tangents of each other.
        /// </summary>
        Tangents = 0x00000010,
        /// <summary>
        /// Areas A and B are identical.
        /// </summary>
        Identical = 0x00000020,
        /// <summary>
        /// The status of the areas towards each other is unknown.
        /// </summary>
        Unknown = 0x00000040,
        /// <summary>
        /// The areas overlap, but don't necessarily intersect.
        /// </summary>
        Overlap = 0x00000080
    }
}
