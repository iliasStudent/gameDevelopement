using Logic.Core;
using Logic.Data;
using Logic.Environment;
using Logic.ExtensionMethods;
using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Players
{
    public class FairyGiant : Fairy
    {
        public FairyGiant(List<Animation> animaties, List<Animation> projectileAnimation, Vector2 newPosition, SoundEffect effect, SoundEffect hitEffect) : base(animaties, projectileAnimation, newPosition, effect, hitEffect)
        {
            stats.maxHealth = 10;
            stats.health = 10;
        }

        public override void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(hero, tilemap);

            Move(gameTime, tilemap);

            currentAnimation.Update(gameTime, 150);

            EndOfAnimation();

            if (attackCooldown)
            {
                attackCooldownTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (attackCooldownTimer <= 0)
            {
                attackCooldown = false;
                attackCooldownTimer = 0;
            }

            if (invisible)
            {
                invisibleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (invisibleTimer >= 2000)
            {
                invisible = false;
                invisibleTimer = 0;
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1)
            {
                Attack1(hero);
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2)
            {
                Attack2(hero);
            }

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3)
            {
                Attack3(hero);
            }

            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime, hero, tilemap);
            }

            projectiles.RemoveAll(x => x.isRemove);
        }

        public void Follow(Hero hero, Tilemap tilemap)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
            {
                if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 200, 50))
                {
                    if (hero.GetCollisionRectangle().Center.X >= GetCollisionRectangle().Center.X)
                    {
                        Movement.right(this);
                    }
                    else
                    {
                        Movement.left(this);
                    }
                }
                else
                {
                    if (hero.GetCollisionRectangle().Center.X >= GetCollisionRectangle().Center.X)
                    {
                        lookingLeft = false;
                    }
                    else
                    {
                        lookingLeft = true;
                    }
                }

                if (!attackCooldown && !Movement.InAir)
                {
                    if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 400, 50) && currentAnimation.AnimatieNaam.canMove())
                    {
                        Random rand = new Random();
                        if (rand.Next(0, 2) == 1)
                        {
                            Attack1(hero);
                        }
                        else
                        {
                            Attack2(hero);
                        }
                    }
                    else if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 1000, 50) && currentAnimation.AnimatieNaam.canMove())
                    {
                        Attack3(hero);
                    }
                }
            }

            foreach (var tile in tilemap.MiddleGround.Tiles)
            {

                if (CollisionManager.Detection(new Rectangle((int)GetCollisionRectangle().Left - 45, (int)GetCollisionRectangle().Bottom - 30, 45, 16), tile.GetCollisionRectangle()) && Movement.Velocity.X < 0)
                {
                    Movement.jump();
                }
                else if (CollisionManager.Detection(new Rectangle((int)GetCollisionRectangle().Right, (int)GetCollisionRectangle().Bottom - 30, 45, 16), tile.GetCollisionRectangle()) && Movement.Velocity.X > 0)
                {
                    Movement.jump();
                }
            }
        }

        public override Rectangle GetMonsterRangeRectangle()
        {
            var center = GetCollisionRectangle().Center.ToVector2();
            var rectangle = new Rectangle(0, 0, 900, 900);
            rectangle.X = (int)(-rectangle.Width / 2 + center.X);
            rectangle.Y = (int)(-rectangle.Height / 2 + center.Y);
            return rectangle;
        }

        public override Rectangle GetCollisionRectangle()
        {
            var beginPoint = position.ToPoint();
            if (lookingLeft)
            {
                beginPoint.X += 120;
                beginPoint.Y += 113;
            }
            else
            {
                beginPoint.X += 120;
                beginPoint.Y += 113;
            }

            return new Rectangle(beginPoint, new Point(60, 153));
        }

        public override Rectangle GetNextCollisionRectangle()
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            var spriteEffects = SpriteEffects.None;

            if (lookingLeft)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;

            }

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 8f, spriteEffects, 0.5f);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(_spriteBatch, 3f);
            }
        }

        public override void Attack1(Hero hero)
        {

            ChangeAnimation(AnimationsTypes.attack1);

            const int Width = 160;
            const int Height = 172;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 2) * 500;
                Rectangle attackCollsionRectangle;
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - Width, GetCollisionRectangle().Top + yOffset, Width, Height);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right, GetCollisionRectangle().Top + yOffset, Width, Height);
                }

                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage);
                }
            }
        }

        public override void Attack2(Hero hero)
        {
            ChangeAnimation(AnimationsTypes.attack2);

            const int Width = 160;
            const int Height = 172;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2 && currentAnimation.count == 6)
            {
                Random random = new Random();

                attackCooldownTimer = random.Next(0, 3) * 500;
                Rectangle attackCollsionRectangle;
                if (lookingLeft)
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Left - Width, GetCollisionRectangle().Top + yOffset, Width, Height);
                }
                else
                {
                    attackCollsionRectangle = new Rectangle(GetCollisionRectangle().Right, GetCollisionRectangle().Top + yOffset, Width, Height);
                }

                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage + 1);
                }
            }
        }

        public override void Attack3(Hero hero)
        {
            Random random = new Random();
            attackCooldownTimer = 2000 + (random.Next(0, 10) * 500);

            ChangeAnimation(AnimationsTypes.attack3);
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3 && currentAnimation.count == 3)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (!attackCooldown)
            {
                attackCooldown = true;

                var hitbox = new Rectangle(120, 126, 72, 72);
                var center = GetCollisionRectangle().Center.ToVector2();
                center -= hitbox.Center.ToVector2();
                projectiles.Add(new Projectile(projectileInAirAnimation.Clone(), projectileHitAnimation.Clone(), lookingLeft, center, hitbox));
            }
        }
    }
}
