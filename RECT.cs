using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveWindow
{
    /// <summary>
    /// The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect</remarks>
    struct RECT
    {
        /// <summary>Specifies the x-coordinate of the upper-left corner of the rectangle.</summary>
        public int Left;
        /// <summary>Specifies the y-coordinate of the upper-left corner of the rectangle.</summary>
        public int Top;
        /// <summary>Specifies the x-coordinate of the lower-right corner of the rectangle.</summary>
        public int Right;
        /// <summary>Specifies the y-coordinate of the lower-right corner of the rectangle.</summary>
        public int Bottom;
    }
}
