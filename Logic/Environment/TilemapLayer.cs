using Logic.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.Environment
{
    public class TilemapLayer
    {
        public List<Tile> Tiles { get; set; }
        public int LayerDepth { get; set; }

        public TilemapLayer(int layerDepth)
        {
            Tiles = new List<Tile>();
            LayerDepth = layerDepth;
        }

        public TilemapLayer(BinaryReader reader)
        {
            Tiles = new List<Tile>();
            Load(reader);
        }

        internal void Draw(SpriteBatch _spriteBatch, Rectangle screen)
        {
            foreach (var tile in Tiles)
            {
                if (CollisionManager.Detection(screen, tile.GetCollisionRectangle()))
                {
                    tile.Draw(_spriteBatch, 0.5f + ((float)LayerDepth / 100f));
                }
            }
        }

        internal void Save(BinaryWriter writer)
        {
            writer.Write(LayerDepth);
            writer.Write(Tiles.Count);

            foreach (var tile in Tiles)
            {
                tile.Save(writer);
            }
        }

        internal void Load(BinaryReader reader)
        {
            LayerDepth = reader.ReadInt32();
            var tileCount = reader.ReadInt32();

            for (int i = 0; i < tileCount; i++)
            {
                Tiles.Add(new Tile(reader));
            }
        }
    }
}
