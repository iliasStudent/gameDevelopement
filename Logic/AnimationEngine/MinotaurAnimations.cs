using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.AnimationEngine
{
    public class MinotaurAnimations
    {

        public static int _width { get; set; } = 96;
        public static int _height { get; set; } = 90;

        public static List<Animation> AllAnimation(ContentManager content)
        {
            List<Animation> GoblinAnimaties = new List<Animation>() {
                   MinotaurAnimations.GetIdleAnimatieFromMinotaur(content),
                   MinotaurAnimations.GetRunAnimatieFromMinotaur(content),
                   MinotaurAnimations.GetTakeHitAnimatieFromMinotaur(content),
                   MinotaurAnimations.GetDeathAnimatieFromMinotaur(content),
                   MinotaurAnimations.GetAttack1AnimatieFromMinotaur(content),
                   MinotaurAnimations.GetAttack2AnimatieFromMinotaur(content),
                   MinotaurAnimations.GetAttack3AnimatieFromMinotaur(content)
            };

            return GoblinAnimaties;
        }


        public static Animation GetIdleAnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurIdle");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 5; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animation GetRunAnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurRun");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.run;

            for (int i = 0; i < 8; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetTakeHitAnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurTakeHit");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.hit;

            for (int i = 0; i < 3; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetDeathAnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurDead");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.death;

            for (int i = 0; i < 6; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetAttack1AnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurAttack2");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 9; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }


        public static Animation GetAttack2AnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurAttack2");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack2;

            for (int i = 0; i < 9; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }
        public static Animation GetAttack3AnimatieFromMinotaur(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Monsters/Minotaur/minotaurAttack");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack3;

            for (int i = 0; i < 9; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }




            return animation;
        }
    }
}
