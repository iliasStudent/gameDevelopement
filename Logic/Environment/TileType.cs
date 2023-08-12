using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Environment
{
    public class TileType
    {
        public Texture2D texture { get; set; }
        public int index { get; set; }
        public int hash { get; set; }

        public TileType(Texture2D texture, int index)
        {
            this.texture = texture;
            this.index = index;
            this.hash = texture.GetHashCode();
        }
    }
}
