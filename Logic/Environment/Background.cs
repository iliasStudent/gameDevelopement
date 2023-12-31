﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Environment
{
    public class Background
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        public Background(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

    }
}
