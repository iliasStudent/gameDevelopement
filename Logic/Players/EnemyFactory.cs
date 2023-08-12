using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Players
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(string monsterType, List<Animation> animaties, List<Animation> projectileAnimation, Vector2 newPosition, SoundEffect effect, SoundEffect hitEffect)
        {

            try
            {
                return (Enemy)Activator.CreateInstance(Type.GetType($"Logic.Players.{monsterType}"), new Object[] { animaties, projectileAnimation, newPosition, effect, hitEffect});
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
