using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Logic.AnimationEngine
{
    public class FairyAnimations
    {
        public static int _width { get; set; } = 32;
        public static int _height { get; set; } = 44;

        public static List<Animation> AllAnimation(ContentManager content)
        {
            List<Animation> fairyAnimations = new List<Animation>() {
                   FairyAnimations.GetIdleAnimatieFromFairy(content),
                   FairyAnimations.GetRunAnimatieFromFairy(content),
                   FairyAnimations.GetTakeHitAnimatieFromFairy(content),
                   FairyAnimations.GetDeathAnimatieFromFairy(content),
                   FairyAnimations.GetAttack1AnimatieFromFairy(content),
                   FairyAnimations.GetAttack2AnimatieFromFairy(content),
                   FairyAnimations.GetAttack3AnimatieFromFairy(content)
            };

            return fairyAnimations;
        }


        public static Animation GetIdleAnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafIdle");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animation GetRunAnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafRun");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetTakeHitAnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafHit");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 4; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetDeathAnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafDead");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 5; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetAttack1AnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafAttack1");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animation GetAttack2AnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafAttack1");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetAttack3AnimatieFromFairy(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/leafThrow");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack3;

            for (int i = 0; i < 7; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
    }
}