using System;
using System.Collections;
using UnityEngine;

namespace Utilities.AnimatorChecker
{
    public static class AnimationChecks
    {
        public static IEnumerator CheckForAnimationChanged(Animator animator, Action onAnimationChanged)
        {
            var currentAnimation = animator.GetCurrentAnimatorClipInfo(0);
            do
            {
                yield return null;
                
            } while (currentAnimation[0].clip == animator.GetCurrentAnimatorClipInfo(0)[0].clip);

            onAnimationChanged.Invoke();
        }
        
        public static IEnumerator CheckForAnimationFinished(Animator animator, Action onAnimationFinished)
        {
            yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            
            onAnimationFinished.Invoke();
        }
    }
}