using UnityEngine;

namespace Util.Extensions
{
    public static class AnimatorExtensions
    {
        public static void Reset(this Animator animator)
        {
            animator.Rebind();
            animator.Update(0f);
        }

    }
}
