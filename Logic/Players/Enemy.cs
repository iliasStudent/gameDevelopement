using Logic.Core;
using Logic.Characters;
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

namespace Logic.Players
{
    public enum moveActions
    {
        left,
        right,
        idle
    }

    public abstract class Enemy : ICollisionable, IAnimationable, IHitable, IMoveable
    {
        public Animation currentAnimation { get; set; }
        public List<Animation> Animaties { get; set; }
        public bool lookingLeft { get; set; }
        public Stats stats { get; set; }
        public double invisibleTimer { get; set; }
        public bool invisible { get; set; }
        public virtual Movement Movement { get; set; }
        public Vector2 position { get; set; }
        public Animation projectileHitAnimation { get; set; }
        public Animation projectileInAirAnimation { get; set; }
        public List<Projectile> projectiles { get; set; }
        public double attackCooldownTimer { get; set; }
        public bool attackCooldown { get; set; }
        public bool isDead { get; set; }
        public moveActions moveActions { get; set; }
        public double moveCooldownTimer { get; set; }
        public int moveOffset { get; set; }

        public SoundEffect deathSound { get; set; } 
        public SoundEffect hitSound { get; set; }

        public abstract void Draw(SpriteBatch spriteBacth);
        public virtual void Update(GameTime gameTime, Hero hero, Tilemap tilemap)
        {
            Follow(gameTime, hero, tilemap);

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
        public abstract void Follow(GameTime gameTime, Hero hero, Tilemap tilemap);
        public virtual void Move(GameTime gameTime, Tilemap tilemap)
        {
            if (currentAnimation.AnimatieNaam.canMove())
            {

                this.Movement.update(gameTime, this, this);

                List<Tuple<CollisionDirection, Rectangle>> directions = tilemap.hitAnyTile(GetCollisionRectangle());
                foreach (var direction in directions)
                {
                    if (this.Movement.Velocity.X < 0 && direction.Item1 == CollisionDirection.left)
                    {
                        this.Movement.Velocity.X = 0;
                        position += new Vector2(direction.Item2.Width, 0);
                    }

                    if (this.Movement.Velocity.X > 0 & direction.Item1 == CollisionDirection.right)
                    {
                        this.Movement.Velocity.X = 0;
                        position += new Vector2(-direction.Item2.Width, 0);
                    }

                    if (this.Movement.Velocity.Y < 0 && direction.Item1 == CollisionDirection.up)
                    {
                        this.Movement.Velocity.Y = 0;
                        position += new Vector2(0, direction.Item2.Height);
                    }

                    if ((this.Movement.Velocity.Y > 0 & direction.Item1 == CollisionDirection.down))
                    {
                        position += new Vector2(0, -direction.Item2.Height);

                        this.Movement.Velocity.Y = 0;
                        this.Movement.InAir = false;
                    }
                }
            }
            else
            {
                this.Movement.Velocity.X = 0;
            }

            if (this.Movement.InAir == false)
            {
                if (tilemap.hitAnyTile(GetUnderCollisionRectangle()).Count <= 0)
                {
                    this.Movement.InAir = true;
                }
            }
        }

        public virtual void RandomMovement(GameTime gameTime)
        {

            if (moveCooldownTimer >= 1000)
            {
                moveCooldownTimer = 0;

                Random random = new Random();
                var getal = random.Next(0, 4);
                if (getal == 1)
                {
                    moveActions = moveActions.left;
                }
                else if (getal == 2)
                {
                    moveActions = moveActions.right;
                }
                else
                {
                    moveActions = moveActions.idle;
                }
            }

            moveCooldownTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            

            switch (moveActions)
            {
                case moveActions.left:
                    if (moveOffset >= -20)
                    {
                        Movement.left(this);
                        moveOffset--;
                    }
                    break;
                case moveActions.right:
                    if (moveOffset <= 20)
                    {
                        Movement.right(this);
                        moveOffset++;
                    }
                    break;
                case moveActions.idle:
                    break;
                default:
                    break;
            }
        }

        public void ChangeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false)
        {
            if (!(this.currentAnimation.AnimatieNaam == animationsTypes) && (this.currentAnimation.AnimatieNaam.isHigherPriority(animationsTypes) || ignorePriority))
            {
                this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == animationsTypes);
                if (this.currentAnimation == null)
                {
                    this.currentAnimation = this.Animaties.FirstOrDefault(x => x.AnimatieNaam == AnimationsTypes.idle);
                }
                this.currentAnimation.Reset();
            }
        }
        public abstract Tuple<CollisionDirection, Rectangle> CollisionDetection(Rectangle rectangle);
        public Rectangle GetUnderCollisionRectangle()
        {
            var rectangle = GetCollisionRectangle();
            rectangle.Height += 10;
            return rectangle;
        }
        public abstract Rectangle GetMonsterRangeRectangle();
        public abstract Rectangle GetCollisionRectangle();
        public abstract Rectangle GetNextCollisionRectangle();
        public abstract void Attack1(Hero hero);
        public abstract void Attack2(Hero hero);
        public abstract void Attack3(Hero hero);
        public void Hit(int damage)
        {
            if (!invisible)
            {
                HitSoundMet();
                stats.health -= damage;
                invisible = true;
                ChangeAnimation(AnimationsTypes.hit);
                if (stats.health <= 0)
                {
                    ChangeAnimation(AnimationsTypes.death);
                    DeadSound();
                }
            }
        }
        public abstract void DeadSound();
        public abstract void HitSoundMet();
        public void EndOfAnimation()
        {
            if (this.currentAnimation.IsFinished)
            {
                switch (this.currentAnimation.AnimatieNaam)
                {
                    case AnimationsTypes.attack1:
                    case AnimationsTypes.attack2:
                    case AnimationsTypes.attack3:
                        ChangeAnimation(AnimationsTypes.idle, true);
                        attackCooldown = true;
                        break;
                    case AnimationsTypes.hit:
                        ChangeAnimation(AnimationsTypes.idle, true);
                        break;
                    case AnimationsTypes.death:
                        position = new Vector2(100, 100);
                        isDead = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
