using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soleduo.CharacterComponent
{ 
    [System.Serializable]
    public class AnimationController
    {
        [SerializeField] protected Animator animator;

        public AnimationController(Animator animator)
        {
            this.animator = animator;
        }

        public void SetTrigger(string name)
        {
            animator.SetTrigger(name);
        }

        public void SetInt(string name, int value)
        {
            animator.SetInteger(name, value);
        }

        public void SetFloat(string name, float value)
        {
            animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            animator.SetBool(name, value);
        }
    }
}