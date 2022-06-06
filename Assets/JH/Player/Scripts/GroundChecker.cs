using System;
using JZ.AUDIO;
using UnityEngine;
using JH.LEVEL;

namespace JH.PLAYER
{
    public class GroundChecker : MonoBehaviour, IOnResetLevel
    {
        #region //Cached components
        [SerializeField] private Collider2D myCollider = null;
        [SerializeField] private LayerMask collisionMask = -1;
        [SerializeField] private LayerMask jumpPadMask = -1;
        private Animator animator = null;
        private SoundPlayer sfxPlayer = null;
        #endregion

        #region //Collision variables
        [SerializeField] private float castDistance = 1;
        private bool isGrounded = true;
        public event Action OnLanded;
        #endregion
    
        #region //Monobehaviour
        private void Awake() 
        {
            sfxPlayer = GetComponent<SoundPlayer>();    
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            bool touchingGround = false;
            bool touchingJumpPad = false;

            RaycastHit2D[] hits = new RaycastHit2D[10];
            Debug.DrawRay(myCollider.bounds.min, Vector2.down * castDistance, Color.green);
            int numHits = myCollider.Cast(Vector2.down, hits, castDistance);
            for(int ii = 0; ii < numHits; ii++)
            {
                if((1 << hits[ii].collider.gameObject.layer & collisionMask) != 0)
                    touchingGround = true;

                if((1 << hits[ii].collider.gameObject.layer & jumpPadMask) != 0)
                    touchingJumpPad = true;
            }

            if(touchingGround && !touchingJumpPad)
                Landing();
            else
                isGrounded = false;
        }

        private void LateUpdate()
        {
            animator.SetBool("isInAir", !isGrounded);
        }
        #endregion

        public bool GetIsGrounded()
        {
            return isGrounded;
        }

        private void Landing()
        {
            if(isGrounded) return;
            sfxPlayer.Play("Land");
            OnLanded?.Invoke();
            isGrounded = true;
        }

        public void OnResetLevel()
        {
            isGrounded = true;
        }
    }
}
