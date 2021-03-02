using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] string defaultAnimationName = string.Empty;

        [SerializeField] protected Animator animator;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (!string.IsNullOrEmpty(defaultAnimationName))
                animator.SetTrigger(defaultAnimationName);
        }

        public virtual void Play(string animationName)
        {
            animator.speed = 1;
            animator.SetTrigger(animationName);
        }

        public void Stop()
        {
            animator.speed = 0;
        }

        public void Reset(string animationName)
        {
            if (!string.IsNullOrEmpty(animationName))
            {
                animator.ResetTrigger(animationName);
            }
        }
    }
}