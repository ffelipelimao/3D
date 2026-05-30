using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        DEATH
    }
    public class AnimationBase : MonoBehaviour
    {
        public List<AnimationSetup> animationSetups;
        public Animator animator;


        public void PlayAnimationTrigger(AnimationType trigger)
        {
            var setup = animationSetups.Find(i => i.animationType == trigger);
            if (setup != null) animator.SetTrigger(setup.trigger);
        }
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string trigger;
    }
}

