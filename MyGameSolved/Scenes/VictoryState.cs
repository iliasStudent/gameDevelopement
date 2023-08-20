using Logic.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Scenes
{
    public class VictoryState : SceneState
    {
        List<Background> background3;
        Texture2D imageTitleWin;
        public Song songVictory { get; set; }
        int count = 0;
        public double ElapsedGameTime { get; set; }

        public VictoryState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            background3[0].Draw(_spriteBatch);

            if (ElapsedGameTime >= 120)
            {
                count++;
                ElapsedGameTime = 0;
            }

            if (count >= 39)
            {
                count = 0;
            }

            _spriteBatch.Draw(imageTitleWin, new Vector2(280, 100), imageTitleWin.Bounds, Color.White, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);



            _spriteBatch.End();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);
            background3 = new List<Background>();
            MediaPlayer.Stop();
            songVictory = Content.Load<Song>("Sound/victory_sound");
            MediaPlayer.Volume = 0.7f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songVictory);
            for (int i = 0; i < 1; i++)
            {
                background3.Add(new Background(MainGame.Content.Load<Texture2D>("victoryScreen/you_win"), new Rectangle(0, 0, 1600, 900)));
            }

            imageTitleWin = MainGame.Content.Load<Texture2D>("victoryScreen/empty");


        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                MainGame.ChangeSceneState(new MenuState(MainGame, _graphics, _spriteBatch));
            }
        }
    }
}
