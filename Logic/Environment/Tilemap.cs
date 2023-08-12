using Logic.Core;
using Logic.ExtensionMethods;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Logic.Environment
{
    public class Tilemap
    {
        public List<TilemapLayer> ForeGrounds { get; set; }
        public TilemapLayer MiddleGround { get; set; }
        public List<TilemapLayer> Backgrounds { get; set; }
        public float Scale { get; set; }

        public Tilemap()
        {
            ForeGrounds = new List<TilemapLayer>();
            MiddleGround = new TilemapLayer(0);
            Backgrounds = new List<TilemapLayer>();
            this.Scale = 2f;
        }

        public Tilemap(Stream stream)
        {
            ForeGrounds = new List<TilemapLayer>();
            Backgrounds = new List<TilemapLayer>();
            Load(stream);
        }

        public void addTiles(Texture2D map, GraphicsDevice graphicsDevice, int layer = 0)
        {
            Debug.WriteLine("Beginning " + layer);
            for (int i = 0; i < map.Width / 16; i++)
            {
                for (int j = 0; j < map.Height / 16; j++)
                {
                    Texture2D texture = map.Cut(new Rectangle(i * 16, j * 16, 16, 16), graphicsDevice);
                    if (!texture.IsTransparent())
                    {
                        addTile(texture, new Vector2(i, j), layer);
                    }

                    
                    Debug.WriteLine($"{layer}: {(i* (map.Height / 16)) +j}/{(map.Width/16)* (map.Height/16)}");
                    
                }
            }
            Debug.WriteLine("Finised " + layer);
        }

        public void addTile(Texture2D texture, Vector2 position, int layer = 0)
        {
            var tileType = TileFactory.GetTileType(texture);
            GetTilemapLayer(layer).Tiles.Add(new Tile(tileType, position * 16 * Scale));
        }

        public void addTile(Texture2D textureTileSet, Vector2 position, Rectangle rectangle, GraphicsDevice graphicsDevice, int layer = 0)
        {
            Texture2D texture = textureTileSet.Cut(rectangle, graphicsDevice);
            if (!texture.IsTransparent())
            {
                addTile(texture, position, layer);
            }
        }

        public void draw(SpriteBatch _spriteBatch, Rectangle screen)
        {
            foreach (var tile in MiddleGround.Tiles)
            {
                if (CollisionManager.Detection(screen ,tile.GetCollisionRectangle()))
                {
                    tile.Draw(_spriteBatch);
                }
            }

            foreach (var foreGround in this.ForeGrounds)
            {
                foreGround.Draw(_spriteBatch, screen);
            }

            foreach (var background in this.Backgrounds)
            {
                background.Draw(_spriteBatch, screen);
            }
        }

        public List<Tuple<CollisionDirection, Rectangle>> hitAnyTile(Rectangle rectangle, int layer = 0)
        {
            return GetTilemapLayer(layer).Tiles.Select(x => x.CollisionDetection(rectangle)).Where(x => x != null).Where(x => x.Item1 != CollisionDirection.noHit).GroupBy(x => x.Item1).Select(x => x.First()).ToList();
        }

        private TilemapLayer GetTilemapLayer(int layer)
        {
            if (layer == 0)
            {
                return MiddleGround;
            }
            else if (layer > 0)
            {
                if (ForeGrounds.Count < layer)
                {
                    ForeGrounds.Add(new TilemapLayer(layer));
                }
                return ForeGrounds[layer-1];
            }
            else
            {
                if (Backgrounds.Count < Math.Abs(layer))
                {
                    Backgrounds.Add(new TilemapLayer(layer));
                }
                return Backgrounds[Math.Abs(layer)-1];
            }
        }

        public void Save(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Convert.ToDouble(Scale));
                writer.Write(ForeGrounds.Count);
                foreach (var foreGround in ForeGrounds)
                {
                    foreGround.Save(writer);
                }

                MiddleGround.Save(writer);

                writer.Write(Backgrounds.Count);
                foreach (var background in Backgrounds)
                {
                    background.Save(writer);
                }
            }
        }

        public void Load(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                Scale = (float)reader.ReadDouble();

                var ForeGroundsCount = reader.ReadInt32();

                for (int i = 0; i < ForeGroundsCount; i++)
                {
                    ForeGrounds.Add(new TilemapLayer(reader));
                }

                MiddleGround = new TilemapLayer(reader);

                var BackgroundsCount = reader.ReadInt32();

                for (int i = 0; i < BackgroundsCount; i++)
                {
                    Backgrounds.Add(new TilemapLayer(reader));
                }
            }
        }
    }
}