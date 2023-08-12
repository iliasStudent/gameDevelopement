using Logic.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Characters
{
    public interface ICollisionable
    {
        public Rectangle GetNextCollisionRectangle();
        public Rectangle GetCollisionRectangle();
        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle);

    }
}
