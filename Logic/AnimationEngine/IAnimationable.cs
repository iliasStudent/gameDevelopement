using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.AnimationEngine
{
    public interface IAnimationable
    {
        public Animation currentAnimation { get; set; }
        public List<Animation> Animaties { get; set; }
        public bool lookingLeft { get; set; }

        public void ChangeAnimation(AnimationsTypes animationsTypes, bool ignorePriority = false);

        public void EndOfAnimation();
    }
}
