using Logic.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Logic.Scenes
{
    public class MenuState : SceneState
    {

        List<Background> background2;
        Background backgroundCharacterSelect;
        public Song songMenu { get; set; }
        public double keyCooldown { get; set; }

        Texture2D image;
        Texture2D imageTitel;
        int count = 0;
        public double ElapsedGameTime { get; set; }
        public MenuState(MainGame game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch)
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            background2[count].Draw(_spriteBatch);
            if (ElapsedGameTime >= 120)
            {
                count++;
                ElapsedGameTime = 0;
            }

            if (count >= 1) 
            {
                count = 0;
            }
            
            _spriteBatch.End();
        }

        public override void Initialize()
        {
            
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(MainGame.GraphicsDevice);
            background2 = new List<Background>();

            songMenu = Content.Load<Song>("Sound/dofus_menu");
            MediaPlayer.Volume = 50.9f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songMenu);
            background2.Add(new Background(MainGame.Content.Load<Texture2D>("newStartScreen/adventure_time_pixel_enter"), new Rectangle(0, 0, 1600, 900)));
            

            imageTitel = null;
            image = null;

            
        }

        public override void Update(GameTime gameTime)
        {
            keyCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && keyCooldown > 500)
            { 
                MainGame.ChangeSceneState(new LvlOneState(MainGame, _graphics, _spriteBatch));

            }
        }
    }
}
