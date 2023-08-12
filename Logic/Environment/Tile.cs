using Logic.Core;
using Logic.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Logic.Environment
{
    public class Tile : ICollisionable
    {
        public TileType tileType { get; set; }

        public Vector2 position { get; set; }

        public Tile(TileType tileType, Vector2 position)
        {
            this.tileType = tileType;
            this.position = position;
        }

        public Tile(BinaryReader reader)
        {
            Load(reader);
        }

        public void Draw(SpriteBatch _spriteBatch, float layerDepth = 0.5f)
        {
            _spriteBatch.Draw(tileType.texture, position, tileType.texture.Bounds, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, layerDepth);
        }

        public Rectangle GetNextCollisionRectangle()
        {
            var rectangle = tileType.texture.Bounds;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            rectangle.Width *= 2;
            rectangle.Height *= 2;

            return rectangle;
        }

        internal void Save(BinaryWriter writer)
        {
            writer.Write(Convert.ToDouble(position.X));
            writer.Write(Convert.ToDouble(position.Y));
            writer.Write(tileType.index);
        }

        internal void Load(BinaryReader reader)
        {
            position = new Vector2((float)reader.ReadDouble(), (float)reader.ReadDouble());

            tileType = TileFactory.GetTileType(reader.ReadInt32());
        }

        public Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {

            //Rectangle rectangle1 = this.GetNextCollisionRectangle();
            var temp = CollisionManager.DetectionAndDirection(GetCollisionRectangle(), rectangle);
            return temp;
            //return CollisionManager.detection(this.getCollsionRectangle(), rectangle);
        }

        public Rectangle GetCollisionRectangle()
        {
            var rectangle = tileType.texture.Bounds;

            rectangle.X = (int)position.X;
            rectangle.Y = (int)position.Y;
            rectangle.Width *= 2;
            rectangle.Height *= 2;

            return rectangle;
        }
    }
}
