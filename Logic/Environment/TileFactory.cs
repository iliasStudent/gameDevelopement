using Logic.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logic.Environment
{
    public class TileFactory
    {

        public static List<TileType> TileTypes { get; set; } = new List<TileType>();

        public const int maxTileSetWidth = 32;

        public static TileType GetTileType(Texture2D texture)
        {
            var tileType = TileTypes.FirstOrDefault(x => x.texture.EqualsPixelBased(texture));

            if (tileType == null)
            {
                tileType = new TileType(texture, TileTypes.Count);
                TileTypes.Add(tileType);
            }

            return tileType;
        }

        public static TileType GetTileType(int index)
        {
            var tileType = TileTypes.FirstOrDefault(x => x.index == index);

            return tileType;
        }

        public static void Save(GraphicsDevice graphicsDevice)
        {
            using (FileStream fs = File.Create(@"ExportedTileSet.png"))
            {
                RenderTarget2D renderTarget2D = new RenderTarget2D(graphicsDevice, maxTileSetWidth * 16, (TileTypes.Count / maxTileSetWidth + 1) * 16);
                graphicsDevice.SetRenderTarget(renderTarget2D);

                graphicsDevice.Clear(Color.Transparent);

                var spriteBatch = new SpriteBatch(graphicsDevice);

                spriteBatch.Begin();

                int counterH = 0;
                int counterW = 0;

                foreach (var tileType in TileTypes)
                {
                    spriteBatch.Draw(tileType.texture, new Vector2(counterW * 16, counterH * 16), Color.White);

                    counterW++;

                    if (counterW >= maxTileSetWidth)
                    {
                        counterH++;
                        counterW = 0;
                    }
                }

                spriteBatch.End();

                renderTarget2D.SaveAsPng(fs, renderTarget2D.Width, renderTarget2D.Height);

                graphicsDevice.SetRenderTarget(null);
            }
        }

        public static void load(GraphicsDevice graphicsDevice, Texture2D tileSet)
        {
            TileTypes = new List<TileType>();
            for (int i = 0; i < tileSet.Height / 16; i++)
            {
                for (int j = 0; j < tileSet.Width / 16; j++)
                {
                    TileTypes.Add(new TileType(tileSet.Cut(new Rectangle(j * 16, i * 16, 16, 16), graphicsDevice), j + i * maxTileSetWidth));
                }
            }
        }
    }
}
