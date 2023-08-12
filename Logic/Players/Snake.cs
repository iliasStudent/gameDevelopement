﻿using Logic.Core;
using Logic.Characters;
using Logic.Environment;
using Logic.AnimationEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Logic.Data;
using Logic.ExtensionMethods;
using Microsoft.Xna.Framework.Audio;

namespace Logic.Players
{
    public class Snake : Enemy
    {
        public Snake(List<Animation> animaties, List<Animation> projectileAnimation, Vector2 newPosition, SoundEffect effect, SoundEffect hitEffect)
        {
            this.Animaties = animaties;
            
            
            this.position = newPosition;
            this.deathSound = effect;
            this.hitSound = hitEffect;
            this.Movement = new Movement();
            this.Movement.MaxSpeedX = 1f;
            this.Movement.jumpSpeed = 4f;

            this.lookingLeft = true;

            this.currentAnimation = animaties.First(x => x.AnimatieNaam == AnimationsTypes.idle);

            this.stats = new Stats(5, 1);
            this.projectiles = new List<Projectile>();
        }
        public override Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(gameTime ,hero, tilemap);

            Move(gameTime, tilemap);

            currentAnimation.Update(gameTime);

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

        public override void Follow(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            if (CollisionManager.Detection(GetMonsterRangeRectangle(), hero.GetCollisionRectangle()))
            {
                if (!CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 100, 30))
                {
                    if (hero.position.X >= position.X)
                    {
                        Movement.right(this);
                    }
                    else
                    {
                        Movement.left(this);
                    }
                }


                if (!attackCooldown && !Movement.InAir)
                {
                    if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 200, 30) && currentAnimation.AnimatieNaam.canMove())
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
                    else if (CollisionManager.Detection(hero.GetCollisionRectangle(), GetCollisionRectangle().Center, 1000, 30) && currentAnimation.AnimatieNaam.canMove())
                    {
                        Attack3(hero);
                    }
                }
                
            }
            else
            {
                RandomMovement(gameTime);
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
            beginPoint.X += 55;
            beginPoint.Y += 75;
            return new Rectangle(beginPoint, new Point(15 * 2, 30 * 2));
            //return new Rectangle((int)position.X, (int)position.Y, (int)currentAnimation.bounds.X, (int)currentAnimation.bounds.Y);
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

            _spriteBatch.Draw(currentAnimation.texture, position + currentAnimation.offset, currentAnimation.currentFrame.borders, Color.White, 0, Vector2.Zero, 4f, spriteEffects, 0.49f);

            foreach (var projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
        }

        public override void Attack1(Hero hero)
        {
            Debug.WriteLine("testAttack1: " );
            Debug.WriteLine($"THIS IS 1: {currentAnimation.AnimatieNaam} == {AnimationsTypes.attack1} && {currentAnimation.count} == 6");
            ChangeAnimation(AnimationsTypes.attack1);

            const int Width = 34 * 2;
            const int Height = 66 * 2;
            const int yOffset = 0;

            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack1 && currentAnimation.count == 3)
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
                Debug.WriteLine("test: " + attackCollsionRectangle.ToString());
                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage);
                }
            }
        }

        public override void Attack2(Hero hero)
        {
            ChangeAnimation(AnimationsTypes.attack2);
            
            const int Width = 34 * 2;
            const int Height = 66 * 2;
            const int yOffset = 0;
            
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack2 && currentAnimation.count == 3)
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
                Debug.WriteLine("test2: " + attackCollsionRectangle.ToString());
                if (CollisionManager.Detection(hero.GetCollisionRectangle(), attackCollsionRectangle))
                {
                    hero.Hit(stats.damage + 1);
                }
            }
        }

        public override void Attack3(Hero hero)
        {
            Random random = new Random();
            attackCooldownTimer = 1000 + (random.Next(0, 6) * 500);

            ChangeAnimation(AnimationsTypes.attack3);
            if (currentAnimation.AnimatieNaam == AnimationsTypes.attack3 && currentAnimation.count == 7)
            {
                shoot();
            }
        }

        public void shoot()
        {
            if (!attackCooldown)
            {
                attackCooldown = true;

                var center = GetCollisionRectangle().Center.ToVector2();
                center -= new Vector2(projectileInAirAnimation.bounds.X, projectileInAirAnimation.bounds.Y);
                projectiles.Add(new Projectile(projectileInAirAnimation.Clone(), projectileHitAnimation.Clone(), lookingLeft, center, new Rectangle(44, 44, 16, 16)));
            }
        }

        public override void DeadSound()
        {
            deathSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
        }

        public override void HitSoundMet()
        {
            hitSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
        }
    }
}