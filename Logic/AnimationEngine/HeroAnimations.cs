using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace Logic.AnimationEngine
{
    public class HeroAnimations
    {
        public static int _width { get; set; } = 62;
        public static int _height { get; set; } = 86;

        public static List<Animation> AllAnimation(ContentManager content)
        {
            List<Animation> heroAnimaties = new List<Animation>() {
                HeroAnimations.GetIdleAnimatieFromHero(content),
                HeroAnimations.GetRunAnimatieFromHero(content),
                HeroAnimations.GetFallAnimatieFromHero(content),
                HeroAnimations.GetJumpAnimatieFromHero(content),
                HeroAnimations.GetAttack1FromHero(content),
                HeroAnimations.GetHitFromHero(content),
                HeroAnimations.GetDeathFromHero(content),
                HeroAnimations.GetAttack2AnimatieFromHero(content)
            };

            return heroAnimaties;
        }

        public static Animation GetIdleAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewIdle");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            // Aantal motion in een afbeelding
            for (int i = 0; i < 12; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetRunAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewRun2");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 13; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetFallAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewIdle");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.fall;

            for (int i = 0; i < 5; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetJumpAnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewRun2");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.jump;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetAttack2AnimatieFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewFight2");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;
           
            for (int i = 0; i < 3; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetAttack1FromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewFight");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetHitFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/andrewHit");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetDeathFromHero(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Andrew/null");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 9; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
    }
}
