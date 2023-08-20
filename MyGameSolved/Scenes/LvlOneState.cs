using Logic.Core;
using Logic.Players;
using Logic.Data;
using Logic.Environment;
using Logic.ExtensionMethods;
using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Logic.Scenes
{
    public class LvlOneState : SceneState
    {
        private Hero hero { get; set; }
        public Song song1 { get; set; }
        public Song song1Battle { get; set; }

        
        private List<Enemy> monsters { get; set; }
        private List<Scrolling> _scrollingBackgrounds;
        private Tilemap tilemap { get; set; }

        private bool muziekBattle = false;
        const int speed = 3;


        private MouseState lastMouseState = new MouseState();
        private MouseState lastMouseStateRight = new MouseState();
        public LvlOneState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            SoundEffect goblinDeath = Content.Load<SoundEffect>("Sound/bull_death_sound");
            SoundEffect mushroomDeath = Content.Load<SoundEffect>("Sound/snake_siss_sound");
            SoundEffect hit = Content.Load<SoundEffect>("Sound/snake_siss_sound");
            SoundEffect andrewTakeHit = Content.Load<SoundEffect>("Sound/andrew_hit_sound");
            SoundEffect bullTakeHit = Content.Load<SoundEffect>("Sound/bull_hit_sound");


            monsters = new List<Enemy>(); // 700 300  X:1006.17267, Y:942.92206
            hero = new Hero(HeroAnimations.AllAnimation(Content), new Vector2(900, 700), andrewTakeHit);
            monsters.Add(new SnakeGiant(SnakeAnimations.AllAnimation(Content), ProjectileAnimations.AllSnakeAnimation(Content), new Vector2(4653, 1000),mushroomDeath,hit));

            monsters.Add(EnemyFactory.CreateEnemy("Snake", SnakeAnimations.AllAnimation(Content), ProjectileAnimations.AllSnakeAnimation(Content), new Vector2(2516, 900), mushroomDeath, hit));
            monsters.Add(EnemyFactory.CreateEnemy("Snake", SnakeAnimations.AllAnimation(Content), ProjectileAnimations.AllSnakeAnimation(Content), new Vector2(2149, 900), mushroomDeath, hit));
            monsters.Add(EnemyFactory.CreateEnemy("Snake", SnakeAnimations.AllAnimation(Content), ProjectileAnimations.AllSnakeAnimation(Content), new Vector2(3204, 1000), mushroomDeath, hit));
            monsters.Add(EnemyFactory.CreateEnemy("Minotaur", MinotaurAnimations.AllAnimation(Content), ProjectileAnimations.AllMinotaurAnimation(Content), new Vector2(1775, 850), goblinDeath, bullTakeHit));
            monsters.Add(EnemyFactory.CreateEnemy("Minotaur", MinotaurAnimations.AllAnimation(Content), ProjectileAnimations.AllMinotaurAnimation(Content), new Vector2(3696, 1000), goblinDeath, bullTakeHit));
            monsters.Add(EnemyFactory.CreateEnemy("Minotaur", MinotaurAnimations.AllAnimation(Content), ProjectileAnimations.AllMinotaurAnimation(Content), new Vector2(3274, 850), goblinDeath, bullTakeHit));

            song1 = Content.Load<Song>("Sound/fight1");
            song1Battle = Content.Load<Song>("Sound/fight1V2");

            MediaPlayer.Volume = 0.6f;
           
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Stop(); 
            muziekBattle = false;
            MediaPlayer.Play(song1);
            hero.hartjeVol = Content.Load<Texture2D>("heart/full_life");
            hero.hartjeLeeg = Content.Load<Texture2D>("heart/empty_life");

            _scrollingBackgrounds = new List<Scrolling>()
            {
                
            };

            
            TileFactory.load(GraphicsDevice, Content.Load<Texture2D>("Tilemap/Grass/autumn_tilemap"));
            using (FileStream fs = File.OpenRead(@"../../../Content/Tilemap/Grass/ExportedTilemapData.txt"))
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
                //hero.attack1(this.Monsters);
                hero.ChangeAnimation(AnimationsTypes.attack1);  


            }
     
            
            else if (Mouse.GetState().RightButton == ButtonState.Pressed && currentState.RightButton == ButtonState.Pressed &&
        lastMouseStateRight.RightButton == ButtonState.Released)
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
            lastMouseStateRight = currentState;

            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Update(gameTime);
            }

            hero.update(gameTime, tilemap, monsters);

            foreach (var monster in this.monsters)
            {
                monster.Update(gameTime, hero, tilemap);
            }


            if (hero.position.X > 4285 && muziekBattle == false)
            {
                muziekBattle = true;
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(song1Battle);
            }


            if (hero.isDead)
            {
                MainGame.ChangeSceneState(new DeathState(MainGame, _graphics, _spriteBatch));
            }

            if (monsters[0].isDead)
            {
                MainGame.ChangeSceneState(new LvlTwoState(MainGame, _graphics, _spriteBatch));
            }

            monsters.RemoveAll(x => x.isDead);
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

            var Texture2D = new Texture2D(MainGame.GraphicsDevice, 1, 1);
            Texture2D.SetData(new[] { Color.Red });

            hero.draw(_spriteBatch);

            foreach (var monster in this.monsters)
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
