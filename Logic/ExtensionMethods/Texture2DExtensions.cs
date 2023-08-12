using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.ExtensionMethods
{
    public static class Texture2DExtensions
    {
        public static Texture2D Cut(this Texture2D texture, Rectangle borders, GraphicsDevice graphicsDevice)
        {
            Texture2D croppedTexture = new Texture2D(graphicsDevice , borders.Width, borders.Height);

            Color[] data = new Color[borders.Width * borders.Height];
            texture.GetData(0, borders, data, 0, borders.Width * borders.Height);
            croppedTexture.SetData(data);
            return croppedTexture;
        }

        public static bool IsTransparent(this Texture2D texture)
        {
            int size = texture.Width * texture.Height;
            Color[] buffer = new Color[size];
            texture.GetData(0, texture.Bounds, buffer, 0, size);
            return buffer.All(c => c == Color.Transparent);
        }

        public static bool EqualsPixelBased(this Texture2D texture1, Texture2D texture2)
        {
            int size = texture1.Width * texture1.Height;
            Color[] buffer1 = new Color[size];
            Color[] buffer2 = new Color[size];
            texture1.GetData(0, texture1.Bounds, buffer1, 0, size);
            texture2.GetData(0, texture2.Bounds, buffer2, 0, size);

            bool temp = true;

            for (int i = 0; i < buffer1.Length; i++)
            {
                if (buffer1[i] != buffer2[i])
                {
                    temp = false;
                    break;
                }
            }

            return temp;
        }
    }
}
