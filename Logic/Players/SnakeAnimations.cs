using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Logic.AnimationEngine
{
    public class SnakeAnimations
    {

        public static int _width { get; set; } = 32;
        public static int _height { get; set; } = 38;


        public static List<Animation> AllAnimation(ContentManager content)
        {
            List<Animation> mushroomAnimaties = new List<Animation>() {
                   SnakeAnimations.GetIdleAnimatieFromSnake(content),
                   SnakeAnimations.GetRunAnimatieFromSnake(content),
                   SnakeAnimations.GetTakeHitAnimatieFromSnake(content),
                   SnakeAnimations.GetDeathAnimatieFromSnake(content),
                   SnakeAnimations.GetAttack1AnimatieFromSnake(content),
                   SnakeAnimations.GetAttack2AnimatieFromSnake(content),
                   SnakeAnimations.GetAttack3AnimatieFromSnake(content)
            };

            return mushroomAnimaties;
        }

        public static Animation GetIdleAnimatieFromSnake(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Cobra/cobraIdle");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetRunAnimatieFromSnake(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Cobra/cobraRun");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetTakeHitAnimatieFromSnake(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Cobra/cobraTakeHit");
            

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;
            
            for (int i = 0; i < 4; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetDeathAnimatieFromSnake(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Cobra/cobraDead");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 6; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

       
