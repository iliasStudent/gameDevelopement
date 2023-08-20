using Logic.Core;
using Logic.Players;
using Logic.Data;
using Logic.Environment;
using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Logic.Scenes
{
    public class LvlTwoState : SceneState
    {
        private Hero hero { get; set; }

        public Song song2 { get; set; }

        public Song song2Battle { get; set; }
        private bool muziekBattle2 = false;

        public Enemy fairyGiant { get; set; }
        public Enemy boss2 { get; set; }
        private List<Enemy> monsters { get; set; }

        private List<Scrolling> _scrollingBackgrounds;
        private Tilemap tilemap { get; set; }

        const int speed = 3;


        private MouseState lastMouseState = new MouseState();
        public LvlTwoState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            monsters = new List<Enemy>();

            SoundEffect deathSkeleton = Content.Load<SoundEffect>("Sound/female_death_sound");
            SoundEffect hitSkeleton = Content.Load<SoundEffect>("Sound/female_hit_sound");
            
            SoundEffect andrewTakeHit = Content.Load<SoundEffect>("Sound/andrew_hit_sound");

            hero = new Hero(HeroAnimations.AllAnimation(Content), new Vector2(1353, 1450), andrewTakeHit);

            monsters.Add(new FairyGiant(FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(11344, 1518),deathSkeleton, hitSkeleton));
            monsters.Add(new FairyGiant(FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(12147, 1518),deathSkeleton, hitSkeleton));
            fairyGiant = monsters[0];
            boss2 = monsters[1];

            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(1211, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(2270, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(3433, 2532), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(3646, 2532), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(4124, 2298), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(4165, 2042), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(4095, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(4568, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(5342, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(5982, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(6808, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(7967, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(9240, 1828), deathSkeleton, hitSkeleton));
            monsters.Add(EnemyFactory.CreateEnemy("Fairy", FairyAnimations.AllAnimation(Content), ProjectileAnimations.AllLeafAnimation(Content), new Vector2(8567, 900), deathSkeleton, hitSkeleton));
            
            song2 = Content.Load<Song>("Sound/zelda_music");
            song2Battle = Content.Load<Song>("Sound/mystery_chiptune");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Stop();
            MediaPlayer.Play(song2);
            hero.hartjeVol = Content.Load<Texture2D>("heart/full_life");
            hero.hartjeLeeg = Content.Load<Texture2D>("heart/empty_life");

            _scrollingBackgrounds = new List<Scrolling>()
            {
              
            };

            

            TileFactory.load(GraphicsDevice, Content.Load<Texture2D>("Tilemap/Castle/ExportedTileSet"));
            using (FileStream fs = File.OpenRead(@"../../../Content/Tilemap/Castle/ExportedTilemapData.txt"))
            {
                tilemap = new Tilemap(fs);
            }
        }

        public override void Update(GameTime gameTime)
        {

            MouseState currentState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Q) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                hero.Movement.left(hero);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                hero.Movement.right(hero);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Pressed &&
        lastMouseState.LeftButton == ButtonState.Released)
            {
                hero.ChangeAnimation(AnimationsTypes.attack1);
            }
            else if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                hero.ChangeAnimation(AnimationsTypes.attack2);
            }
            else
            {
                hero.ChangeAnimation(AnimationsTypes.idle);
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Z)) && hero.Movement.InAir == false)
            {
                hero.Movement.jump();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                TileFactory.Save(GraphicsDevice);
                using (FileStream fs = File.Create(@"ExportedTilemapData.txt"))
                {
                    tilemap.Save(fs);
                }
            }

            lastMouseState = currentState;

            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);


            }

            hero.update(gameTime, tilemap, monsters);

            foreach (var monster in this.monsters.Where(x => CollisionManager.Detection(x.GetMonsterRangeRectangle(), getScreen())))
            {
                monster.Update(gameTime, hero, tilemap);
            }


            if (hero.position.X > 9570 && muziekBattle2 == false)
            {
                muziekBattle2 = true;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(song2Battle);
            }


            if (hero.isDead)
            {
                MainGame.ChangeSceneState(new DeathState(MainGame, _graphics, _spriteBatch));
            }




            if (fairyGiant.isDead && boss2.isDead)
            {
                MainGame.ChangeSceneState(new VictoryState(MainGame, _graphics, _spriteBatch));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(182, 211, 242));

            var position = Matrix.CreateTranslation(
                  -hero.GetCollisionRectangle().Center.X,
                  -hero.GetCollisionRectangle().Center.Y,
                  0);

            var offset = Matrix.CreateTranslation(
                Settings.ScreenW / 2,
                Settings.ScreenH / 2,
                0);

            var Transform = position * offset;

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, transformMatrix: Transform);

            const int Width = 50 * 2;
            const int Height = 29 * 2;
            const int yOffset = 0;

            var Texture2D = new Texture2D(MainGame.GraphicsDevice, 1, 1);
            Texture2D.SetData(new[] { Color.Red });
           
            hero.draw(_spriteBatch);

            foreach (var monster in this.monsters.Where(x => CollisionManager.Detection(x.GetMonsterRangeRectangle(), getScreen())))
            {
                monster.Draw(_spriteBatch);
            }

            tilemap.draw(_spriteBatch, getScreen());


            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Draw(_spriteBatch);
            }
            _spriteBatch.End();
        }

        public Rectangle getScreen()
        {
            var position = new Point(hero.GetCollisionRectangle().Center.X - Settings.ScreenW / 2, hero.GetCollisionRectangle().Center.Y - Settings.ScreenH / 2);

            return new Rectangle(position, new Point(Settings.ScreenW, Settings.ScreenH));
        }
    }
}
