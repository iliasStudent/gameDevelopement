using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Logic.AnimationEngine
{
    public enum AnimationsTypes
    {
        idle,
        run,
        attack1,
        attack2,
        attack3,
        jump,
        fall,
        hit,
        death,
    }

    public class Animation
    {
        public AnimationsTypes AnimatieNaam { get; set; }

        public Texture2D texture { get; set; }

        public List<AnimationFrame> frames { get; set; }

        public AnimationFrame currentFrame { get; set; }

        public double ElapsedGameTime { get; set; }

        public int count { get; set; }

        public Vector2 offset { get; set; }

        public Vector2 bounds { get; set; }

        public bool IsFinished { get; set; }

        public Animation(Texture2D texture)
        {
            this.texture = texture;
            this.frames = new List<AnimationFrame>();
            this.offset = new Vector2();

            count = 0;
            IsFinished = false;
        }

        public Animation(Animation animatie)
        {
            this.AnimatieNaam = animatie.AnimatieNaam;
            this.bounds = animatie.bounds;
            this.count = animatie.count;
            this.currentFrame = animatie.currentFrame;
            this.ElapsedGameTime = animatie.ElapsedGameTime;
            this.frames = animatie.frames;
            this.IsFinished = animatie.IsFinished;
            this.offset = animatie.offset;
            this.texture = animatie.texture;
        }

        public void AddFrame(AnimationFrame animatieFrame)
        {
            this.frames.Add(animatieFrame);
            currentFrame = frames[0];
            bounds = new Vector2(frames[0].borders.Width, frames[0].borders.Height);
        }

        public void Update(GameTime gameTime)
        {
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedGameTime >= 200)
            {
                count++;
                ElapsedGameTime = 0;
            }


            if (count > frames.Count - 1)
            {
                count = 0;
                IsFinished = true;
            }

            currentFrame = frames[count];
        }

        public void Update(GameTime gameTime, int time)
        {
            ElapsedGameTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedGameTime >= time)
            {
                count++;
                ElapsedGameTime = 0;
            }


            if (count > frames.Count - 1)
            {
                count = 0;
                IsFinished = true;
            }

            currentFrame = frames[count];
        }

        public void Reset()
        {
            count = 0;
            IsFinished = false;
        }

        public Animation Clone()
        {
            return new Animation(this);
        }
    }
}
