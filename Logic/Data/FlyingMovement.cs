using Logic.ExtensionMethods;
using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Logic.Data
{
    public class FlyingMovement : Movement
    {
        public override void update(GameTime gameTime, IAnimationable animationable, IMoveable moveable)
        {
            if (Velocity.X >= MaxSpeedX)
            {
                Velocity.X = MaxSpeedX;
            }
            else if (Velocity.X <= -MaxSpeedX)
            {
                Velocity.X = -MaxSpeedX;
            }

            if (!IsButtonXPressed)
            {
                if (Velocity.X > 0)
                {
                    Velocity.X -= deceleration;

                }
                else if (Velocity.X < 0)
                {
                    Velocity.X += deceleration;
                }
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                if (animationable.currentAnimation.AnimatieNaam.canMove())
                {
                    animationable.ChangeAnimation(AnimationsTypes.idle, true);
                }
            }

            moveable.position += Velocity * gameTime.ElapsedGameTime.Ticks / 100000;

            IsButtonXPressed = false;
        }
    }
}
