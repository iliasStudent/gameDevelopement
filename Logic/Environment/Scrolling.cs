using Logic.Core;
using Logic.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Logic.Environment

{
    public class Scrolling : Component
    {
        public int offsetY;

        public Vector2 heroStartPosition;

        private bool _constantSpeed;

        private float _layer;

        private float _scrollingSpeed;

        private List<Sprite> _sprites;

        private readonly Hero hero;

        private float _speed;

        public float parallaxEffect;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
                foreach (var sprite in _sprites)
                {
                    sprite.Layer = _layer;
                }
            }
        }

        public Scrolling(Texture2D texture, Hero player, float scrollingSpeed, int offsetY = 0, bool constantSpeed = false)
            : this(new List<Texture2D>() { texture, texture, texture }, player, scrollingSpeed, offsetY, constantSpeed)
        {

        }

        public Scrolling(List<Texture2D> textures, Hero player, float scrollingSpeed, int offsetY = 0, bool constantSpeed = false)
        {
            hero = player;
            _scrollingSpeed = scrollingSpeed;
            _constantSpeed = constantSpeed;
            _sprites = new List<Sprite>();
            this.offsetY = 2400;

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(texture)
                {
                    Position = new Vector2((i * texture.Width) - 1, player.position.Y)
                });

            }

            heroStartPosition = hero.position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            foreach (var sprite in _sprites)
            {
                sprite.Draw(spriteBatch);

            }
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Position.Y = hero.GetCollisionRectangle().Center.Y - sprite.Rectangle.Height/2 - 200;
            }

            ApplySpeed(gameTime);

            CheckPosition();

        }

        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (!_constantSpeed || hero.Movement.Velocity.X != 0)
            {

                _speed *= hero.Movement.Velocity.X;
            }

            foreach (var sprite in _sprites)
            {
                sprite.Position.X -= _speed;
            }

        }


        private void CheckPosition()
        {

            var screen = getScreen();

            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (hero.Movement.Velocity.X > 0 && !CollisionManager.Detection(sprite.Rectangle, screen))
                {
                    var index = i - 1;

                        if (index < 0)
                        index = _sprites.Count - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                }

                if (hero.Movement.Velocity.X < 0 && !CollisionManager.Detection(sprite.Rectangle, screen))
                {
                    var index = i + 1;

                    if (index > _sprites.Count - 1)
                        index = 0;

                    sprite.Position.X = _sprites[index].Rectangle.Left - _sprites[index].Rectangle.Width - (_speed * 2);
                }
                //if (sprite.Rectangle.Right >= hero.position.X - heroStartPosition.X)
                //{
                //    var index = i + 1;

                //    if (index > 0)
                //        index = _sprites.Count + 1;

                //    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                //}

            }


        }

        public Rectangle getScreen()
        {
            var position = new Point(hero.GetCollisionRectangle().Center.X - Settings.ScreenW/2, hero.GetCollisionRectangle().Center.Y - Settings.ScreenH / 2);

            return new Rectangle(position, new Point(Settings.ScreenW, Settings.ScreenH));
        }
    }
}

