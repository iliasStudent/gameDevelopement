using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Logic.AnimationEngine
{
    public class ProjectileAnimations
    {
        public static List<Animation> AllSnakeAnimation(ContentManager content)
        {
            List<Animation> projectileAnimaties = new List<Animation>() {
                   
                   
            };

            return projectileAnimaties;
        }

        public static List<Animation> AllLeafAnimation(ContentManager content)
        {
            List<Animation> projectileAnimaties = new List<Animation>() {
                   ProjectileAnimations.GetProjectileInAirAnimatieFromFairy(content),
                   ProjectileAnimations.GetProjectileHitAnimatieFromFairy(content)
            };

            return projectileAnimaties;
        }

        public static List<Animation> AllMinotaurAnimation(ContentManager content)
        {
            List<Animation> projectileAnimaties = new List<Animation>() {
            };

            return projectileAnimaties;
        }

        public static Animation GetProjectileInAirAnimatieFromFairy(ContentManager content)
        {
            int _width = 92;
            int _height = 102;

            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/ProjectileInAir");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.idle;

            for (int i = 0; i < 3; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        public static Animation GetProjectileHitAnimatieFromFairy(ContentManager content)
        {
            int _width = 92;
            int _height = 102;

            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/ProjectileHit");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 5; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        

        public static Animation GetProjectileHitAnimatieFromMinotaur(ContentManager content)
        {
            int _width = 100;
            int _height = 100;

            Texture2D texture = content.Load<Texture2D>("Monsters/Leaf/ProjectileHit");

            var animation = new Animation(texture);

            animation.AnimatieNaam = AnimationsTypes.attack1;

            for (int i = 0; i < 10; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(_width * i, 0, _width, _height)));
            }

            return animation;
        }

        
    }
}
