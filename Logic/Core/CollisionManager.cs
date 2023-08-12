using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Logic.Core
{
    public enum CollisionDirection
    {
        up,
        down,
        left,
        right,
        noHit
    }
    public static class CollisionManager
    {
        public static Tuple<CollisionDirection, Rectangle> DetectionAndDirection(Rectangle rectangle1, Rectangle rectangle2)
        {
            var intersectRectangle = Rectangle.Intersect(rectangle1, rectangle2);
            if (Detection(rectangle1, rectangle2))
            {
                return new Tuple<CollisionDirection, Rectangle>(RectanglesToDirection(rectangle1, rectangle2), intersectRectangle);
            }

            return new Tuple<CollisionDirection, Rectangle>(CollisionDirection.noHit, intersectRectangle);
        }

        public static bool Detection(Rectangle rectangle1, Point center, int width, int height)
        {
            var rectangle2 = new Rectangle(center, new Point(width, height));
            rectangle2.X -= width / 2;
            rectangle2.Y -= height / 2;
            return Detection(rectangle1, rectangle2);
        }

        public static bool Detection(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Intersects(rectangle2);
        }

        private static CollisionDirection RectanglesToDirection(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (IsTouchingLeft(rectangle1, rectangle2))
            {
                return CollisionDirection.left;

            }
            else if (IsTouchingRight(rectangle1, rectangle2))
            {
                return CollisionDirection.right;

            }
            else if (IsTouchingTop(rectangle1, rectangle2))
            {
                return CollisionDirection.up;

            }
            else if (IsTouchingBottom(rectangle1, rectangle2))
            {
                return CollisionDirection.down;
            }

            return CollisionDirection.noHit;
        }

        public static bool IsTouchingLeft(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Left &&
              rectangle1.Bottom > rectangle2.Top + 10 &&
              rectangle1.Top < rectangle2.Bottom - 10;
        }

        public static bool IsTouchingRight(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Left < rectangle2.Right &&
              rectangle1.Right > rectangle2.Right &&
              rectangle1.Bottom > rectangle2.Top + 10 &&
              rectangle1.Top < rectangle2.Bottom - 10;
        }

        public static bool IsTouchingTop(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Bottom > rectangle2.Top &&
              rectangle1.Top < rectangle2.Top &&
              rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Right;
        }
        public static bool IsTouchingBottom(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Top < rectangle2.Bottom &&
              rectangle1.Bottom > rectangle2.Bottom &&
              rectangle1.Right > rectangle2.Left &&
              rectangle1.Left < rectangle2.Right;
        }
    }
}
