using UnityEngine;

namespace JH.CORE
{
    public class ResetAnimationOffscreen : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        [SerializeField] string animationName = "";


        private void OnBecameInvisible() 
        {
            animator.Play(animationName, -1, 0f);
        }
    }
}