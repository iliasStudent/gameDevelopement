using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class Component
    {
        public abstract void Draw(SpriteBatch spriteBacth);

        public abstract void Update(GameTime gameTime);

    }
}