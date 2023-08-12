using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.AnimationEngine
{
    public class AnimationFrame
    {
        public Rectangle borders { get; set; }

        public AnimationFrame(Rectangle borders)
        {
            this.borders = borders;
        }
    }
}
