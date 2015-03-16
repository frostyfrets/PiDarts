using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiDarts.Core.Layout
{

    /// <summary>
    /// Represents an area on the screen that is reserved for drawing user scores.
    /// </summary>
    public class Region
    {
        public Vector2 location;
        public float height;
        public float width;
        public float padding;
        public float margin;

        public Region() { }
    }
}
